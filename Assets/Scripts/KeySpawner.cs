using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public Transform[] spawnPositions;
    public Key key;

    private void Start()
    {
        foreach (var point in spawnPositions)
        {
            Instantiate(key, point.position, Quaternion.identity);
        }

/*        int randomPosition = Random.Range(0, spawnPositions.Length);
        Instantiate(key, spawnPositions[randomPosition].position, Quaternion.identity);*/
    }
}
