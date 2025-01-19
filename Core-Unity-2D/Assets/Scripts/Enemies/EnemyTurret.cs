using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [SerializeField] GameObject gun;
    [SerializeField] GameObject bulletObject;
    [SerializeField] GameObject bulletSpawnPoint;
    GameObject player;
    [SerializeField] float baseFiringRate = 0.5f;
    [SerializeField] float minFiringRate = 0.2f;
    [SerializeField] float firingRateVariance = 0.2f;
    
    private void Awake()
    {
        player = FindFirstObjectByType<PlayerMovement>().gameObject;
    }
    void Start()
    {
        StartCoroutine(ShootContinuously());
    }

    void Update()
    {
        GunFollow();
    }


    private void GunFollow()
    {
        UnityEngine.Vector2 directionToTarget = (player.transform.position - gun.transform.position).normalized;
        gun.transform.right = directionToTarget;

        //DIFFERENT METHOD BELOW::
        //var angle = Mathf.Atan2(player.transform.position.y - gun.transform.position.y, player.transform.position.x - gun.transform.position.x) * Mathf.Rad2Deg;
        //gun.transform.localRotation = Quaternion.Euler(0,0,angle);
    }

    private IEnumerator ShootContinuously()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(TimeBetweenShots());
        }
    }

    private void Shoot()
    {
        Instantiate(bulletObject, bulletSpawnPoint.transform.position, gun.transform.rotation);
    }

    private float TimeBetweenShots()
    {
        float timeToNextShot = UnityEngine.Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
        return Mathf.Clamp(timeToNextShot, minFiringRate, float.MaxValue);
    }

}
