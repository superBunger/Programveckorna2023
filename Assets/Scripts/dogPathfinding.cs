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
    public int waitTime;
    bool dogWalked;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        StartCoroutine("DogWalk");
    }

    // Update is called once per frame
    void Update()
    {
        if (dogWalked)
        {
            StartCoroutine("DogWalk");
        }
    }

    IEnumerator DogWalk()
    {
        for (int i = 0; i < waypoint.Length; i++)
        {
            transform.up = waypoint[i].position - transform.position;
            agent.SetDestination(waypoint[i].position);
            yield return new WaitForSecondsRealtime(waitTime);
        }
        dogWalked = true;
        yield return null;
    }
}
