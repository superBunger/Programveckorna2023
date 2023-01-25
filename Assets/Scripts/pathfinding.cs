using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class pathfinding : MonoBehaviour
{
    //Henry jobbade på Juggernautens pathfinding, att den förstör vägen, fångar spelaren etc.
    //Erik la in audio.
    //Max jobbade på animation för juggernauten.
    public bool activated = false;
    public int othersSee = 0;
    [SerializeField]
    Transform playerTransform;
    NavMeshAgent agent;
    GameObject Player;
    Animator animator;

    bool isScreaming = false;

    private void Start () 
    {
        //Henry
        othersSee = 0;
        agent = GetComponent<NavMeshAgent>();
        
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        
        if (othersSee >= 1)
        {
           //Henry 
            if(isScreaming == false)
            {
                //Erik
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

        //Allt detta nedanför är Max
        if (agent.velocity.x > 0)
        {
            animator.SetBool("juggerRight", true);
        }
        else
        {
            animator.SetBool("juggerRight", false);
        }

        if (agent.velocity.x < 0)
        {
            animator.SetBool("juggerLeft", true);
        }
        else
        {
            animator.SetBool("juggerLeft", false);
        }

        if (agent.velocity.y > 0)
        {
            animator.SetBool("juggerButt", true);
        }
        else
        {
            animator.SetBool("juggerButt", false);
        }

        if (agent.velocity.y < 0)
        {
            animator.SetBool("juggerForward", true);
        }
        else
        {
            animator.SetBool("juggerForward", false); //spelar animationer baserat på vilket håll juggernauten rör sig - max
        }


    }
}
