using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] List<GameObject> pathPoints = new List<GameObject>();
    int listIndex = 0;
    Transform targetPoint;
    [SerializeField] float moveSpeed = 1f;

    void Start()
    {
        targetPoint = pathPoints[listIndex].transform;
    }

    void Update()
    {
        Move(targetPoint);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.5f)  //Mathf.Abs(transform.position.x - targetPoint.position.x) < 0.5f
        {
            listIndex++;
            if (listIndex >= pathPoints.Count)
            {
                listIndex = 0;
            }
            targetPoint = pathPoints[listIndex].transform;
        }
    }

    void Move(Transform targetPoint)
    {
        float speed = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed);
    }

    private void OnDrawGizmos() 
    {
        for (int i = 0; i < pathPoints.Count; i++)
        {
            Gizmos.DrawWireSphere(pathPoints[i].transform.position, 0.5f);
            if (i > 0)
            {
                Gizmos.DrawLine(pathPoints[i].transform.position, pathPoints[i-1].transform.position);
            }
        }       
    }
}
