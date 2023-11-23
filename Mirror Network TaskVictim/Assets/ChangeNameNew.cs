using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNameNew : MonoBehaviour
{
    public InputField nameField;
    public PlayerDataNew playerData;
    public GameObject instructionPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        instructionPanel.SetActive(true);
    }

    public void changeNameButton()
    {
        playerData.CmdSendName(nameField.text);
    }

    public void changeScoreButton(string namaTim)
    {
        playerData.CmdSendScore(1,namaTim);
        
    }

    public void changeTeamNameButton(string team)
    {
        playerData.CmdSendTeamName(team);
        //scoreboard.UpdateScoreboard();
    }
}
