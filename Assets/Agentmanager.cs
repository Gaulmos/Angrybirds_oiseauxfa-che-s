using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agentmanager : MonoBehaviour
{
    [SerializeField] private GameObject square;
    [SerializeField] private GameObject square1;
    [SerializeField] private GameObject square2;
    [SerializeField] private GameObject square3;
    [SerializeField] private GameObject square4;
    public float changeSpeed = 10f;
    private static float timer = 0.0f;

    void Start()
    {
        // Initialize squares
        InitializeSquare(square);
        InitializeSquare(square1);
        InitializeSquare(square2);
        InitializeSquare(square3);
        InitializeSquare(square4);
    }

    void Update()
    {
        if (square != null)
        {
            // Rotate(square, changeSpeed * 5); // Uncomment if rotation is needed
        }
        if (square1 != null)
        {
            SpriteRenderer sr = square1.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.material.color = GetSinCosColor(changeSpeed / 2);
            }
            else
            {
                Debug.LogError("SpriteRenderer component is missing on square1.");
            }
        }
    }

    void InitializeSquare(GameObject square)
    {
        if (square != null)
        {
            square.transform.localScale = new Vector3(2.0f, 0.5f, 0.5f);
            Rigidbody2D rb = square.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0f; // Disable gravity by default
                rb.isKinematic = true; // Make the square kinematic initially
            }
            else
            {
                Debug.LogError("Rigidbody2D component is missing on square.");
            }
            // Add a BoxCollider2D component to the square
            BoxCollider2D collider = square.AddComponent<BoxCollider2D>();
            collider.isTrigger = true; // Set the collider as a trigger
        }
        else
        {
            Debug.LogError("Square is not assigned in the Inspector.");
        }
    }

    // Return a new color interpolating R(sin) and B(cos) channels by a given speed
    Color GetSinCosColor(float speed)
    {
        timer += speed * Time.deltaTime;
        return new Color(Mathf.Sin(timer), Mathf.Cos(timer), 1, 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bird"))
        {
            Rigidbody2D rb = collision.collider.attachedRigidbody;
            if (rb != null)
            {
                rb.gravityScale = 1f; // Enable gravity
                rb.isKinematic = false; // Make the square dynamic
            }
        }
    }
}
