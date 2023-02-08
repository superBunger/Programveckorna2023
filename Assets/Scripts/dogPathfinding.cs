using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus;
using NavMeshPlus.Extensions;
using UnityEngine.AI;
using System.IO;

public class dogPathfinding : MonoBehaviour
{
    //Henry jobbade på hundens patrul och syn.
    //Max jobbade på hundens animation.
    public Transform Transform;
    NavMeshAgent agent;
    [SerializeField]
    public Transform[] waypoint;
    Transform newPosition;
    bool detected;
    public float waitTime;
    bool dogWalked;
    Animator animator;
    public enemyeyesight ees;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        StartCoroutine("DogWalk");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dogWalked == true)
        {
            //Startar om DogWalk om den har gjort klar en runda - Henry
            dogWalked = false;
            StartCoroutine("DogWalk");
        }

        if(agent.velocity.x > 0.1)
        {
            animator.SetBool("dogRight", true);
        }
        else
        {
            animator.SetBool("dogRight", false);
        }

        if (agent.velocity.x < 0.1)
        {
            animator.SetBool("dogLeft", true);
        }
        else
        {
            animator.SetBool("dogLeft", false);
        }

        if (agent.velocity.y > 0.1)
        {
            animator.SetBool("dogButt", true);
        }
        else
        {
            animator.SetBool("dogButt", false);
        }

        if (agent.velocity.y < 0.1)
        {
            animator.SetBool("dogForward", true);
        }
        else
        {
            animator.SetBool("dogForward", false); //spelar animationer baserat på vilket håll hunden rör sig - max
        }
    }

    IEnumerator DogWalk()
    {
        //Dogwalk säger till hunden att kolla rakt mot sin nästa waypoint och sen gå mot den. Den väntar för en specifierad tid innan den går till den nästa waypointen - Henry
        print("Dogwalk1 start");
        for (int i = 1; i < waypoint.Length; i++)
        {
            ees.gameObject.transform.up = waypoint[i].position - transform.position;
            agent.SetDestination(waypoint[i].position);
            yield return new WaitForSecondsRealtime(waitTime);
        }
        print("Second part of DogWalk1");
        for (int i = waypoint.Length - 2; i >= 0; i--)
        {
            ees.gameObject.transform.up = waypoint[i].position - transform.position;
            agent.SetDestination(waypoint[i].position);
            yield return new WaitForSecondsRealtime(waitTime);
        }
        dogWalked =true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Stannar hunden om den blir träffad av en EMP - Max
        if(collision.gameObject.tag == "emp")
        {
            StopAllCoroutines();
            agent.isStopped = true;
            StartCoroutine(disabledTimer());
        }
    }


    IEnumerator disabledTimer()
    {
        yield return new WaitForSeconds(3);
        agent.isStopped = false;
        StartCoroutine("DogWalk1");
       
    }

    
 
}
