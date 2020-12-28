using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent (typeof(LineRenderer))]
public class RopeScript : MonoBehaviour
{
    
    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    [SerializeField] private float ropeSegLen = 0.25f;
    [SerializeField] private int segmentLength = 35;
    private float lineWidth = 0.1f;

    [SerializeField] private int constraintAmmount;
    [SerializeField] private Transform startPoint;
    
    bool bPlayerIsOnRope = true;
    [SerializeField] Transform playerStartObject;
    [SerializeField] GameObject player;
    int objIndex;
    // Use this for initialization
    void Start()
    {
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
        if(Input.GetKeyDown(KeyCode.Space)) {
            bPlayerIsOnRope = false;
        }
        if(Input.GetKey(KeyCode.W)) {
            playerStartObject.Translate(0,0.1f,0);
            
        }
        else if (Input.GetKey(KeyCode.S)) {
            playerStartObject.Translate(0,-0.1f,0);
        }

        if(bPlayerIsOnRope) {
            // Check on which index is the player hanging
            float startY = startPoint.position.y;
            float startX = startPoint.position.x;

            float endY = startY + segmentLength * ropeSegLen;

            float objY = playerStartObject.position.y;
            float objX = playerStartObject.position.x;

            float difY = Mathf.Abs(startY - objY);
            float difX = Mathf.Abs(startX - objX);

            float diff = Mathf.Sqrt(difY * difY + difX * difX);
            globalDif = difY;
            float ratio = diff / Mathf.Abs(startY - endY);
            objIndex = (int) (segmentLength * ratio);
        
            if(Input.GetKey(KeyCode.A)) {
                RopeSegment last = ropeSegments[objIndex];
                last.posOld = last.posNow;
                last.posNow += new Vector2(-0.1f,0);
                ropeSegments[objIndex] = last;
            } else if(Input.GetKey(KeyCode.D)) {
                RopeSegment last = ropeSegments[objIndex];
                last.posOld = last.posNow;
                last.posNow += new Vector2(0.1f,0);
                ropeSegments[objIndex] = last;
            }
            player.transform.position = ropeSegments[objIndex - 1].posNow;
        }
        
    }

    private void FixedUpdate()
    {
        this.Simulate();
    }

    private void Simulate()
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


}   

