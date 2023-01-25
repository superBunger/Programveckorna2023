using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creepyLittleHead : MonoBehaviour
{
    public int headNumber;
    Animator animator;
    public Transform GameObject;
    
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("owlRotation", gameObject.transform.localRotation.eulerAngles.z);
        if (gameObject.transform.localRotation.eulerAngles.z >= -20 && gameObject.transform.localRotation.eulerAngles.z < 25)
        {
            animator.SetInteger("owlStage", 0);
        }

        if (gameObject.transform.localRotation.eulerAngles.z >= 25 && gameObject.transform.localRotation.eulerAngles.z < 70)
        {
            animator.SetInteger("owlStage", 1);
        }

        if (gameObject.transform.localRotation.eulerAngles.z >= 70 && gameObject.transform.localRotation.eulerAngles.z < 115)
        {
            animator.SetInteger("owlStage", 2);
        }

        if (gameObject.transform.localRotation.eulerAngles.z >= 115 && gameObject.transform.localRotation.eulerAngles.z < 160)
        {
            animator.SetInteger("owlStage", 3);
        }

        if (gameObject.transform.localRotation.eulerAngles.z >= 160 && gameObject.transform.localRotation.eulerAngles.z < 205)
        {
            animator.SetInteger("owlStage", 4);
        }

        if (gameObject.transform.localRotation.eulerAngles.z >= 205 && gameObject.transform.localRotation.eulerAngles.z < 250)
        {
            animator.SetInteger("owlStage", 5);
        }

        if (gameObject.transform.localRotation.eulerAngles.z >= 295 && gameObject.transform.localRotation.eulerAngles.z < 340)
        {
            animator.SetInteger("owlStage", 6);
        }


    }
}
