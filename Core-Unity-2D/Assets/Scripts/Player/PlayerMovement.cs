using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D myRigidbody;  //declare variable
    CapsuleCollider2D myCollider;
    float playerHeight;
    [SerializeField] float moveSpeed = 1f;    //we want to be able to alter moveSpeed from the inspector
    [SerializeField] float jumpHeight = 1f;   //we want to be able to alter jumpHeight from the inspector
    Vector2 moveInput; // declare variable
    [SerializeField] LayerMask layerMask;
    Shooting shootingScript;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();  //get rigidbody component from gameobject and set it as myRigidbody variable
        myCollider = GetComponent<CapsuleCollider2D>();
        shootingScript = GetComponent<Shooting>();
        layerMask = LayerMask.GetMask("Ground");
    }

    private void Start() 
    {
        playerHeight = myCollider.bounds.size.y;
    }

    void Update()
    {
        Move();     //we want to update player motion every frame
    }

    private void Move()
    {
        myRigidbody.linearVelocityX = moveInput.x * moveSpeed; //set the velocity of the player to the direction set by moveInput * speed
    }

    private void OnMove(InputValue inputValue)      //this is the function used by the unity engine player input system to handle move input
    {
        moveInput = inputValue.Get<Vector2>();      //here we access the vector2 description of the input value of the keyboard movement
                                                    //this will be (1,0) for right arrow, (-1,0) for left arrow etc
    }

    private void OnJump()                           //this is the function used by the unity engine player input system to handle jump input
    {                                             //it will execute whenever the system detects the input for the associated action
        if (Physics2D.Raycast(transform.position, Vector2.down,  playerHeight/2*1.02f, layerMask))
        {
            myRigidbody.linearVelocityY = jumpHeight;   //here we just want a single instance of explosive velocity in the form of a jump
        }
    }

    private void OnFire()
    {
        shootingScript.ShootOnce();
    }
}
