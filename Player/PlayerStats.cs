using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IDataPersistance
{
    [Header("Health")]
    public float maxHealth;
    public float health;
    public bool isDead = false;

    [Header("Stamina")]
    public float maxStamina;
    public float stamina;
    [SerializeField] float staminaRegenerationRate;
    public bool hasStamina = true;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Slider staminaSlider;

    public void LoadData(GameData data)
    {
        this.health = data.healthPoints;
    }

    public void SaveData(GameData data)
    {
        data.healthPoints = this.health;
    }

    public void DamageHealth(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            UpdateUI();
        }

        if (health <= 0)
        {
            isDead = true;
            UpdateUI();
        }
    }

    public void HealHealth(float heal)
    {
        if (health + heal > maxHealth)
        {
            health = maxHealth;
            UpdateUI();
        }
        else
        {
            health += heal;
            UpdateUI();
        }
    }

    public void DrainStamina(float drainRate)
    {
        if (stamina - drainRate > 0 && hasStamina)
        {
            stamina -= drainRate * Time.deltaTime;
            UpdateUI();
        }
        else
        {
            stamina = 0;
            hasStamina = false;
            UpdateUI();
        }
    }

    public void RegenerateStamina()
    {
        if (stamina <= maxStamina)
        {
            if (hasStamina)
            {
                stamina += staminaRegenerationRate * Time.deltaTime;
                UpdateUI();
            }
            else
            {
                if (stamina != maxStamina)
                {
                    stamina += staminaRegenerationRate * Time.deltaTime;
                    UpdateUI();
                }
                else
                {
                    stamina = maxStamina;
                    hasStamina = true;
                    UpdateUI();
                }
            }
        }
    }

    void UpdateUI()
    {
        healthText.text = "HP: " + health / maxHealth * 100 + " %";
        staminaSlider.value = stamina / maxStamina;
    }
}
