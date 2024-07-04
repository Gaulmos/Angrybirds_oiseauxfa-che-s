using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rb;

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D component found on this GameObject.");
        }
    }

    void Update()
    {
        // Sortir de la méthode s'il n'y a pas de toucher
        if (Touchscreen.current == null || !Touchscreen.current.primaryTouch.press.isPressed)
        {
            return;
        }

        // Lire la position du toucher
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        // Convertir la position du toucher en position dans le monde
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, mainCamera.nearClipPlane));
        worldPosition.z = 0; // S'assurer que la position z est 0 pour 2D

        // Déplacer la balle à la position du toucher
        rb.MovePosition(worldPosition);
    }
}
