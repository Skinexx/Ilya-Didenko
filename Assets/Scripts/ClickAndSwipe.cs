using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;
    private Camera camera;
    private Vector3 mousePosition;
    private TrailRenderer trail;
    private BoxCollider collider;
    private bool isSwiping;
   
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        camera = Camera.main;
        trail = GetComponent<TrailRenderer>();
        collider = GetComponent<BoxCollider>();
        trail.enabled = false;
        collider.enabled = false;
    }

    
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSwiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isSwiping = false;
                UpdateComponents();
            }

            if (isSwiping)
            {
                UpdateMousePosition();
            }
        }
    }

    private void UpdateMousePosition()
    {
        mousePosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        transform.position = mousePosition;
    }

    private void UpdateComponents()
    {
        trail.enabled = isSwiping;
        collider.enabled = isSwiping;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Target target))
        {
            target.DestroyTarget();
        }
    }
}
