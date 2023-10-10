using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class loginController : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public GameObject setNamePanel;
    public GameObject NetworkManagerObject;

    public void setTheName()
    {
        LocalPlayerData.playerUserName = nameInputField.text;
        Debug.Log(LocalPlayerData.playerUserName);
        NetworkManagerObject.SetActive(true);
        setNamePanel.SetActive(false);
    }
}