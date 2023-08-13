using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    private string[] paragraphs = new string[] { "(All values are rounded)\r\nThis bar is 100m (328ft) long:", 
                                                 "Average sailboat:\r\n16km/t (10mph)\r\n250s to travel across" + "\r\n" +
                                                 "Average human running:\r\n29km/t (18mph)\r\n125s to travel across",
                                                 "Average cyclist:\r\n32km/t (20mph)\r\n111s to travel across",
                                                 "Fastest human (Usain Bolt):\r\n38km/t (24mph)\r\n91s to travel across",
                                                 "Average pro cyclist:\r\n47km/t (29mph)\r\n77s to travel across" + "\r\n" +
                                                 "Danish Metro + Average speedboat:\r\n80km/t (50mph)\r\n45s to travel across",
                                                 "S-Toget (Danish train) + Fastest sailboat (Vestas Sailrocket 2):\r\n120km/t (75mph)\r\n30s to travel across" + "\r\n" + 
                                                 "Freeway speeds:\r\n130km/t (81mph)\r\n28s to travel across",
                                                 "Fastest bicycle (Unpaced : No vehicle in front):\r\n144km/t (89mph)\r\n25s to travel across",
                                                 "Average car top speed:\r\n193km/t (120mph)\r\n19s to travel across",
                                                 "Average helicopter:\r\n260km/t (162mph)\r\n14s to travel across" + "\r\n" +
                                                 "Fastest bicycle (Paced : Motorized vehicle in front):\r\n296km/t (184mph)\r\n12s to travel across",
                                                 "F1 car (Race top speed):\r\n360km/t (224mph)\r\n10s to travel across",
                                                 "F1 car (Top speed):\r\n397km/t (247mph)\r\n9s to travel across",
                                                 "Fastest Helicopter (Silkorsky X2) + Fastest train (Shanghai Maglev):\r\n460km/t (286mph)\r\n8s to travel across" + "\r\n" + 
                                                 "Fastest boat (Spirit of Australia):\r\n510km/t (317mph)\r\n7s to travel across" + "\r\n" +
                                                 "Fastest car (Koenigsegg Jesko Absolut):\r\n531km/t (330mph)\r\n7s to travel across",
                                                 "Boeing 737 (Airliner):\r\n946km/t (588mph)\r\n4s to travel across",
                                                 "Landspeed record (ThrustSSC):\r\n1228km/t (763mph)\r\n3s to travel across" + "\r\n" + 
                                                 "Speed of sound:\r\n1235km/t (767mph)\r\n3s to travel across",
                                                 "F35 (Jet):\r\n1960km/t (1218mph)\r\n2s to travel across",
                                                 "SR-71 Blackbird:\r\n3920km/t (2436mph)\r\n1s to travel across",
                                                 "X-15:\r\n7274km/t (4520mph)\r\n0.5s to travel across"};
    private Animator[] anims = new Animator[26];

    public int index = 0;
    public int actionIndex = 0;
    public bool isNextAction = false;
    public TextMeshProUGUI textbox;
    public GameObject[] animationObjects;


    private void Awake()
    {
        for (int i = 0; i < animationObjects.Length; i++)
        {
            anims[i] = animationObjects[i].GetComponent<Animator>();
            anims[i].enabled = false;

            animationObjects[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        textbox.text = paragraphs[index];
    }

    public void PressedNext()
    {
        if (!isNextAction)
        {
            index++;
            textbox.text = paragraphs[index];
            isNextAction = true;
        } 
        else
        {
            NextAction();
            isNextAction = false;
        }
    }

    void NextAction()
    {
        if (actionIndex == 0)
        {
            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;

            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;
        }
        else if (actionIndex == 4)
        {
            anims[actionIndex - 1].enabled = false;
            anims[actionIndex - 2].enabled = false;
            anims[actionIndex - 3].enabled = false;

            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;

            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;

            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;
        }
        else if (actionIndex == 6)
        {
            anims[actionIndex - 1].enabled = false;
            anims[actionIndex - 2].enabled = false;
            anims[actionIndex - 3].enabled = false;

            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;

            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;

            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;
        }
        else if (actionIndex == 10)
        {
            anims[actionIndex - 1].enabled = false;
            anims[actionIndex - 2].enabled = false;
            anims[actionIndex - 3].enabled = false;

            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;

            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;
        }
        else if (actionIndex == 14)
        {
            anims[actionIndex - 1].enabled = false;
            anims[actionIndex - 2].enabled = false;
            anims[actionIndex - 3].enabled = false;

            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;

            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;

            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;

            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;
        }
        else if (actionIndex == 18)
        {
            anims[actionIndex - 1].enabled = false;
            anims[actionIndex - 2].enabled = false;
            anims[actionIndex - 3].enabled = false;
            anims[actionIndex - 4].enabled = false;


            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;

            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;
        }
        else
        {
            anims[actionIndex - 1].enabled = false;

            if (actionIndex >= 3)
            {
                anims[actionIndex - 2].enabled = false;
                anims[actionIndex - 3].enabled = false;
            } else if (actionIndex == 2)
            {
                anims[actionIndex - 2].enabled = false;
            }

            anims[actionIndex].enabled = true;
            animationObjects[actionIndex].SetActive(true);
            actionIndex++;

        }
    }
}
