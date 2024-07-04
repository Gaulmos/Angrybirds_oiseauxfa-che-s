using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInput : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Out the method if not touching the screen
        if (Touchscreen.current == null || !Touchscreen.current.primaryTouch.press.isPressed)
        {
            return;
        }

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, mainCamera.nearClipPlane));

        Debug.Log("TOUCH POSITION: " + touchPosition);
        Debug.Log("WORLD POSITION: " + worldPosition);
    }
}
