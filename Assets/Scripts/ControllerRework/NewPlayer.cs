using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NewController2D))]
public class NewPlayer : MonoBehaviour {

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	NewController2D controller;

	Vector2 directionalInput;
	bool wallSliding;
	int wallDirX;

	private InteractChecker interactChecker;


	[Range(0,1)]
    public float grabFriction;
	float grabSlow;


	private PlayerState playerState;
    Animator playerAnimator;

	private bool bCanInteract;
	private int numOfJumps;
	public int MaxNumOfJumps = 2;


	void Start() {
		controller = GetComponent<NewController2D> ();
		interactChecker = GetComponentInChildren<InteractChecker>();
		grabSlow = 1;
		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
		playerAnimator = GetComponentInChildren<Animator>();
	}

	void Update() {
		CalculateVelocity ();
		HandleWallSliding ();

		if(controller.collisions.below) numOfJumps = MaxNumOfJumps;
		if(playerState != PlayerState.INTERACTING && playerState != PlayerState.ONROPE)
        {
			controller.Move (velocity * Time.deltaTime, directionalInput);
        }

		if (controller.collisions.above || controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope) {
				velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
			} else {
				velocity.y = 0;
			}
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


        if(directionalInput.x == 0)
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

	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
	}

	public void OnJumpInputDown() {
		if (wallSliding) {
			if (wallDirX == directionalInput.x) {
				velocity.x = -wallDirX * wallJumpClimb.x;
				velocity.y = wallJumpClimb.y;
			}
			else if (directionalInput.x == 0) {
				velocity.x = -wallDirX * wallJumpOff.x;
				velocity.y = wallJumpOff.y;
			}
			else {
				velocity.x = -wallDirX * wallLeap.x;
				velocity.y = wallLeap.y;
			}
		}
		if (controller.collisions.below || numOfJumps != 0) {
			if (controller.collisions.slidingDownMaxSlope) {
				if (directionalInput.x != -Mathf.Sign (controller.collisions.slopeNormal.x)) { // not jumping against max slope
					velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
					velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
				}
			} else {
				if(numOfJumps != 0) numOfJumps--;
				velocity.y = maxJumpVelocity;
			}
		}
	}

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}
		

	void HandleWallSliding() {
		wallDirX = (controller.collisions.left) ? -1 : 1;
		wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (directionalInput.x != wallDirX && directionalInput.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}
		}
	}

	void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
	}

	public void Die()
    {
        GameManager.ResetLevel();
    }

	public void SetPlayerState(PlayerState state)
    {
        playerState = state;
    }

    public PlayerState GetPlayerState()
    {
        return playerState;
    }
	    IEnumerator InteractCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        bCanInteract = true;
    }

    public void LaunchCharacter(Vector2 direction, bool overrideVelocity) {
        Vector3 newDir = new Vector3(direction.x / 2.5f, direction.y, 0) * maxJumpVelocity;
        velocity = overrideVelocity ? newDir : velocity + newDir;
    }
	public Vector3 getVelocity()
    {
        return velocity;
    }
}
