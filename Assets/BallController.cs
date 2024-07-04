using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rb;
    private bool isDragging = false;

    [SerializeField] private SpringJoint2D springJoint;
    [SerializeField] private GameObject ballPrefab; // Champ sérialisé pour le prefab de la balle
    [SerializeField] private Transform spawnPoint;  // Point de spawn pour les nouvelles balles

    void Start()
    {
        mainCamera = Camera.main;
        SpawnNewBall(); // Spawner une nouvelle balle au démarrage
    }

    void Update()
    {
        if (rb == null) return; // Si pas de balle active, ne rien faire

        if (Touchscreen.current == null) return;

        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, mainCamera.nearClipPlane));
            worldPosition.z = 0;

            if (!isDragging)
            {
                isDragging = true;
                rb.isKinematic = true;
                rb.velocity = Vector2.zero;
            }

            rb.MovePosition(worldPosition);
        }
        else if (isDragging)
        {
            isDragging = false;
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        rb.isKinematic = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Invoke("DetachBall", 0.1f); // Détacher la balle après un petit délai
    }

    void DetachBall()
    {
        springJoint.enabled = false;
        springJoint.connectedBody = null;
        rb = null;
        springJoint = null;
        SpawnNewBall(); // Spawner une nouvelle balle après avoir détaché l'ancienne
    }

    void SpawnNewBall()
    {
        GameObject newBall = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        rb = newBall.GetComponent<Rigidbody2D>();
        springJoint = newBall.GetComponent<SpringJoint2D>();
        if (rb == null || springJoint == null)
        {
            Debug.LogError("The instantiated ball does not have the required components.");
        }
    }
}
