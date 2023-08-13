using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponSwitching : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform[] weapons;

    [Header("Key Binds")]
    [SerializeField] KeyCode[] keys;

    [Header("Settings")]
    [SerializeField] float switchTime;

    private int selectedWeapon;
    private float timeSinceLastSwitch;

    private void Start()
    {
        SetWeapons();
        Select(selectedWeapon);

        timeSinceLastSwitch = 0f;
    }

    private void SetWeapons()
    {
        weapons = new Transform[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            weapons[i] = transform.GetChild(i);
        }

        if (keys == null)
        {
            keys = new KeyCode[0];
        }
    }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        //For selecting with "1", "2", "3" and so on
        for(int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch >= switchTime) 
            {
                selectedWeapon = i;
            }
        }

        //For selecting by scrolling mouse wheel
        if(Input.mouseScrollDelta.y > 0 && timeSinceLastSwitch >= switchTime)
        {
            if (selectedWeapon == weapons.Length - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        else if (Input.mouseScrollDelta.y < 0 && timeSinceLastSwitch >= switchTime)
        {
            if (selectedWeapon == 0)
            {
                selectedWeapon = weapons.Length - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            Select(selectedWeapon);
        }

        timeSinceLastSwitch += Time.deltaTime;
    }

    //The selected weapon is the only active
    private void Select(int weaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }

        timeSinceLastSwitch = 0f;

        OnWeaponSelected();
    }

    private void OnWeaponSelected()
    {
        //Debug.Log("New weapon selected");
    }
}
