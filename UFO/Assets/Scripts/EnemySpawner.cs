using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    const float DIFFICULTY_ACCELERATION = 0.99f;
    const float START_COOLDOWN = 1.0f;

    float maxCooldown = START_COOLDOWN;
    float cooldown = 0;

    public GameObject[] arrEnemies;

    Vector3 GetSpawnPosition()
    {
        float x = Random.Range(-0.1f, 1.1f);
        float y = Random.Range(-0.1f, 1.1f);
        int i = Random.Range(0, 2); //decide what side will be chosen, 0 mean choose horizontal, 1 mean choose vertical

        if (i == 0)
        {
            y = (int)Random.Range(0, 2);
        }
        else
        {
            x = (int)Random.Range(0, 2);
        }

        Vector3 newPosition_ViewPort = new Vector3(x, y, -5.0f);
        Vector3 newPosition_WorldPoint = Camera.main.ViewportToWorldPoint(newPosition_ViewPort);
        newPosition_WorldPoint.z = -5.0f;

        return newPosition_WorldPoint;
    }

    void SpawnNewEnemy()
    {
        //if there is no enemy, this will not work
        if (arrEnemies.Length == 0)
        {
            Debug.LogError("No Enemy in the Spawner");
            return;
        }

        int type = Random.Range(0, arrEnemies.Length - 1);
        Vector3 spawnPosition = GetSpawnPosition();

        Instantiate(arrEnemies[type], spawnPosition, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            //SPAWN NEW ENEMY
            SpawnNewEnemy();

            //DECRESE MAX_COOLDOWN by multiplying with difficulty_acceleration
            //maxCooldown now will be 95%
            maxCooldown *= DIFFICULTY_ACCELERATION;

            //RESET COOLDOWN
            cooldown = maxCooldown;
        }
    }
}
