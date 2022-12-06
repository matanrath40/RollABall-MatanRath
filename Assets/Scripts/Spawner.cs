using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstacle;

    public void Start()
    {
        SpawnObstacles();
    }

    public void SpawnObstacles()
    {
        //for (int i = 0; i < 20; i++)
        //{
        //    Vector3 randomSpawnPosition = new Vector3(Random.Range(-9, 10), 0f, Random.Range(-9, 10));
        //    Instantiate(obstacle, randomSpawnPosition, Quaternion.Euler(0f, Random.Range(0, 360), 0f));

        //    //if (!Physics.CheckSphere(randomSpawnPosition, spawnCollisionCheckRadius))
        //    //{
        //    //    Instantiate(obstacle, randomSpawnPosition, Quaternion.Euler(0f, Random.Range(0, 360), 0f));
        //    //}
        //}

        Vector3 randomSpawnPosition = new Vector3(Random.Range(-9, 10), 0f, Random.Range(-9, 10));
        Instantiate(obstacle, randomSpawnPosition, Quaternion.Euler(0f, Random.Range(0, 360), 0f));

    }

}
