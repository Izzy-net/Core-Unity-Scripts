using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IDamageable
{
    [Header("General")]
    [SerializeField] float health = 30f;

    [Header("Movement")]
    Rigidbody2D myRigidbody;  //declare variable
    CapsuleCollider2D myCollider;
    float playerHeight;
    [SerializeField] float moveSpeed = 1f;    //we want to be able to alter moveSpeed from the inspector
    [SerializeField] float jumpHeight = 1f;   //we want to be able to alter jumpHeight from the inspector
    Vector2 moveInput; // declare variable
    [SerializeField] LayerMask canJump;
    
    [Header("Shooting")]
    [SerializeField] GameObject bulletObject;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject bulletSpawnPoint;
 
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();  //get rigidbody component from gameobject and set it as myRigidbody variable
        myCollider = GetComponent<CapsuleCollider2D>();
        canJump = LayerMask.GetMask("Ground");
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
        if (Physics2D.Raycast(transform.position, Vector2.down,  playerHeight/2*1.02f, canJump))
        {
            myRigidbody.linearVelocityY = jumpHeight;   //here we just want a single instance of explosive velocity in the form of a jump
        }
    }

    private void OnFire()
    {
        Shoot();
    }

    private void Shoot()
    {
        Instantiate(bulletObject, bulletSpawnPoint.transform.position, gun.transform.rotation);
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
