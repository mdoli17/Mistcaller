using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float JumpForce;

    public float WaterPower;

    public float RopeSwing;

    // Movement

    public float Acceleration;

    [Range(0,20)]
    public float MaxSpeed;

    public float FrictionCoefficient;

    public int MaxMistyJumps;

    public Animator animator;

    public GameObject SmokeObject;

    public float ropeSpeed;

    int CurrentMistyJumps;

    bool bIsOnGround;

    bool bIsOnRope;

    bool bIsInWater;

    bool bCanSpawnSmoke;
    GameObject HookStartObject;

    [System.NonSerialized]
    public bool keyPicked;
    
    /* Components */

    [System.NonSerialized]
    public Rigidbody2D MistyBody;

    AbilityManager abilityManager;

    MistCheck mistChecker;

    // Start is called before the first frame update
    void Start()
    {
        MistyBody = GetComponent<Rigidbody2D>();
        ResetMistyJump();
        mistChecker = GetComponentInChildren<MistCheck>();
        abilityManager = GetComponent<AbilityManager>();
        bCanSpawnSmoke = false;
        keyPicked = false;
        bIsOnRope = false;
        bIsInWater = false;
    }

    private void FixedUpdate()
    {
        if(bIsOnGround)
        {
            MistyBody.gravityScale = 1;
        }
        else
        {
            MistyBody.gravityScale = 3;
        }
        if(bIsOnRope)
        {
            float moveValue = Input.GetAxisRaw("Vertical");
            if(moveValue > 0)
            {
                transform.position += new Vector3(0,ropeSpeed,0);
            }
            else if(moveValue < 0)
            {
                transform.position -= new Vector3(0,ropeSpeed,0);
            }
        }    
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1"))
        {

            abilityManager.currentAbility = AbilityManager.AvailableAbilities.Hook;
        }
        
        if(Input.GetKeyDown("2"))
        {
            abilityManager.currentAbility = AbilityManager.AvailableAbilities.Smoke;
        }


        Vector2 Force = Vector2.zero;

        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            if(bIsOnGround) 
                Force = new Vector2(Input.GetAxisRaw("Horizontal"),0f) * Acceleration;
            else if(bIsOnRope)
            {
                Force = Vector2.zero;

                RopeCheck ropeCheck = GetComponentInChildren<RopeCheck>();

                if(ropeCheck)
                {
                    Collider2D[] ropes = ropeCheck.getRopes();
                 
                    transform.position = new Vector3(ropes[0].transform.position.x, ropes[0].transform.position.y, transform.position.z);

                    foreach(var rope in ropes)
                    {
                        Debug.Log(rope);
                        rope.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(RopeSwing * Input.GetAxisRaw("Horizontal"),0));
                    }
                }
            
            }
            else
                Force = new Vector2(Input.GetAxisRaw("Horizontal") * 0.5f,0f) * Acceleration;
            
            Vector2 Friction = new Vector2(-MistyBody.velocity.x * FrictionCoefficient, 0);
            
            if(MistyBody.velocity.x > MaxSpeed || MistyBody.velocity.x < -MaxSpeed)
            {
                Force += Friction;
            }
        }

        HandleCheck handle = GetComponentInChildren<HandleCheck>();
        if(Input.GetKey("left ctrl"))
        {
            if(handle)
            {
                if(handle.CanReachHandle())
                {
                    Force /= 3.5f;
                    handle.PushObject(Force.magnitude * Input.GetAxisRaw("Horizontal"));
                }
            }
        }
        if(Input.GetKeyDown("left ctrl"))
        {
            if(handle)
            {
                if(handle.CanReachHandle())
                {
                    
                }
            }
        }
        if(Input.GetKeyUp("left ctrl"))
        {
            if(handle)
            {
                if(handle.CanReachHandle())
                {
                    
                }
            }
        }

        MistyBody.AddForce(Force);

        if(true)
        {
            Vector3 characterScale = transform.localScale;
            if(Input.GetAxisRaw("Horizontal") < 0)
                characterScale.x = Mathf.Abs(transform.localScale.x) * -1;
            else if(Input.GetAxisRaw("Horizontal") > 0)
                characterScale.x = Mathf.Abs(transform.localScale.x);

            transform.localScale = characterScale;
        }

        animator.SetFloat("Player Speed", Mathf.Abs(MistyBody.velocity.x));

        if(Input.GetKeyDown("space"))
        {
            if(bIsOnGround)
            {
                JumpFromGround();
                animator.SetBool("bIsJumping", true);
            }
            else if(bIsOnRope)
            {
                JumpFromRope();
            }
            else if(!bIsInWater)
                Abilities.MistyJump(this);
        }


        if(Input.GetKey("space"))
        {
            if (bIsInWater)
            {
                MistyBody.AddForce(WaterPower * Vector2.up);
            }
        }

        // Breaking Stuff
        if(Input.GetKeyDown("q"))
        {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 MousePosition = new Vector2(pz.x, pz.y);

            int layerMask = LayerMask.GetMask("Breakable");

            RaycastHit2D hit2D = Physics2D.Raycast(MousePosition, Vector2.zero, Mathf.Infinity, layerMask);

            if(hit2D.collider)
            {
                GameObject obj = hit2D.collider.gameObject;
                if(obj.layer == LayerMask.NameToLayer("Breakable"))
                {
                    obj.GetComponent<BreakableObject>().Break();
                }
            }
        }

        if(Input.GetButtonDown("Fire1"))
        {
            if(abilityManager.currentAbility == AbilityManager.AvailableAbilities.Hook)
            {
                HookStartObject = Abilities.Hook();
            }         
            else if(abilityManager.currentAbility == AbilityManager.AvailableAbilities.Smoke)
            {
                bCanSpawnSmoke = Abilities.CanDetectMist();
            }
        }

        if(Input.GetButtonUp("Fire1"))
        {
            if(abilityManager.currentAbility == AbilityManager.AvailableAbilities.Hook)
            {
                if(HookStartObject)
                {
                    Abilities.EndHook(HookStartObject);
                }
            }
            else if(abilityManager.currentAbility == AbilityManager.AvailableAbilities.Smoke)
            {
                if(bCanSpawnSmoke)
                {
                    Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 MousePosition = new Vector2(pz.x, pz.y);
    
                    RaycastHit2D hit2D = Physics2D.Raycast(MousePosition, Vector2.zero);

                    GameObject temp = Instantiate(SmokeObject, Camera.main.ScreenToWorldPoint(Input.mousePosition),Quaternion.identity);
                    SmokeScript tmpscript = temp.GetComponent<SmokeScript>();

                    GameObject obj = null;

                    if(hit2D.collider)
                    {
                        if(hit2D.collider.gameObject.layer == LayerMask.NameToLayer("Light"))
                        {
                            obj = hit2D.collider.gameObject.transform.parent.gameObject;
                        }
                    }
                    tmpscript.doStuff(obj);
                }
                bCanSpawnSmoke = false;
            }
        }

        if(MistyBody.velocity.y < -3f && !bIsOnGround)
        {
            
            animator.SetBool("bIsJumping", false);
            animator.SetBool("bIsFalling", true);
        }
        

        Debug.DrawRay(transform.position, Vector3.down, Color.red,0.0f);

    }

    void JumpFromRope()
    {
        MistyBody.isKinematic = false;
        MistyBody.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") / 2, 1) * JumpForce); 
    }

    void JumpFromGround()
    {
        MistyBody.AddForce(new Vector2(0,1) * JumpForce);
    }

    public void HasLanded()
    {
        bIsOnGround = true;
        
        animator.SetBool("bIsFalling", false);
        ResetMistyJump();
    }

    public void NotOnGround()
    {
        bIsOnGround = false;
    }

    void ResetMistyJump()
    {
        CurrentMistyJumps = MaxMistyJumps;
    }

    public void UseMistyJump()
    {
        CurrentMistyJumps--;
    }
    public bool MistyJumpsAvailable()
    {
        return CurrentMistyJumps > 0;
    }

    public bool bCanDetectMist()
    {
        return mistChecker.MistList.Count > 0;
    }

    public void RopeDetected()
    {
        bIsOnRope = true;
        MistyBody.isKinematic = true;
        MistyBody.velocity = new Vector3(0,0,0);
        GetComponent<Collider2D>().enabled = false;
    }

    public void RopeLeft()
    {
        bIsOnRope = false;
        MistyBody.isKinematic = false;
    }

    public void inWater()
    {
        bIsInWater = true;
    }

    public void outOfWater()
    {
        bIsInWater = false;
    }

    public bool OnGround()
    {
        return bIsOnGround;
    }
}
