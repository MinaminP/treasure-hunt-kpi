using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCollision : MonoBehaviour
{
    public float forceMagnitude;
    // Start is called before the first frame update
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        /*if (hit.collider.tag == "Player")
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            CharacterController controller = hit.gameObject.GetComponent<CharacterController>();

            controller.Move(forceDirection * forceMagnitude);
        }

        /*Rigidbody rigidbody = hit.collider.attachedRigidbody;
        if (rigidbody != null) 
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            hit.controller.Move(forceDirection * forceMagnitude);
            //rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.tag == "Player")
        {
            Debug.Log("Collided");
            Vector3 direction = (other.gameObject.transform.position - gameObject.transform.position).normalized;
            CharacterController controller = other.gameObject.GetComponent<CharacterController>();
            controller.Move(direction.normalized  * forceMagnitude);
            //collision.rigidbody.AddForce(direction * strength, ForceMode.Impulse);
        }*/
    }

    private void OnTriggerStay(Collider other)
    {
       if (other.tag == "Player")
        {
            Debug.Log("Collided");
            Vector3 direction = (other.gameObject.transform.position - gameObject.transform.position).normalized;
            CharacterController controller = other.gameObject.GetComponent<CharacterController>();
            controller.Move(direction.normalized * forceMagnitude);
            //collision.rigidbody.AddForce(direction * strength, ForceMode.Impulse);
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("Collided");
            Vector3 direction = (collision.gameObject.transform.position - gameObject.transform.position).normalized;
            CharacterController controller = collision.gameObject.GetComponent<CharacterController>();
            controller.Move(-direction.normalized * strength);
            //collision.rigidbody.AddForce(direction * strength, ForceMode.Impulse);
        }
    }*/
}
