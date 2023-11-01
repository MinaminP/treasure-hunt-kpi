using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerData : NetworkBehaviour
{
    [SyncVar(hook = "UpdatePlayerName")] public string PlayerName;
    [SyncVar(hook = "UpdatePlayerScore")] public int PlayerScore;
    [SyncVar(hook = "UpdateTeamName")] public string PlayerTeamName;

    [SyncVar(hook = "UpdatePlayerStatus")] public bool PlayerStatus = false;

    private List<PlayerData> playerScores = new List<PlayerData>();

    public PlayerData[] playerdatas;

    public TextMeshProUGUI playerNameUI;

    public ScoreBoardNew scoreboard;
    public TeamManager teamManager;

    public CountdownTimer timerCounter;

    

    //public ChangeName change;

    private void Start()
    {
        scoreboard = GameObject.FindWithTag("canvas").GetComponent<ScoreBoardNew>();
        teamManager = GameObject.FindWithTag("canvas").GetComponent<TeamManager>();
        timerCounter = GameObject.FindWithTag("canvas").GetComponent<CountdownTimer>();
        //CmdSendPlayerStatus();
        //scoreboard.playerCountTotal++;
        //scoreboard.playerCountTotal--;
        //timerCounter.hasStarted = true;
        //change = GameObject.Find("Canvas").GetComponent<ChangeName>();
        cmdaddcount();
        if (isLocalPlayer)
        {
            GameObject.Find("Canvas").GetComponent<ChangeName>().playerData = this;
            //CmdSendName("Red");
            scoreboard.addPlayer(PlayerName, PlayerStatus, PlayerTeamName);
            CmdSendTeamName(PlayerPrefs.GetString("teamsName"));

            //scoreboard.addPlayer(PlayerName, PlayerStatus, PlayerTeamName);
            CmdSendInitializeScore();

            
            
            //teamManager.SetPlayerTeam(GetComponent<NetworkIdentity>(), TeamManager.Team.Red);
            //playerdatas = Object.FindObjectsOfType<PlayerData>();

            //addNewPlayer();
            //scoreboard.AddPlayer(this);
            //change.playerDataList.Add(this);
        }
        else
        {
            enabled = false;
        }
    }

    

    private void Update()
    {
        //scoreboard.UpdateTopTeamScore();
        if(timerCounter.timer <= 0)
        {
            scoreboard.UpdateTopTeamScore();
        }

        if (PlayerScore >= timerCounter.scoreTarget)
        {
            //timerCounter.timer = 0;
            //scoreboard.UpdatePreWinner();
            timerCounter.endTimer();
        }
    }

    [Command]
    public void addNewPlayer()
    {
        //scoreboard.AddPlayer(this);
    }

    [Command]
    public void CmdSendScore(int playerScore)
    {
        PlayerScore += playerScore;
        Debug.Log(PlayerScore);

        //scoreboard.addPlayer(PlayerName);

        //scoreboard.UpdateScoreboard();

        //scoreboard.UpdateScore(PlayerName, playerScore);

        //scoreboard.updateDataPlayer();
        //scoreboard.UpdateScore(PlayerName, PlayerScore);

        scoreboard.UpdateTeamScore(PlayerTeamName, 1);
        //scoreboard.RpcUpdateTopTeamScore();
        //scoreboard.benerUpdate();

        //scoreboard.realUpdate();

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

        scoreboard.UpdateTeamScore(PlayerTeamName, 0);
        //scoreboard.playerCountTotal += 1;

        //scoreboard.realUpdate();

    }



    [Command]
    public void CmdSendName(string playerName)
    {
        PlayerName = playerName;
        //gameObject.name = PlayerName;
        //gameObject.name = "player" + gameObject.GetComponent<NetworkMatch>().matchId.ToString();
        //scoreboard.UpdateName(gameObject.name, PlayerName);
        //scoreboard.UpdateScoreboard(pla);
        //scoreboard.updateDataPlayer();

    }

    [Command]
    public void CmdSendTeamName(string playerTeamName)
    {
        PlayerTeamName = playerTeamName;
        scoreboard.UpdateSummaryTeam(PlayerName, playerTeamName);
        //gameObject.name = PlayerName;
        //scoreboard.UpdateName(gameObject.name, PlayerName);
        //scoreboard.UpdateScoreboard(pla);
        //scoreboard.updateDataPlayer();

    }

    [Command]
    public void CmdSendPlayerStatus(bool playerStatus)
    {
        PlayerStatus = playerStatus;
        scoreboard.playerReadyTotal++;
    }

    [Command]
    public void cmdaddcount()
    {
        scoreboard.playerCountTotal++;
    }

    [Command]
    public void cmdaddtarget()
    {
        timerCounter.scoreTarget++;
    }

    [Command]
    public void cmdsumtarget()
    {
        timerCounter.scoreTarget--;
    }


    public void UpdatePlayerName(string oldName, string newName)
    {
        Debug.Log("Player changed name from " + oldName + " to " + newName);

        playerNameUI.text = newName;
        scoreboard.UpdateName(oldName, newName);
        //gameObject.name = newName;
    }

    public void UpdatePlayerScore(int oldScore, int newScore)
    {
        Debug.Log("Player score updated from " + oldScore + " to " + newScore);
        scoreboard.UpdateScore(PlayerName, oldScore+1);

        if (timerCounter.timer <= 0)
        {
            scoreboard.UpdateTopTeamScore();
        }

        //scoreboard.UpdatePreWinner();

        //scoreboard.benerUpdate();
        //scoreboard.GetTotalScore();
        //scoreboard.UpdateTeamScore(PlayerTeamName, 1);
    }
    
    public void UpdateTeamName(string oldTeam, string newTeam)
    {
        Debug.Log("Player team updated from " + oldTeam + " to " + newTeam);
        
        
    }

    public void UpdatePlayerStatus(bool oldStatus, bool newStatus)
    {
        Debug.Log("Player status updated from " + oldStatus + " to " + newStatus);
        scoreboard.UpdateStatus(PlayerName, newStatus);
    }




}
