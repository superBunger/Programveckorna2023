using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energiSystem : MonoBehaviour
{
    public bool hasKey = false;
    public int energyBar;

    public GameObject barOne;
    public GameObject barTwo;
    public GameObject barThree;
    public GameObject barFour;
    
    public GameObject speedIcon;
    public GameObject smokeIcon;
    public GameObject empIcon;
    public GameObject bombIcon;

    public GameObject keycardIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<LevelLoader>().gameOverAnimator.GetBool("isGameOver") == false)
        {
            if (energyBar >= 1)
            {
                barOne.SetActive(true);
                speedIcon.SetActive(true); //om man har nog med energi f�r att anv�nda den blir den if�rgad - max
            }
            else
            {
                barOne.SetActive(false);
                speedIcon.SetActive(false);
            }

            if (energyBar >= 2)
            {
                barTwo.SetActive(true);
                smokeIcon.SetActive(true);
            }
            else
            {
                barTwo.SetActive(false);
                smokeIcon.SetActive(false); //om man har nog med energi, if�rgad - max
            }

            if (energyBar >= 3)
            {
                barThree.SetActive(true);
                empIcon.SetActive(true);
            }
            else
            {
                barThree.SetActive(false);
                empIcon.SetActive(false);  //om man har nog med energi, if�rgad - max
            }

            if (energyBar == 4)
            {
                barFour.SetActive(true);
                bombIcon.SetActive(true);
            }
            else
            {
                barFour.SetActive(false); //allt deh�r �ndrar m�taren baserat p� hur mycket energi man har - max
                bombIcon.SetActive(false);
            }

            if (hasKey == true)
            {
                keycardIcon.SetActive(true);
            }
            else
            {
                keycardIcon.SetActive(false);
            }

            if (energyBar > 4)
            {
                energyBar = 4;  //ser till att energi inte kan �ka �ver 4 - max
            }
        }

       
    }
}

