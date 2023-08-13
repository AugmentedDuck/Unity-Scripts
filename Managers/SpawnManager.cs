using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float timeToNextSpawn = 0f;
    private readonly float timeBetweenSpawns = 2f;
    [SerializeField] private GameObject[] spawnables;

    // Update is called once per frame
    void Update()
    {
        if (timeToNextSpawn < 0)
        {
            Instantiate(papers[Random.Range(0, spawnables.Length)]);
            timeToNextSpawn = timeBetweenSpawns;
        }
        else
        {
            timeToNextSpawn -= Time.deltaTime;
        }
    }
}
