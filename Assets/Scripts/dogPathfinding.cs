using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus;

public class dogPathfinding : MonoBehaviour
{
    
    [SerializeField]
    public Transform[] waypoint;
    bool detected;
    // Start is called before the first frame update
    void Start()
    {
        print(waypoint.Length);
    }

    // Update is called once per frame
    void Update()
    {
        while (detected) {
         for (int i=0; i < waypoint.Length; i++)
            {
             
            }
        } 
    }
}
