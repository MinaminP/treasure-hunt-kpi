using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using Mirror.Examples.MultipleMatch;
using UnityEngine.TextCore.Text;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;
using StarterAssets;
using DMM;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerDataNew : NetworkBehaviour
{
    [SyncVar(hook = "UpdatePlayerName")] public string PlayerName;
    [SyncVar(hook = "UpdatePlayerScore")] public int PlayerScore;
    [SyncVar(hook = "UpdateTeamName")] public string PlayerTeamName;

    public TextMeshProUGUI playerNameUI;

    public ScoreboardController scoreboardController;

    public countdownNew timerCounter;
    public CanvasController canvasController;
    public RandomSpawnTreasure random;
    PlayerMovement movement;
    public bool isFullscreen = false;

    public string localMatchId;
    public DMMapIcon dmii;
    // Start is called before the first frame update
    void Start()
    {
        //scoreboardController = GameObject.FindWithTag("scoreboard").GetComponent<ScoreboardController>();
        scoreboardController = GameObject.Find("board" + gameObject.GetComponent<NetworkMatch>().matchId.ToString()).GetComponent<ScoreboardController>();

        //sccontroll = GameObject.FindWithTag("scoreboard").GetComponent<NetworkMatch>().matchId = gameObject.GetComponent<NetworkMatch>().matchId;
        //sccontroll = GameObject.
        //canvasController = GameObject.FindWithTag("cControll").GetComponent<CanvasController>();
        timerCounter = GameObject.FindWithTag("time").GetComponent<countdownNew>();

        movement = GetComponent<PlayerMovement>();

        random = GameObject.FindWithTag("random").GetComponent<RandomSpawnTreasure>();
        timerCounter.hasStarted = true;
        //random.RandomSpawn();
        //startwaktu();
        dmii = GetComponent<DMMapIcon>();
        if (isLocalPlayer)
        {
            //float randomNumber = Random.Range(0, 1000000);
            //string tempName = canvasController.temporaryLocalName;
            GameObject.Find("Canvas (2)").GetComponent<ChangeNameNew>().playerData = this;
            //CmdSendName("Player " + randomNumber);
            //CmdSendName(tempName);
            //UpdatePlayerName(PlayerName, "Player " + randomNumber);

            movement.enabled = true;

            CmdSendName(LocalPlayerData.playerUserName);
            CmdSendTeamName(LocalPlayerData.playerTeam);

            //scoreboardController.addPlayer(PlayerName, PlayerTeamName);
            //CmdSendTeamName("Red");
            CmdSendInitializeScore();
            DMMap.instance.LoadConfig(0);
        }
        else
        {
            enabled = false;
        }
        

    }

    void Update()
    {
        //Debug.Log("SAYA BAGIAN DARI " + PlayerTeamName);
        //dmii.tint = Color.red;
        //dmii.iconGO.GetComponent<Image>().color = Color.red;
        if (timerCounter.timer <= 0)
        {
            if (isLocalPlayer)
            {
                //CmdShowWin();
                //scoreboardController.UpdateTopTeamScore();
                scoreboardController.GetComponent<Animator>().Play("end");
            }
            //scoreboardController.isEnd = true;
            
        }

        if (isLocalPlayer)
        {
            if (Input.GetKeyUp(KeyCode.M))
            {
                if (isFullscreen == false)
                {
                    DMMap.instance.LoadConfig(1);
                    isFullscreen = true;
                }else if (isFullscreen == true)
                {
                    DMMap.instance.LoadConfig(0);
                    isFullscreen = false;
                }
                
            }
            
        }

    }

    private void OnDestroy()
    {
        if (PlayerTeamName == "Red")
        {
            random.maxRed--;
        }
        else if (PlayerTeamName == "Blue")
        {
            random.maxBlue--;
        }
    }

    [Command]
    public void CmdShowWin()
    {
        scoreboardController.UpdateTopTeamScore();
        
    }

    [Command]
    public void CmdSendName(string playerName)
    {
        PlayerName = playerName;
        gameObject.name = PlayerName;
        //gameObject.name = PlayerName;
        gameObject.name = "player" + gameObject.GetComponent<NetworkMatch>().matchId.ToString();
    }

    [Command]
    public void CmdSendScore(int playerScore, string tim)
    {
        PlayerScore += playerScore;
        Debug.Log(PlayerScore);

        //pake ini
        if (tim == PlayerTeamName)
        {
            scoreboardController.UpdateTeamScore(PlayerTeamName, 1);
            scoreboardController.UpdateScore(PlayerName, 1);
        }
        scoreboardController.UpdateTopTeamScore();

        /*if (PlayerTeamName == "Red")
        {
            scoreboardController.addRedScore();
        }
        else if (PlayerTeamName == "Blue")
        {
            scoreboardController.addBlueScore();
        }*/

    }

    [Command]
    public void CmdSendTeamName(string playerTeamName)
    {
        PlayerTeamName = playerTeamName;
        //Debug.Log("Player Team " + PlayerTeamName);

        //scoreboardController.UpdateSummaryTeam(PlayerName, playerTeamName);
        //scoreboard.UpdateSummaryTeam(PlayerName, playerTeamName);
        //gameObject.name = PlayerName;
        //scoreboard.UpdateName(gameObject.name, PlayerName);
        //scoreboard.UpdateScoreboard(pla);
        //scoreboard.updateDataPlayer();

    }

    [Command]
    public void CmdSendInitializeScore()
    {
        //PlayerScore += playerScore;
        //Debug.Log(PlayerScore);

        //scoreboard.addPlayer(PlayerName);

        //scoreboard.UpdateScoreboard();

        //scoreboard.UpdateScore(PlayerName, playerScore);

        //scoreboard.updateDataPlayer();
        //scoreboard.UpdateScore(PlayerName, PlayerScore);

        //pake ini
        if (scoreboardController != null)
        {
            scoreboardController.UpdateTeamScore(PlayerTeamName, 0);
            scoreboardController.addPlayer(PlayerName, PlayerTeamName);
            scoreboardController.UpdateScore(PlayerName, 0);
        }
        

        Debug.Log("Initialize");
        //scoreboard.playerCountTotal += 1;

        //scoreboard.realUpdate();

    }
    public void UpdatePlayerName(string oldName, string newName)
    {
        Debug.Log("Player changed name from " + oldName + " to " + newName);
        playerNameUI.text = newName;

       
        //scoreboardController.UpdateName(oldName, newName);
    }

    public void UpdatePlayerScore(int oldScore, int newScore)
    {
        Debug.Log(PlayerName + " score updated from " + oldScore + " to " + newScore);
        //scoreboardController.UpdateTopTeamScore();
        //scoreboardController.UpdateScore(PlayerName, newScore);

        if (PlayerTeamName == "Red")
        {
            scoreboardController.RedScore++;
            //scoreboardController.addRedScore();
            
        }
        else if (PlayerTeamName == "Blue")
        {
            scoreboardController.BlueScore++;
            //scoreboardController.addBlueScore();
            
        }


    }

    public void UpdateTeamName(string oldTeam, string newTeam)
    {
        Debug.Log("Player team updated from " + oldTeam + " to " + newTeam);

        if (newTeam == "Red")
        {
            playerNameUI.color = Color.red;
            dmii.tint = Color.red;
            random.maxRed++;
        }
        else if (newTeam == "Blue")
        {
            playerNameUI.color = Color.blue;
            dmii.tint = Color.blue;
            random.maxBlue++;
        }
        //scoreboardController.UpdateSummaryTeam(PlayerName, newTeam);
    }

    private void OnDestroy()
    {
        if (PlayerTeamName == "Red")
        {
            random.maxRed--;
        }
        else if (PlayerTeamName == "Blue")
        {
            random.maxBlue--;
        }
    }
}

