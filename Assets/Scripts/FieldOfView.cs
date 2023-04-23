using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius = 5f;
    [Range(1, 360)] public float angle = 45f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;

    public GameObject player;
    public GameObject juggernautObject;
    pathfinding juggernaut;

    public bool CanSeePlayer { get; private set; }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVCheck());
        juggernautObject = GameObject.FindGameObjectWithTag("Juggernaut");
        juggernaut = juggernautObject.GetComponent<pathfinding>();
    }

    void Update()
    {
        
    }

    void FOV()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if(rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            
            if(Vector2.Angle(transform.up, directionToTarget) < angle * 0.5)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if(!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                {
                    CanSeePlayer = true;
                    juggernaut.othersSee = 1;
                }
                else
                {
                    CanSeePlayer = false;
                    juggernaut.othersSee = 0;
                }
            }
            else
            {   
                CanSeePlayer = false;
                juggernaut.othersSee = 0;
            }
        } 
        else if(CanSeePlayer)
        {
            CanSeePlayer = false;
            juggernaut.othersSee = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -angle * 0.5f);
        Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, angle * 0.5f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        if(CanSeePlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while(true)
        {
            yield return wait;
            FOV();
        }
    }
}
