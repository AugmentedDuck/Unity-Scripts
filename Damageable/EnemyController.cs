using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable, IDataPersistance
{
    [SerializeField] private string id; //DO NOT CHANGE

    [ContextMenu("Generate GUID for ID")]
    private void GenerateGUID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    [SerializeField] float health = 100f;

    private void Start()
    {
        gameObject.SetActive(true);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void Update()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void LoadData(GameData data)
    {
        this.health = data.targetsHealth[id];
    }

    public void SaveData(GameData data)
    {
        if(data.targetsHealth.ContainsKey(id))
        {
            data.targetsHealth.Remove(id);
        }
        data.targetsHealth.Add(id, this.health);
    }
}
