using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creepyLittleHead : MonoBehaviour
{
    public int headNumber;
    public rotatingVision body;
    Animator animator;
    
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
      if(body.gameObject.transform.rotation.z >= -20 && body.gameObject.transform.rotation.z < 25)
      {
            animator.SetInteger("owlStage", 0);
      }

        if (body.gameObject.transform.rotation.z >= 25 && body.gameObject.transform.rotation.z < 70)
        {
            animator.SetInteger("owlStage", 1);
        }

        if (body.gameObject.transform.rotation.z >= 70 && body.gameObject.transform.rotation.z < 115)
        {
            animator.SetInteger("owlStage", 2);
        }
        
        if (body.gameObject.transform.rotation.z >= 115 && body.gameObject.transform.rotation.z < 160)
        {
            animator.SetInteger("owlStage", 3);
        }

        if (body.gameObject.transform.rotation.z >= 160 && body.gameObject.transform.rotation.z < 205)
        {
            animator.SetInteger("owlStage", 4);
        }

        if (body.gameObject.transform.rotation.z >= 205 && body.gameObject.transform.rotation.z < 250)
        {
            animator.SetInteger("owlStage", 5);
        }

        if (body.gameObject.transform.rotation.z >= 295 && body.gameObject.transform.rotation.z < 340)
        {
            animator.SetInteger("owlStage", 6);
        }
    }
}
