using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror;
using Mirror.Examples.MultipleMatch;

public class loginController : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public GameObject setNamePanel;
    public GameObject NetworkManagerObject;

    //public CanvasController canvasController;

    //[SyncVar(hook = nameof(SetPlayerName))] public string PlayerUserName;

    public void setTheName()
    {
        LocalPlayerData.playerUserName = nameInputField.text;
        Debug.Log(LocalPlayerData.playerUserName);
        PlayerPrefs.SetString("theName", nameInputField.text);
        //PlayerUserName = LocalPlayerData.playerUserName;
        //canvasController.temporaryLocalName = LocalPlayerData.playerUserName;
        //Debug.Log("temporaryLocalName : " + canvasController.temporaryLocalName);
        //CmdSetPlayerName(LocalPlayerData.playerUserName);
        NetworkManagerObject.SetActive(true);
        setNamePanel.SetActive(false);
    }

    /*[Command]
    public void CmdSetPlayerName(string name)
    {
        PlayerUserName = name;
        Debug.Log(PlayerUserName);
    }

    public void SetPlayerName(string oldValue, string newValue)
    {
        //canvasController.temporaryLocalName = newValue;
        Debug.Log(newValue);
        //playerName.text = newValue;
    }*/
}
