using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Mirror;
using UnityEngine.InputSystem;

public class SwimBehaviour : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        //playerMovement.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "water")
        {
            playerMovement.Gravity = 0;
            playerMovement.Grounded = false;
            playerMovement.GroundedOffset = 0;
            playerMovement.GroundedRadius = 0;
            playerMovement.isSwimming = true;
            characterController.height = 0.3f;
            characterController.center = new Vector3(0, 1.2f, 0);
            Debug.Log("Entering Water");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "water")
        {
            playerMovement.Gravity = -15.0f;
            playerMovement.Grounded = true;
            playerMovement.GroundedOffset = -0.26f;
            playerMovement.GroundedRadius = 0.28f;
            playerMovement.isSwimming = false;
            characterController.height = 1.8f;
            characterController.center = new Vector3(0, 0.93f, 0);
            Debug.Log("Exiting Water");
        }
    }
}
