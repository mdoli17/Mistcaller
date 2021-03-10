using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEditor;




public enum PlayerState
{
    IDLE,
    INTERACTING,
    MOVING,
    ONROPE
}

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour
{
    private const string SAVE_FILENAME = "Quicksave";
    public float moveSpeed = 6;
    public float jumpHeight;
    public float timeToJump;
    
    [Range(0,1)]
    public float grabFriction;
    
    public float accelerationTimeAirbourne =.5f;
    public float accelerationTimeGrounded = .1f;


    public float mass;
    
    Controller2D controller;
    Vector3 velocity;

    float jumpVelocity;
    float gravity;
    float velocityXSmoothing;
    float grabSlow;

    Animator playerAnimator;

    private InteractChecker interactChecker;

    private PlayerState playerState;
    
    private bool bCanInteract;
Vector2 input;
    private void OnDrawGizmos() {
        // Handles.Label(transform.position + new Vector3(0, 5, 0), playerState.ToString());
        Gizmos.DrawLine(transform.position, transform.position + velocity.normalized);
    }

    // Start is called before the first frame update
    void Start()
    {
        interactChecker = GetComponentInChildren<InteractChecker>();

        controller = GetComponent<Controller2D>();
        CalculateGravityAndVelocity();
        playerAnimator = GetComponentInChildren<Animator>();
        grabSlow = 1;
        
    }

    private bool bSpace;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bSpace = true;
            playerAnimator.SetBool("bIsJumping", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            bSpace = false;
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            GetComponentInChildren<HandleCheck>().grab();
            grabSlow = grabFriction;
        }

        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            GetComponentInChildren<HandleCheck>().letGo();
            grabSlow = 1;
        }

        Vector3 characterScale = transform.localScale;
        if(playerState != PlayerState.INTERACTING && playerState != PlayerState.ONROPE)
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
                characterScale.x = Mathf.Abs(transform.localScale.x) * -1;
            else if (Input.GetAxisRaw("Horizontal") > 0)
                characterScale.x = Mathf.Abs(transform.localScale.x);

            transform.localScale = characterScale;
        }

        if(velocity.y < -2f && !controller.collisions.below)
        {
            playerAnimator.SetBool("bIsJumping", false);
            playerAnimator.SetBool("bIsFalling", true);
        }
        else if (controller.collisions.below)
        {
            playerAnimator.SetBool("bIsFalling", false);
        }
        playerAnimator.SetFloat("Player Speed", Mathf.Abs(velocity.x));


        if(Input.GetKeyDown(KeyCode.E))
        {
            GameObject interactable = interactChecker.getInteractableObject();
            if(interactable)
            {
                Debug.Log("Interactable Found");
                interactable.GetComponent<Interactable>().Interact();
            }   
        }
    }

    void FixedUpdate()
    {

        if(controller.collisions.above || controller.collisions.below)
        {
            if(controller.collisions.slidingDownMaxSlope)
            {
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            } 
            else{

                velocity.y = 0;
            }
        }
    
         input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
  
        float targetVelocityX = input.x * moveSpeed * grabSlow;
        if(bSpace && controller.collisions.below && !controller.collisions.slidingDownMaxSlope)
        {
            velocity.y = jumpVelocity;
            
            targetVelocityX /= 2;
        }

        if(controller.collisions.below)
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirbourne); 
    
        velocity.y += gravity * Time.deltaTime;
        
        if(playerState != PlayerState.INTERACTING && playerState != PlayerState.ONROPE)
        {
            controller.Move(velocity * Time.deltaTime);
        }
        if(input.x == 0)
        {
            if(playerState != PlayerState.IDLE  && playerState != PlayerState.INTERACTING && playerState != PlayerState.ONROPE)
            {
                playerState = PlayerState.IDLE;
            }
        }
        else
        {
            if(playerState != PlayerState.MOVING  && playerState != PlayerState.INTERACTING && playerState != PlayerState.ONROPE)
            {
                playerState = PlayerState.MOVING;
            }
        }
    }

    void CalculateGravityAndVelocity()
    {
        gravity = - (2 * jumpHeight) / (timeToJump * timeToJump);
        jumpVelocity = Mathf.Abs(gravity) * timeToJump;
        
    }

    public Vector3 getVelocity()
    {
        return velocity;
    }

    public float getGravity()
    {
        return gravity;
    }

    public void SetPlayerState(PlayerState state)
    {
        playerState = state;
    }

    public PlayerState GetPlayerState()
    {
        return playerState;
    }

    public void resetInteract(float time)
    {
        StartCoroutine(InteractCooldown(time));
    }

    IEnumerator InteractCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        bCanInteract = true;
    }

    public void LaunchCharacter(Vector2 direction, bool overrideVelocity) {
        Vector3 newDir = new Vector3(direction.x / 2.5f, direction.y, 0) * jumpVelocity;
        velocity = overrideVelocity ? newDir : velocity + newDir;
    }

    public void ResetVelocity() {
        velocity = Vector2.zero;
    }

    public void Die()
    {
        GameManager.ResetLevel();
    }
}
