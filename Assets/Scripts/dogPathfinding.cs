using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer; 

public class dogPathfinding : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField]
    public Transform[] waypoint;
    bool detected;
    bool reset = false;
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
        agent.updateRotation = false; 
        agent.updateUpAxis = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        print(waypoint.Length);
        var agentPosition = agent.transform.position;
        StartCoroutine("dogWalk");
    }

    // Update is called once per frame
    void Update()
    {
        if (reset = true)
        {
            StartCoroutine("dogWalk");
        }
 
    }

    IEnumerator dogWalk(){ 
    for (int i=0; i<waypoint.Length; i++)
           {
                transform.up = waypoint[i].position - transform.position;
                while (agent.transform.position != waypoint[i].position)
                agent.SetDestination(waypoint[i].position);
           }
        reset = true;
        yield return null;
    }
}
