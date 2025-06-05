using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
    public EnemyController[] enemiesToSpawn;
    public Transform enemyFolder;
    public Transform[] spawnLocations;
    public float spawnRate;
    public float spawnTime;

    private void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime >= spawnRate)
        {
            spawnTime = 0;
            EnemyController newEnemy = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)], enemyFolder);
            newEnemy.transform.position = spawnLocations[Random.Range(0, spawnLocations.Length)].position;
        }
    }
}
