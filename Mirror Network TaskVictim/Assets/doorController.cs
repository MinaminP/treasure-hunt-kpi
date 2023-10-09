using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : NetworkBehaviour
{
    public bool isOpened;
    public bool isInArea;
    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInArea == true)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (isOpened == false)
                {
                    bukaPintu();
                    isOpened = true;
                }else if (isOpened == true)
                {
                    tutupPintu();
                    isOpened = false;
                }
            }
            
            
        }
    }

    [Command(requiresAuthority = false)]
    void bukaPintu()
    {
        GetComponent<Animator>().Play("open");

    }

    [Command(requiresAuthority = false)]
    void tutupPintu()
    {
        GetComponent<Animator>().Play("close");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInArea = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInArea = false;

        }
    }
}
