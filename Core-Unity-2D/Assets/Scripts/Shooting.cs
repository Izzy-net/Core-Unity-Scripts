using System.Collections;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject gun;
    [SerializeField] GameObject bulletObject;
    GameObject player;
    [SerializeField] float baseFiringRate = 0.5f;
    [SerializeField] float minFiringRate = 0.2f;
    [SerializeField] float firingRateVariance = 0.2f;

    private void Awake() 
    {
        player = FindFirstObjectByType<PlayerMovement>().gameObject;
    }

    private void Start() 
    {
        if (gameObject.tag == "Target")
        {
            StartCoroutine(ShootContinuously());
        }
    }

    public void ShootOnce()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (!gameObject.TryGetComponent<PlayerMovement>(out var playerPresent))
        {
            //ALTERNATE METHOD TO WHAT IS BELOW
            //UnityEngine.Vector2 directionToTarget = player.transform.position - gun.transform.position;
            //float angle = UnityEngine.Vector3.Angle(UnityEngine.Vector3.right, directionToTarget);

            //if (player.transform.position.x < gun.transform.position.x)
            //{
                //angle *= -1;
            //}

            //UnityEngine.Quaternion bulletRotation = UnityEngine.Quaternion.AngleAxis(angle, UnityEngine.Vector3.forward);
            var angle = Mathf.Atan2(player.transform.position.y - gun.transform.position.y, player.transform.position.x - gun.transform.position.x) * Mathf.Rad2Deg;
            var bulletRotation = UnityEngine.Quaternion.Euler(0,0,angle);
            Instantiate(bulletObject, gun.transform.position, bulletRotation);
        }
        else
        {
            Instantiate(bulletObject, gun.transform.position, bulletObject.transform.rotation);
        }
    }

    private IEnumerator ShootContinuously()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(TimeBetweenShots());
        }
    }

    private float TimeBetweenShots()
    {
        float timeToNextShot = UnityEngine.Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
        return Mathf.Clamp(timeToNextShot, minFiringRate, float.MaxValue);
    }
}
