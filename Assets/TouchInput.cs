using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInput : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rb;
    private bool isDragging = false;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the touchscreen is active
        if (Touchscreen.current != null)
        {
            HandleTouchInput();
        }
        // Fallback to mouse input if touchscreen is not available
        else if (Mouse.current != null)
        {
            HandleMouseInput();
        }
    }

    void HandleTouchInput()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, mainCamera.nearClipPlane));
            worldPosition.z = 0;

            if (!isDragging)
            {
                // Check if the touch is on the ball
                Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);
                if (hitCollider != null && hitCollider.gameObject == gameObject)
                {
                    isDragging = true;
                    rb.isKinematic = true;
                    rb.velocity = Vector2.zero;
                }
            }

            if (isDragging)
            {
                rb.MovePosition(worldPosition);
            }
        }
        else if (isDragging)
        {
            isDragging = false;
            LaunchBall();
        }
    }

    void HandleMouseInput()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));
            worldPosition.z = 0;

            if (!isDragging)
            {
                // Check if the click is on the ball
                Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);
                if (hitCollider != null && hitCollider.gameObject == gameObject)
                {
                    isDragging = true;
                    rb.isKinematic = true;
                    rb.velocity = Vector2.zero;
                }
            }

            if (isDragging)
            {
                rb.MovePosition(worldPosition);
            }
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
    }
}
