using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardDog : MonoBehaviour
{
    public float speed;
    public float changeTime = 3.0f;
    public bool up;

    Rigidbody rigidBody;
    float timer;
    int direction = 1;

    public NavMeshAgent guardDog;
    public Transform player;
    public Transform resetPoint;
    public GameObject startPoint;
    public GameObject endPoint;

    public bool seen;

    public PlayerDogScript playerDog;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        timer = changeTime;
        seen = false;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    

    private void FixedUpdate()
    {
       Vector3 position = rigidBody.position;

        if (up)
        {
            position.z = position.z + Time.deltaTime * speed * -direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rigidBody.MovePosition(position);
    }
    

    private void OnTriggerEnter(Collider other)
    {
        PlayerDogScript controller = other.GetComponent<PlayerDogScript>();
        seen = true;

        if (seen == true)
        {
            LateUpdate();
        }

        else
        {
            guardDog.SetDestination(resetPoint.position);
            FixedUpdate();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        PlayerDogScript controller = other.GetComponent<PlayerDogScript>();
        seen = false;
        /*
        if (seen == false)
        {
            guardDog.SetDestination(resetPoint.position);
            FixedUpdate();
        }
        */
    }

    private void LateUpdate()
    {
        guardDog.SetDestination(player.position);
    }
}
