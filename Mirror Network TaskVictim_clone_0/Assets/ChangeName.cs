using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeName : NetworkBehaviour
{
    public InputField nameField;
    public PlayerData playerData;

    //public List<PlayerData> playerDataList = new List<PlayerData>();

    public ScoreBoardNew scoreboard;

    private void Start()
    {
        scoreboard = GameObject.FindWithTag("canvas").GetComponent<ScoreBoardNew>();
    }

   
    public void changeNameButton()
    {
        playerData.CmdSendName(nameField.text);
        //scoreboard.UpdateScoreboard();
    }

    public void changeTeamNameButton(string team)
    {
        playerData.CmdSendTeamName(team);
        //scoreboard.UpdateScoreboard();
    }

    public void TestFUnctyion()
    {

    }

    public void changeScoreButton()
    {
        playerData.CmdSendScore(1);
        //scoreboard.UpdateScoreboard();
        //scoreboard.updateDataPlayer();
        //scoreboard.benerUpdate();
    }

    public void changeStatusButton()
    {
        playerData.CmdSendPlayerStatus(true);
        //scoreboard.playerCountTotal++;
        //scoreboard.UpdateScoreboard();
        //scoreboard.updateDataPlayer();
        //scoreboard.benerUpdate();
    }

    public void changeTargetButton()
    {
        playerData.cmdaddtarget();
    }

    public void changesumTargetButton()
    {
        playerData.cmdsumtarget();
    }



}
