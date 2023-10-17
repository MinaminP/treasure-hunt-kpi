using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;

public class interact : NetworkBehaviour
{
    public bool isInArea;
    //public GameObject siCanvas;
    public float score = 0f;

    public ChangeNameNew canvas;
    //public CountdownTimer timer;

    //public GameObject Player;

    public scoreAdder scoreAdd;
    // Start is called before the first frame update
    public void Start()
    {
        //scoreAdd.dataUpdates();
        canvas = GameObject.FindWithTag("canvas").GetComponent<ChangeNameNew>();
        isInArea = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInArea == true)
        {
            /*if (isLocalPlayer)
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    //Player.GetComponent<scoreAdder>().skorNambahh(1f);
                    //siCanvas.GetComponent<playerList>().updateDataPlayer();
                    scoreAdd.tambahSkor();
                    scoreAdd.dataUpdates();
                    hancurkan();
                }
            }*/
            if (Input.GetKeyUp(KeyCode.E))
            {
                //Player.GetComponent<scoreAdder>().skorNambahh(1f);
                //siCanvas.GetComponent<playerList>().updateDataPlayer();
                //scoreAdd.tambahSkor();
                //scoreAdd.dataUpdates();
                //canvas.changeScoreButton();
                hancurkan();
            }


        }
        
    }


    [Command(requiresAuthority = false)]
    void hancurkan()
    {
        //score += 1f;
        NetworkServer.Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("Got it");
            canvas.changeScoreButton();
            hancurkan();
        }
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
