using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public enum TargetType { 
        Good,
        Bad
    }

    public int pointvalue;
    public ParticleSystem explosionParticle;
    public TargetType targetType;


    private float minSpeed = 12;
    private float maxSpeed = 18;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;

    private Rigidbody rigidbody;
    private GameManager gameManager;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>(); 

        transform.position = RandomSpawnPosition();
        rigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        rigidbody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);       
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);

    }
    
    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            gameManager.UpdateScore(pointvalue);
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetType  == TargetType.Good)
        {
            gameManager.UpdateLives(1);
        }

        Destroy(gameObject);
    }
}
 

