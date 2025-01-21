using Mono.Cecil.Cil;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] float damageDealt;
    [SerializeField] float moveSpeed;
    Rigidbody2D enemyRigidbody;
    PolygonCollider2D enemyCollider;
    TopDownPlayer player;

    private void Awake() 
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<PolygonCollider2D>();
        player = FindFirstObjectByType<TopDownPlayer>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        if (!player.playerDead)
        {
            Follow();
            MoveInDirection();
        }
        else
        {
            enemyRigidbody.linearVelocity = new Vector2 (0f,0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.Damage(damageDealt);
        }
    }

    private void Follow()
    {
        var direction = (player.gameObject.transform.position - transform.position).normalized;
        transform.right = direction;
    }

    private void MoveInDirection()
    {
        enemyRigidbody.linearVelocity = transform.right * moveSpeed;
    }
}
