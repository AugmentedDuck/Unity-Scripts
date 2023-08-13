using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI ammoCounterText;

    //Just for updating the ammocounter on screen !WILL PROBABLY BE REPLACED!
    public void UpdateAmmoUI(int topInt, int bottomInt)
    {
        ammoCounterText.text = topInt.ToString() + "\n" + bottomInt.ToString();
    }
}
