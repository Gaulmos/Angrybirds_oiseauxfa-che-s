using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Agentcontroller : MonoBehaviour
{
    // Vitesse de chute de l'Agent
    public float fallSpeed = 15f;
    // Rigidbody de l'Agent (attaché dans l'éditeur Unity)
    private Rigidbody2D rb;
    void Start()
    {
        // Récupérer le Rigidbody attaché à l'Agent
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // Faire tomber l'Agent vers le bas
        Vector3 fall = new Vector3(0, -fallSpeed, 0) * Time.deltaTime;
        rb.MovePosition(transform.position + fall);
    }
}
