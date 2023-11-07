using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float hangTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;           

    private Rigidbody rigidbody;
    public PowerupType currentPowerup = PowerupType.None;
    public GameObject focalPoint;
    public GameObject powerupIndicator;
    public GameObject rocketPrefab;
    private GameObject rocketTmp;
    private Coroutine powerupCountdown;   

    public bool hasPowerup;
   
    private float powerupStregth = 15;
    private float floorY;    
    private bool isSmash;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        rigidbody.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (currentPowerup == PowerupType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }
        else if (currentPowerup == PowerupType.Smash && Input.GetKeyDown(KeyCode.Space) && !isSmash)
        {
            isSmash = true;
            StartCoroutine(Smash());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Powerup powerup))
        {
            hasPowerup = true;
            currentPowerup = powerup.powerupType;
            Destroy(other.gameObject);

            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
           powerupCountdown = StartCoroutine(PowerupCooldown());
            powerupIndicator.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy) && currentPowerup == PowerupType.Pushback)
        {
            Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (enemy.transform.position - transform.position);

            enemyRb.AddForce(awayFromPlayer * powerupStregth, ForceMode.Impulse);
        }
    }

    private IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(7);
        currentPowerup = PowerupType.None;
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    private void LaunchRockets()
    {
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            rocketTmp = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            rocketTmp.GetComponent<RocketBeahaviour>().Fire(enemy.transform);
        }
    }

    private IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();

        floorY = transform.position.y;
        float jumpTime = Time.time + hangTime;

        while (Time.time < jumpTime)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, smashSpeed);
            yield return null;
        }

        while (transform.position.y > floorY)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, -smashSpeed * 2);
            yield return null;            
        }

        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0, ForceMode.Impulse);
            }
        }

        isSmash = false;
    }

}
