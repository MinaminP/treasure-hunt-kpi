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
    public GameObject selectCharacterPanel;
    public GameObject playerSetupCanvas;
    public GameObject NetworkManagerObject;

    public GameObject[] charactersUI;

    public Button startButton;
    public string address;

    NetworkManager manager;
    //public CanvasController canvasController;

    //[SyncVar(hook = nameof(SetPlayerName))] public string PlayerUserName;

    private void Start()
    {
        manager = NetworkManagerObject.GetComponent<NetworkManager>();
    }

    public void setTheName()
    {
        LocalPlayerData.playerUserName = nameInputField.text;
        Debug.Log(LocalPlayerData.playerUserName);
        PlayerPrefs.SetString("theName", nameInputField.text);
        setNamePanel.SetActive(false);
        selectCharacterPanel.SetActive(true);
    }

    public void GameStart()
    {
        NetworkManagerObject.SetActive(true);
        playerSetupCanvas.SetActive(false);

        if (!NetworkClient.active)
        {
            // Client + IP
            manager.StartClient();
            // This updates networkAddress every frame from the TextField
            manager.networkAddress = address;
        }

        NetworkManagerObject.GetComponent<NetworkManagerHUD>().enabled = false;
    }

    public void SelectCharacter(int avatar)
    {
        LocalPlayerData.avatarId = avatar;

        charactersUI[avatar].gameObject.SetActive(true);

        for(int i = 0; i < charactersUI.Length; i++)
        {
            if(i == avatar)
            {
                charactersUI[i].gameObject.SetActive(true);
            } 
            else
            {
                charactersUI[i].SetActive(false);
            }
        }
        
        startButton.interactable = true;
    }

    public void ServerStart()
    {
        if (!NetworkClient.active)
        {
            manager.StartServer();
        }
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
