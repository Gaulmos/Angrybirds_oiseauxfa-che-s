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

    // Start is called before the first frame update
    void Start()
    {
        if (square != null)
        {
            square.transform.localScale = new Vector3(2.0f, 0.5f, 0.5f);
        }
        else
        {
            Debug.LogError("Square is not assigned in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (square != null)
        {
            Rotate(square, changeSpeed * 5);
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
        if (square2 != null)
        {
            Rigidbody2D rb = square2.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                PushUp(rb, 1);
            }
            else
            {
                Debug.LogError("Rigidbody2D component is missing on square2.");
            }
        }
    }

    // Rotate on Z axis by a given speed
    void Rotate(GameObject go, float speed)
    {
        go.transform.localRotation = Quaternion.Euler(0, 0, go.transform.localEulerAngles.z + speed * Time.deltaTime);
    }

    // Return a new color interpolating R(sin) and B(cos) channels by a given speed
    Color GetSinCosColor(float speed)
    {
        timer += speed * Time.deltaTime;
        return new Color(Mathf.Sin(timer), Mathf.Cos(timer), 1, 1);
    }

    // Add a vertical force to the Rigidbody
    void PushUp(Rigidbody2D rb, float force)
    {
        if (rb != null)
        {
            rb.AddForce(new Vector2(0, force));
        }
        else
        {
            Debug.LogError("No Rigidbody2D detected");
        }
    }
}
