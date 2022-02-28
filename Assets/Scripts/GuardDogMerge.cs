using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardDogMerge : MonoBehaviour
{

    public PlayerDogScript playerController;

    public float lookRadius = 12f;

    Transform target;
    NavMeshAgent agent;

    public Transform[] waypoints;
    public int speed;
    private int waypointIndex;
    private float distance;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }

        distance = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (distance < 1f)
        {
            IncreaseIndex();
        }
        Patrol();
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotaion = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotaion, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }

        transform.LookAt(waypoints[waypointIndex].position);
    }

    void OnCollisionEnter(Collision other)
    {
        PlayerDogScript player = other.gameObject.GetComponent<PlayerDogScript>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
