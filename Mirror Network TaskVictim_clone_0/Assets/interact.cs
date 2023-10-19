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
    public GameObject theObject;


    [SyncVar(hook = nameof(OnActiveChanged))]
    public bool isActive = false;

    public RandomSpawnTreasure random;

    // Start is called before the first frame update
    public void Start()
    {
        //scoreAdd.dataUpdates();
        canvas = GameObject.FindWithTag("canvas").GetComponent<ChangeNameNew>();
        random = GameObject.FindWithTag("random").GetComponent<RandomSpawnTreasure>();
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
                //gameObject.SetActive(false);

                hancurkan();
                random.RandomSpawn();
            }


        }

        if (isActive == false)
        {
            //gameObject.SetActive(false);
            theObject.SetActive(false);
        }else if(isActive == true)
        {
            //gameObject.SetActive(true);
            theObject.SetActive(true);
        }
        
    }


    [Command(requiresAuthority = false)]
    public void hancurkan()
    {
        //score += 1f;
        //NetworkServer.Destroy(gameObject);
        isActive = false;
        //gameObject.SetActive(false);
    }

    public void munculkan()
    {
        isActive = true;
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

    public void OnActiveChanged(bool oldVal, bool newVal)
    {

    }
}
