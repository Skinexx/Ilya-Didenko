using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBeahaviour : MonoBehaviour
{

    private Transform target;
    private float speed = 15;
    private float rocketStrenght = 15;
    private float aliveTime = 5;
    private bool homing;

    
    void Update()
    {
        if (homing && target != null)
        {
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (target != null)
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                Rigidbody targetRb = enemy.GetComponent<Rigidbody>();
                Vector3 away = -collision.contacts[0].normal;
                targetRb.AddForce(away * rocketStrenght, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}

