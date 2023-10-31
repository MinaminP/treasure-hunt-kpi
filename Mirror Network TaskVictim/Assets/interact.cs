using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;
using DMM;

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

    //[SyncVar(hook = nameof(OnBlueDetected))]
    public int blue = 0;

    //[SyncVar(hook = nameof(OnRedDetected))]
    public int red = 0;

    public RandomSpawnTreasure random;

    public ScoreboardController scoreboardController;

    public DMMapIcon DMI;

    // Start is called before the first frame update
    public void Start()
    {
        //scoreAdd.dataUpdates();
        DMI = GetComponent<DMMapIcon>();
        canvas = GameObject.FindWithTag("canvas").GetComponent<ChangeNameNew>();
        random = GameObject.FindWithTag("random").GetComponent<RandomSpawnTreasure>();
        isInArea = false;

        scoreboardController = GameObject.FindWithTag("scoreboard").GetComponent<ScoreboardController>();
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
                if (isActive == true)
                {
                    if (blue >= random.maxBlue)
                    {
                        canvas.changeScoreButton();
                        //addBlueScoree();
                        Debug.Log("Blue Team got the treasure");
                        hancurkan();
                        random.RandomSpawn();
                    }

                    if(red >= random.maxRed)
                    {
                        canvas.changeScoreButton();
                        //addRedScoree();
                        Debug.Log("Red Team got the treasure");
                        hancurkan();
                        random.RandomSpawn();
                    }
                    
                }
                
            }
        }

        if (isActive == false)
        {
            //gameObject.SetActive(false);
            DMI.enabled = false;
            theObject.SetActive(false);
        }else if(isActive == true)
        {
            //gameObject.SetActive(true);
            DMI.enabled = true;
            theObject.SetActive(true);
        }
        
    }

    [Command(requiresAuthority = false)]
    public void addRedScoree()
    {
        scoreboardController.addRedScore();
    }

    [Command(requiresAuthority = false)]
    public void addBlueScoree()
    {
        scoreboardController.addBlueScore();
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
            if (other.GetComponent<PlayerDataNew>().PlayerTeamName == "Blue")
            {
                blue++;
            }

            if (other.GetComponent<PlayerDataNew>().PlayerTeamName == "Red")
            {
                red++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInArea = false;
            if (other.GetComponent<PlayerDataNew>().PlayerTeamName == "Blue")
            {
                blue--;
            }

            if (other.GetComponent<PlayerDataNew>().PlayerTeamName == "Red")
            {
                red--;
            }
        }
    }

    public void OnActiveChanged(bool oldVal, bool newVal)
    {

    }

    public void OnBlueDetected(int oldVal, int newVal)
    {

    }

    public void OnRedDetected(int oldVal, int newVal)
    {

    }
}
