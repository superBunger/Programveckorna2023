using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlesystemscript : MonoBehaviour
{
    public bool insideSmoke = false;
    private void OnParticleCollision(GameObject other)
    {
       if(other.tag == "Player")
       {
            print("i see enemy awesome");
            insideSmoke = true;
       }
       
  
    }

    private void LateUpdate()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
