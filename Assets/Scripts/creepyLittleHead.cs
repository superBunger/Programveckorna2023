using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creepyLittleHead : MonoBehaviour
{
   
    Animator animator;
 
    
    
    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
       
        animator.SetFloat("owlRotation", gameObject.transform.localRotation.eulerAngles.z); //s�ger till animatorn hur mycket ugglan har roterat, animatorn �ndrar sprite baserat p� det - max
        
    }
}
