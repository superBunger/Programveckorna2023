using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class pathfinding : MonoBehaviour
{
    public int othersSee = 0;
    [SerializeField]
    Transform player;
    NavMeshAgent agent;
    private void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Update()
    {
        if (othersSee == 1)
        {
            agent.SetDestination(player.position);
        }
    }    
}
