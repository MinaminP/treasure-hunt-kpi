using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chooseTeamButton : MonoBehaviour
{
    public string teamName;
    // Start is called before the first frame update
    void Start()
    {
        teamName = "";
        PlayerPrefs.SetString("teamsName", "");
    }

    public void chooseRed()
    {
        teamName = "Red";
        PlayerPrefs.SetString("teamsName", teamName);
        SceneManager.LoadScene("Playground");
    }
    public void chooseBlue()
    {
        teamName = "Blue";
        PlayerPrefs.SetString("teamsName",teamName);
        SceneManager.LoadScene("Playground");
    }
    public void chooseGreen()
    {
        teamName = "Green";
        PlayerPrefs.SetString("teamsName", teamName);
        SceneManager.LoadScene("Playground");
    }
}
