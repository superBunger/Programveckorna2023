using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus;
using NavMeshPlus.Extensions;
using UnityEngine.AI;
using System.IO;

public class dogPathfinding : MonoBehaviour
{
    public Transform Transform;
    NavMeshAgent agent;
    [SerializeField]
    public Transform[] waypoint;
    Transform newPosition;
    bool detected;
    public float waitTime;
    bool dogWalked;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        StartCoroutine("DogWalk1");
    }

    // Update is called once per frame
    void Update()
    {
        if (dogWalked == true)
        {
            dogWalked = false;
            StartCoroutine("DogWalk2");
        }
    }

    IEnumerator DogWalk1()
    {
        for (int i = 0; i < waypoint.Length; i++)
        {
            print("Start");
            transform.up = waypoint[i].position - transform.position;
            agent.SetDestination(waypoint[i].position);
            yield return new WaitForSecondsRealtime(waitTime);
        }    
        dogWalked=true;
    }

    IEnumerator DogWalk2()
    {
        for (int i = waypoint.Length - 2; i >= 0; i--)
        {
            print("Start");
            transform.up = waypoint[i].position - transform.position;
            agent.SetDestination(waypoint[i].position);
            yield return new WaitForSecondsRealtime(waitTime);
        }

        for (int i = 1; i < waypoint.Length; i++)
        {
            print("Start");
            transform.up = waypoint[i].position - transform.position;
            agent.SetDestination(waypoint[i].position);
            yield return new WaitForSecondsRealtime(waitTime);
        }
        dogWalked=true;
    }
}
