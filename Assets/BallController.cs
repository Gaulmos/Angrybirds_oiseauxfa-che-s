using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D birdRigidbody;
    private bool isDragging = false;

    [SerializeField] private GameObject birdPrefab; // Prefab de l'oiseau
    [SerializeField] private Rigidbody2D pivotRigidbody;
    [SerializeField] private float detachDelay = 0.20f;
    [SerializeField] private float respawnDelay = 6.0f;

    private GameObject currentBird;
    // private Rigidbody2D birdRigidbody;
    private SpringJoint2D birdSpringJoint;
    // private bool isSpawningNewBird = false; // Indique si un nouveau bird est en cours de création

    void Start()
    {
        mainCamera = Camera.main;
        // SpawnNewBird(); // Appel initial pour spawner un bird au démarrage
    }

   void Update()
{
    // Vérifier si Touchscreen.current est null avant d'y accéder
    if (Touchscreen.current != null)
    {
        // Vérifier si Touchscreen.current.primaryTouch est null avant d'y accéder
        if (Touchscreen.current.primaryTouch != null)
        {
            // Vérifier si Touchscreen.current.primaryTouch.press est null avant d'y accéder
            if (Touchscreen.current.primaryTouch.press != null)
            {
                if (!Touchscreen.current.primaryTouch.press.IsPressed())
                {
                    if(isDragging)
                    {
                        LaunchBird();
                        Invoke(nameof(SpawnNewBird), respawnDelay);
                        isDragging = false;
                    }
                    return;
                }
                isDragging = true;

                Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

                // Vérifier si birdRigidbody est null avant d'y accéder
                if (birdRigidbody != null)
                {
                    birdRigidbody.isKinematic = true;
                    birdRigidbody.position = worldPosition;
                }
            }
        }
    }
}



    private void LaunchBird()
    {
        if (birdRigidbody != null)
        {
             birdRigidbody.isKinematic = false;
        // birdRigidbody.bodyType = RigidbodyType2D.Dynamic;
        Invoke(nameof(DetachBird), detachDelay); // Détacher le bird après un petit délai
        }
       
    }

    private void DetachBird()
    {
        if (birdSpringJoint != null)
        {
            birdSpringJoint.enabled = false;
            birdSpringJoint = null;
 
        }

    }

    private void SpawnNewBird()
{
    if (birdPrefab != null)
    {
        if (currentBird != null)
        {
            Destroy(currentBird); // Détruire le bird existant s'il y en a un
        }

        // Instancier un nouveau bird à la position du pivotRigidbody
        currentBird = Instantiate(birdPrefab, pivotRigidbody.transform);
        birdRigidbody = currentBird.GetComponent<Rigidbody2D>();
        if (birdRigidbody == null)
        {
            Debug.LogError("birdRigidbody is null!");
            return;
        }
        birdSpringJoint = currentBird.GetComponent<SpringJoint2D>();
        birdSpringJoint.connectedBody = pivotRigidbody;
    }
}

}

    //         if (birdRigidbody == null)
    //         {
    //             Debug.LogError("The instantiated bird does not have a Rigidbody2D component.");
    //             return;
    //         }

    //         if (birdSpringJoint == null)
    //         {
    //             Debug.LogError("The instantiated bird does not have a SpringJoint2D component.");
    //             return;
    //         }

    //         birdSpringJoint.connectedBody = pivotRigidbody;
    //         birdSpringJoint.enabled = true;
    //         birdRigidbody = birdRigidbody;
    //     }
    //     else
    //     {
    //         Debug.LogError("Bird prefab is not assigned.");
    //     }
    // }

    // IEnumerator SpawnNewBirdCoroutine(float delay)
    // {
    //     Debug.Log("Spawning new bird after delay: " + delay);
    //     yield return new WaitForSeconds(delay);
    //     SpawnNewBird(); // Appeler SpawnNewBird après le délai
    //     // isSpawningNewBird = false; // Réinitialiser le flag après que le bird soit spawner
    //     Debug.Log("New bird spawned and isSpawningNewBird reset.");
    // }
// }
