using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1f;
    [SerializeField] float bulletDamage = 10f;
    Rigidbody2D myRigidbody;
    [SerializeField] LayerMask canDestroyBullet;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start() 
    {
        SetVelocity();
        SetDestroy();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((canDestroyBullet.value & (1<< collider.gameObject.layer)) > 0)
        {
            //vfx
            
            //sfx

            //screenshake

            //damage enemy and/or player
            if (collider.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.Damage(bulletDamage);
            }

            //destroy bullet
            Destroy(gameObject);
        }
    }
    
    private void SetVelocity()
    {
        myRigidbody.linearVelocity = transform.right * bulletSpeed;
    }

    private void SetDestroy()
    {
        Destroy(gameObject, 4);
    }
}
