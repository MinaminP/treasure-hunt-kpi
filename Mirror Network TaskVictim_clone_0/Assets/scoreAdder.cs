using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreAdder : NetworkBehaviour
{
    [SyncVar] public float score = 0f;

    public float tempSkor;
    public bool isInArea;

    public GameObject siCanvas;

    public float skor = 0f;

    public GameObject[] bola;

    private Scoreboard scoreboard;


    // Start is called before the first frame update
    public void Start()
    {
        scoreboard = FindObjectOfType<Scoreboard>();

        /*if (isLocalPlayer)
        {
            siCanvas = GameObject.FindWithTag("canvas");
            siCanvas.GetComponent<playerList>().findPlayers();
            siCanvas.GetComponent<playerList>().updateDataPlayer();

            GameObject.FindWithTag("bola").GetComponent<interact>().scoreAdd = this;
            //bola = GameObject.FindGameObjectsWithTag("bola").GetComponents<interact>().scoreAdd = this;


        }
        else
        {
            enabled = false;
        }*/

        //siCanvas = GameObject.FindWithTag("canvas");
        //siCanvas.GetComponent<playerList>().findPlayers();
        //siCanvas.GetComponent<playerList>().updateDataPlayer2();
        //siCanvas.GetComponent<playerList>().StartCoroutine(updateDataPlayer2());
        //StartCoroutine(siCanvas.GetComponent<playerList>().updateDataPlayer2());
    }

    //public override onother

    // Update is called once per frame
    void Update()
    {
        if (isInArea == true)
        {
            if (isLocalPlayer)
            {
                
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bola"))
        {
            isInArea = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("bola"))
        {
            isInArea = false;

        }
    }

    public void tambahSkor()
    {
        skorNambahh(1f);
    }

    [Command]
    public void skorNambahh(float tambah)
    {
        score += tambah;
        //siCanvas.GetComponent<playerList>().updateDataPlayer();
    }

    public void dataUpdates()
    {
        siCanvas = GameObject.FindWithTag("canvas");
        siCanvas.GetComponent<playerList>().findPlayers();
        siCanvas.GetComponent<playerList>().updateDataPlayer();
    }


}
