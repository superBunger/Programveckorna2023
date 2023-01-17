using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class pathfinding : MonoBehaviour
{
    public bool activated = false;
    public int othersSee = 0;
    [SerializeField]
    Transform playerTransform;
    NavMeshAgent agent;
    GameObject Player;

    bool isScreaming = false;

    private void Start () 
    {
        othersSee = 0;
        agent = GetComponent<NavMeshAgent>();
        
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Update()
    {
        if (othersSee >= 1)
        {
            if(isScreaming == false)
            {
                FindObjectOfType<AudioManager>().Play("JuggernautSummon");
                isScreaming = true;
            }
            activated = true;
            agent.SetDestination(playerTransform.position);
        }
        else
        {
            isScreaming = false;
        }
    }
}
