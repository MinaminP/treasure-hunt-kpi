using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNameNew : MonoBehaviour
{
    public InputField nameField;
    public PlayerDataNew playerData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void changeNameButton()
    {
        playerData.CmdSendName(nameField.text);
    }

    public void changeScoreButton()
    {
        playerData.CmdSendScore(1);
        
    }

    public void changeTeamNameButton(string team)
    {
        playerData.CmdSendTeamName(team);
        //scoreboard.UpdateScoreboard();
    }
}
