using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCollision : NetworkBehaviour
{
    public float forceMagnitude;
    // Start is called before the first frame update
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        /*if (hit.collider.tag == "Player" && !isLocalPlayer)
        {
            //Vector3 pushDir = new Vector3(hit.moveDirection.x, hit.moveDirection.y, hit.moveDirection.z);
            Vector3 direction = (hit.gameObject.transform.position - gameObject.transform.position).normalized;
            Debug.Log(direction);

            if (direction.y < -0.3f) return;
            //CharacterController controller = gameObject.GetComponent<CharacterController>();
            //hit.controller.Move(pushDir.normalized * strength);
            hit.controller.Move(direction * strength);
            //hit.controller.SimpleMove(pushDir.normalized);
        }*/

        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        if (rigidbody != null) 
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.tag == "Player")
        {
            Debug.Log("Collided");
            Vector3 direction = (other.gameObject.transform.position - gameObject.transform.position).normalized;
            CharacterController controller = other.gameObject.GetComponent<CharacterController>();
            controller.Move(direction.normalized * strength);
            //collision.rigidbody.AddForce(direction * strength, ForceMode.Impulse);
        }*/
    }

    private void OnTriggerStay(Collider other)
    {
       /* if (other.tag == "Player")
        {
            Debug.Log("Collided");
            Vector3 direction = (other.gameObject.transform.position - gameObject.transform.position).normalized;
            CharacterController controller = other.gameObject.GetComponent<CharacterController>();
            controller.Move(direction.normalized);
            //collision.rigidbody.AddForce(direction * strength, ForceMode.Impulse);
        }*/
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
