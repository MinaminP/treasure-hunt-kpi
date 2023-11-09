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

    public bool isRedFirst = false;
    public bool isBlueFirst = false;

    public RandomSpawnTreasure random;

    public GameObject pickupArea;
    public ScoreboardController scoreboardController;
    public DMMapIcon DMI;

    public bool isBlueFirst, isRedFirst;

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
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (isActive == true)
                {
                    if (blue >= random.maxBlue)
                    {
                        canvas.changeScoreButton("Blue");
                        //addBlueScoree();
                        scoreboardController.UpdateTeamScore("Blue", 1);
                        //scoreboardController.BlueScore++;
                        Debug.Log("Blue Team got the treasure");
                        hancurkan();
                        random.RandomSpawn();
                    }
                    else if (red >= random.maxRed)
                    {
                        //scoreboardController.RedScore++;
                        canvas.changeScoreButton("Red");
                        //addRedScoree();
                        scoreboardController.UpdateTeamScore("Red", 1);
                        Debug.Log("Red Team got the treasure");
                        hancurkan();
                        random.RandomSpawn();
                    }
                }
            }
            TreasureRadiusChecker();
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
            //TreasureRadiusChecker();

            /*if (red == random.maxRed)
            {
                pickupArea.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 50);
            }
            else if (blue == random.maxBlue)
            {
                pickupArea.GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 50);
            }*/
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            /*if (red == random.maxRed)
            {
                pickupArea.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 50);
            }
            else if (blue == random.maxBlue)
            {
                pickupArea.GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 50);
            }*/
            //TreasureRadiusChecker();
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
                if (blue < 0)
                {
                    blue = 0;
                }
            }

            if (other.GetComponent<PlayerDataNew>().PlayerTeamName == "Red")
            {
                red--;
                if (red < 0)
                {
                    red = 0;
                }
            }
            /*if(blue == random.maxBlue || red == random.maxRed)
            {
                pickupArea.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 30);
            }*/

            //TreasureRadiusChecker();
        }
    }

    public void TreasureRadiusChecker()
    {
        if (red >= random.maxRed)
        {
            if (isBlueFirst == false)
            {
                isRedFirst = true;
            }
            else if (isBlueFirst == true)
            {
                isRedFirst = false;
            }

        }
        else if (red < random.maxRed)
        {
            isRedFirst = false;
        }

        if (blue >= random.maxBlue)
        {
            if (isRedFirst == false)
            {
                isBlueFirst = true;
            }
            else if (isRedFirst == true)
            {
                isBlueFirst = false;
            }
        }
        else if (blue < random.maxBlue)
        {
            isBlueFirst = false;
        }

        if (isBlueFirst == true)
        {
            pickupArea.GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 50);
        }
        else if (isRedFirst == true)
        {
            pickupArea.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 50);
        }
        else if (isRedFirst == false && isBlueFirst == false)
        {
            pickupArea.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 30);
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
