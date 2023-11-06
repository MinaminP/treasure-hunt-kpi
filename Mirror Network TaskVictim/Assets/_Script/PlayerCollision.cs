using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCollision : NetworkBehaviour
{
    [Range(0.5f, 5f)] public float strength = 1.1f;
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
        if (other.tag == "Player")
        {
            Debug.Log("Collided");
            Vector3 direction = (other.gameObject.transform.position - gameObject.transform.position).normalized;
            CharacterController controller = other.gameObject.GetComponent<CharacterController>();
            controller.Move(direction.normalized);
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
