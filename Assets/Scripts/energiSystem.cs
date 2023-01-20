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
    public GameObject smokeIcon;
    public GameObject empIcon;
    public GameObject bombIcon;

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
            bombIcon.SetActive(false); //om man har nog med energi, if�rgad - max
        }

        if(energyBar > 4)
        {
            energyBar = 4;  //ser till att energi inte kan �ka �ver 4 - max
        }

       
    }
}

