using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energiSystem : MonoBehaviour
{
    public int energyBar = 0;
    public GameObject barOne;
    public GameObject barTwo;
    public GameObject barThree;
    public GameObject barFour;
    public GameObject speedIcon;

    public PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        barOne.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (energyBar >= 1)
        {
            barOne.SetActive(true);
            speedIcon.SetActive(true); //om man har nog med energi f�r att anv�nda den blir den if�rgad
        }
        else
        {
            barOne.SetActive(false);
            speedIcon.SetActive(false);
        }

        if (energyBar >= 2)
        {
            barTwo.SetActive(true);
        }
        else
        {
            barTwo.SetActive(false);
        }

        if (energyBar >= 3)
        {
            barThree.SetActive(true);
        }
        else
        {
            barThree.SetActive(false);
        }

        if (energyBar == 4)
        {
            barFour.SetActive(true);
        }
        else
        {
            barFour.SetActive(false); //allt deh�r �ndrar m�taren baserat p� hur mycket energi man har - max
        }

        if(energyBar > 4)
        {
            energyBar = 4;  //ser till att energi inte kan �ka �ver 4 - max
        }

       
    }
}

