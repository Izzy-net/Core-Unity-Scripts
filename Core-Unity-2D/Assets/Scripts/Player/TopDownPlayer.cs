using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownPlayer : MonoBehaviour, IDamageable
{
    [Header("General")]
    [SerializeField] float health = 30f;
    public bool playerDead = false;

    [Header("Movement")]
    Rigidbody2D myRigidbody;  //declare variable
    [SerializeField] float moveSpeed = 1f;    //we want to be able to alter moveSpeed from the inspector
    Vector2 moveInput; // declare variable

    [Header("Effects")]
    SpriteRenderer playerSprite;
    [SerializeField] float timeBetweenHitColorChanges;
    [SerializeField] Color32 defaultColor;
    [SerializeField] Color32 hitColor;
 
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();  //get rigidbody component from gameobject and set it as myRigidbody variable
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        Move();     //we want to update player motion every frame
    }

    private void Move()
    {
        myRigidbody.linearVelocity = moveInput * moveSpeed; //set the velocity of the player to the direction set by moveInput * speed
    }

    private void OnMove(InputValue inputValue)      //this is the function used by the unity engine player input system to handle move input
    {
        moveInput = inputValue.Get<Vector2>();      //here we access the vector2 description of the input value of the keyboard movement
                                                    //this will be (1,0) for right arrow, (-1,0) for left arrow etc
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        StartCoroutine(HitAnimation());

        if (health <= 0)
        {
            Die();
            Destroy(gameObject);
        }
    }

    private IEnumerator HitAnimation()
    {
        playerSprite.color = hitColor;
        yield return new WaitForSeconds(timeBetweenHitColorChanges);
        playerSprite.color = defaultColor;
    }

    private void Die()
    {
        playerDead = true;
    }
}
