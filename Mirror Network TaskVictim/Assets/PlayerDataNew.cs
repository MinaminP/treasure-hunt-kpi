using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using Mirror.Examples.MultipleMatch;

public class PlayerDataNew : NetworkBehaviour
{
    [SyncVar(hook = "UpdatePlayerName")] public string PlayerName;
    [SyncVar(hook = "UpdatePlayerScore")] public int PlayerScore;
    [SyncVar(hook = "UpdateTeamName")] public string PlayerTeamName;


    public TextMeshProUGUI playerNameUI;

    public ScoreboardController scoreboardController;
    public countdownNew timerCounter;
    public CanvasController canvasController;
    // Start is called before the first frame update
    void Start()
    {
        //scoreboardController = GameObject.FindWithTag("canvas").GetComponent<ScoreboardController>();
        //timerCounter = GameObject.FindWithTag("canvas").GetComponent<countdownNew>();
        //canvasController = GameObject.FindWithTag("cControll").GetComponent<CanvasController>();
        //timerCounter.hasStarted = true;
        if (isLocalPlayer)
        {
            //float randomNumber = Random.Range(0, 1000000);
            //string tempName = canvasController.temporaryLocalName;
            GameObject.Find("Canvas (2)").GetComponent<ChangeNameNew>().playerData = this;
            //CmdSendName("Player " + randomNumber);
            //CmdSendName(tempName);
            //UpdatePlayerName(PlayerName, "Player " + randomNumber);
            CmdSendName(LocalPlayerData.playerUserName);

            //scoreboardController.addPlayer(PlayerName, PlayerTeamName);
            //CmdSendTeamName("Red");
            //CmdSendInitializeScore();
        }
        else
        {
            enabled = false;
        }
        
    }

    [Command]
    public void CmdSendName(string playerName)
    {
        PlayerName = playerName;
        gameObject.name = PlayerName;

    }

    [Command]
    public void CmdSendScore(int playerScore)
    {
        PlayerScore += playerScore;
        Debug.Log(PlayerScore);
        //scoreboardController.UpdateTeamScore(PlayerTeamName, 1);

    }

    [Command]
    public void CmdSendTeamName(string playerTeamName)
    {
        PlayerTeamName = playerTeamName;
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

        scoreboardController.UpdateTeamScore(PlayerTeamName, 0);

        //scoreboard.playerCountTotal += 1;

        //scoreboard.realUpdate();

    }

    // Update is called once per frame
    void Update()
    {
        /*if (timerCounter.timer <= 0)
        {
            scoreboardController.UpdateTopTeamScore();
        }*/
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
        //scoreboardController.UpdateScore(PlayerName, newScore);
    }

    public void UpdateTeamName(string oldTeam, string newTeam)
    {
        Debug.Log("Player team updated from " + oldTeam + " to " + newTeam);
        //scoreboardController.UpdateSummaryTeam(PlayerName, newTeam);


    }
}
