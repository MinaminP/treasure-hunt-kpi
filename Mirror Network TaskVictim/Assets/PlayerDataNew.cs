using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using Mirror.Examples.MultipleMatch;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class PlayerDataNew : NetworkBehaviour
{
    [SyncVar(hook = "UpdatePlayerName")] public string PlayerName;
    [SyncVar(hook = "UpdatePlayerScore")] public int PlayerScore;
    [SyncVar(hook = "UpdateTeamName")] public string PlayerTeamName;


    public TextMeshProUGUI playerNameUI;

    public ScoreboardController scoreboardController;
    public ScoreboardController sccontroll;

    public countdownNew timerCounter;
    public CanvasController canvasController;
    public RandomSpawnTreasure random;

    //public bool isActive;

    public string localMatchId;
    // Start is called before the first frame update
    void Start()
    {
        //scoreboardController = GameObject.FindWithTag("scoreboard").GetComponent<ScoreboardController>();
        sccontroll = GameObject.Find("board" + gameObject.GetComponent<NetworkMatch>().matchId.ToString()).GetComponent<ScoreboardController>();
        scoreboardController = GameObject.Find("board" + gameObject.GetComponent<NetworkMatch>().matchId.ToString()).GetComponent<ScoreboardController>();

        //sccontroll = GameObject.FindWithTag("scoreboard").GetComponent<NetworkMatch>().matchId = gameObject.GetComponent<NetworkMatch>().matchId;
        //sccontroll = GameObject.
        //canvasController = GameObject.FindWithTag("cControll").GetComponent<CanvasController>();
        timerCounter = GameObject.FindWithTag("time").GetComponent<countdownNew>();

        random = GameObject.FindWithTag("random").GetComponent<RandomSpawnTreasure>();
        timerCounter.hasStarted = true;
        random.RandomSpawn();
        //startwaktu();
        if (isLocalPlayer)
        {
            //float randomNumber = Random.Range(0, 1000000);
            //string tempName = canvasController.temporaryLocalName;
            GameObject.Find("Canvas (2)").GetComponent<ChangeNameNew>().playerData = this;
            //CmdSendName("Player " + randomNumber);
            //CmdSendName(tempName);
            //UpdatePlayerName(PlayerName, "Player " + randomNumber);
            
            CmdSendName(LocalPlayerData.playerUserName);
            CmdSendTeamName(LocalPlayerData.playerTeam);

            //scoreboardController.addPlayer(PlayerName, PlayerTeamName);
            //CmdSendTeamName("Red");
            CmdSendInitializeScore();
        }
        else
        {
            enabled = false;
        }
        
    }

    void Update()
    {
        
    }

   

    [Command]
    public void CmdSendName(string playerName)
    {
        PlayerName = playerName;
        //gameObject.name = PlayerName;
        gameObject.name = "player" + gameObject.GetComponent<NetworkMatch>().matchId.ToString();

    }

    [Command]
    public void CmdSendScore(int playerScore)
    {
        PlayerScore += playerScore;
        Debug.Log(PlayerScore);

        //pake ini
        scoreboardController.UpdateTeamScore(PlayerTeamName, 1);
        scoreboardController.UpdateScore(PlayerName, 1);

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
            //scoreboardController.RedScore++;
            //scoreboardController.addRedScore();
        }
        else if (PlayerTeamName == "Blue")
        {
            //scoreboardController.BlueScore++;
            //scoreboardController.addBlueScore();
        }


    }

    public void UpdateTeamName(string oldTeam, string newTeam)
    {
        Debug.Log("Player team updated from " + oldTeam + " to " + newTeam);

        if (newTeam == "Red")
        {
            playerNameUI.color = Color.red;
            random.maxRed++;
        }
        else if (newTeam == "Blue")
        {
            playerNameUI.color = Color.blue;
            random.maxBlue++;
        }
        //scoreboardController.UpdateSummaryTeam(PlayerName, newTeam);

        


    }
}
