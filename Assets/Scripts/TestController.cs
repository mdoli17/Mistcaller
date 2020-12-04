using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    // Start is called before the first frame update

    CharacterController ctrl;

    [SerializeField]
    private float moveSpeed;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        ctrl = GetComponent<CharacterController>();    
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Direction = new Vector2(Input.GetAxisRaw("Horizontal"),0);
        Vector2 transformDirection = transform.TransformDirection(Direction);
        Vector2 flatMovement =  moveSpeed * Time.deltaTime * transformDirection;   
        ctrl.SimpleMove(flatMovement);
        
    }


}
