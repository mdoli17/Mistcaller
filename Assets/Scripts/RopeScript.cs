using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent (typeof(LineRenderer))]
public class RopeScript : MonoBehaviour
{
    public LayerMask ropeHangingMask;
    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    [SerializeField] private float ropeSegLen = 0.25f;
    [SerializeField] private int segmentLength = 35;
    public float lineWidth = 0.1f;

    [SerializeField] private int constraintAmmount;
    [SerializeField] private Transform startPoint;
    
    bool bPlayerIsOnRope = false;
    Transform PlayerStartTransform;
    Player player;
    int objIndex;
    // Use this for initialization
    void Start()
    {
        PlayerStartTransform = new GameObject().transform;
        PlayerStartTransform.parent = gameObject.transform;
        this.lineRenderer = this.GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = startPoint.position;

        for (int i = 0; i < segmentLength; i++)
        {
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }
    float globalDif;
    // Update is called once per frame
    void Update()
    {
        this.DrawRope();
        
        if(player) {
            SimulatePlayerOnRope();
        }
    }

    private void FixedUpdate()
    {
        this.SimulateRope();
    }
    private void RemovePlayerFromRope(Vector2 launchDirection) {
        player.SetPlayerState(PlayerState.IDLE);
        player.LaunchCharacter(launchDirection, true);
        player = null;
        StartCoroutine(resetRope());
    }
    private void SimulateRope()
    {
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -1.5f);

        for (int i = 1; i < this.segmentLength; i++)
        {
            RopeSegment curSegment = this.ropeSegments[i];
            Vector2 velocity = curSegment.posNow - curSegment.posOld;
            curSegment.posOld = curSegment.posNow;
            curSegment.posNow += velocity;
            curSegment.posNow += forceGravity * Time.fixedDeltaTime;
            this.ropeSegments[i] = curSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < constraintAmmount; i++)
        {
            this.ApplyConstraint();
        }
    }

    private void SimulatePlayerOnRope() {
        float playerXInput = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space)) {
            RemovePlayerFromRope(new Vector2(playerXInput,1));
        }
        
        if(Input.GetKey(KeyCode.W)) {
            float difY = startPoint.position.y - PlayerStartTransform.position.y;
            if(difY > 1)
                PlayerStartTransform.Translate(0,0.1f,0);            
        }
        else if (Input.GetKey(KeyCode.S)) {
            float difY = PlayerStartTransform.position.y - (startPoint.position.y - segmentLength * ropeSegLen);
            if(difY > 1) 
                PlayerStartTransform.Translate(0,-0.1f,0);
            else {
                RemovePlayerFromRope(new Vector2(0,0));
            }
        }                

        if(player.GetPlayerState() == PlayerState.ONROPE) {
            // Check on which index is the player hanging
            float startY = startPoint.position.y;
            float startX = startPoint.position.x;

            float endY = startY + segmentLength * ropeSegLen;

            float objY = PlayerStartTransform.position.y;
            float objX = PlayerStartTransform.position.x;

            float difY = Mathf.Abs(startY - objY);
            float difX = Mathf.Abs(startX - objX);

            float diff = Mathf.Sqrt(difY * difY + difX * difX);
            
            float ratio = diff / Mathf.Abs(startY - endY);
            objIndex = (int) (segmentLength * ratio);
            globalDif = difY;

            // TODO: Adding Force should be restricted
            float friction = Mathf.Abs((player.transform.position - startPoint.position).normalized.x);
            Debug.Log(friction);
            if(Input.GetKey(KeyCode.A)) {
                RopeSegment last = ropeSegments[objIndex];
                last.posOld = last.posNow;
                last.posNow += new Vector2(-10f,0) * Time.deltaTime;
                ropeSegments[objIndex] = last;
            } else if(Input.GetKey(KeyCode.D)) {
                RopeSegment last = ropeSegments[objIndex];
                last.posOld = last.posNow;
                last.posNow += new Vector2(10f,0) * Time.deltaTime;
                ropeSegments[objIndex] = last;
            }
            player.gameObject.transform.position = ropeSegments[objIndex - 1].posNow;
        }
    }

    private void ApplyConstraint()
    {
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = startPoint.position;
        this.ropeSegments[0] = firstSegment;

        for (int i = 0; i < this.segmentLength - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            } else if (dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }
        }
    }

    private void DrawRope()
    {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[this.segmentLength];
        for (int i = 0; i < this.segmentLength; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
            if(!bPlayerIsOnRope) {
                if (i != segmentLength - 1) {
                    Vector2 direction = (ropeSegments[i].posNow - ropeSegments[i + 1].posNow);
                    RaycastHit2D hit = Physics2D.Raycast(ropeSegments[i].posNow, direction , ropeSegLen, ropeHangingMask);
                    Debug.DrawRay(ropeSegments[i].posNow, direction, Color.red);

                    if(hit) {
                        player = hit.transform.gameObject.GetComponent<Player>();
                        PlayerStartTransform.position = player.gameObject.transform.position;
                        player.SetPlayerState(PlayerState.ONROPE);

                        RopeSegment hitSegment = ropeSegments[i];
                        hitSegment.posOld = hitSegment.posNow;
                        hitSegment.posNow += new Vector2(player.getVelocity().x / 15, 0f);
                        ropeSegments[i] = hitSegment;
                        
                        RopeSegment hitSegment2 = ropeSegments[i - 1];
                        hitSegment2.posOld = hitSegment2.posNow;
                        hitSegment2.posNow += new Vector2(player.getVelocity().x / 15, 0f);
                        ropeSegments[i - 1] = hitSegment2;

                        if (i + 1 <= segmentLength - 1) {
                            RopeSegment hitSegment1 = ropeSegments[i + 1];
                            hitSegment1.posOld = hitSegment1.posNow;
                            hitSegment1.posNow += new Vector2(player.getVelocity().x / 15, 0f);
                            ropeSegments[i + 1] = hitSegment1;
                        }

                        bPlayerIsOnRope = true;
                    }
                }
            }
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(startPoint.position, 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(startPoint.position - new Vector3(0f, globalDif,0), 0.1f);
        
    }


    void LowerRope() {
        Debug.Log("Lowering Rope");
        StartCoroutine(ELowerRope());
    }

    IEnumerator ELowerRope() {
        for(int i = 0; i < 80; i++)
        {
            ropeSegLen += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator resetRope() {
        yield return new WaitForSeconds(0.5f);
        bPlayerIsOnRope = false;
    }


}   

