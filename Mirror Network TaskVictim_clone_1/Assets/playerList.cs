using JetBrains.Annotations;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerList : NetworkBehaviour
{

    public GameObject[] players;
    public GameObject[] playerListItem;

    //public List<GameObject> playerListItemList;
    public GameObject playerListContentObject;
    public Transform playerListContent;
    public GameObject playerListItemt;

    public GameObject[] playerName;

    //public TextMeshProUGUI[] scores;
    //public List<TextMeshProUGUI> scores = new List<TextMeshProUGUI>();

    //public List<TMP_Text> scores = new List<TMP_Text>();
    public TextMeshProUGUI score;
    public scoreAdder scoreAdd;
    //public TextMeshProUGUI scores;
    //Player


    //public TextMeshProUGUI scores2;
    // Start is called before the first frame update
    void Start()
    {
        findPlayers();
        updateDataPlayer();
        //StartCoroutine(updateDataPlayer2());
    }

    // Update is called once per frame
    void Update()
    {
       
        //players = GameObject.FindGameObjectsWithTag("Player");
        //playerName = GameObject.FindGameObjectsWithTag("localUI");
        //score = GameObject.FindGameObjectsWithTag("a");
        //playerListItem = GameObject.FindGameObjectsWithTag("PlayerListItem");


        //TextMeshProUGUI[] scores = playerListItem[].GetComponents<TextMeshProUGUI>();
        //updateDataPlayer();
        //StartCoroutine(updateDataPlayer2());
        //findPlayers();
        
        //tambahSkor();
        
    }

    public void findPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        playerName = GameObject.FindGameObjectsWithTag("localUI");
    }

    [Server]
    public void updateDataPlayer()
    {
        //players = GameObject.FindGameObjectsWithTag("localUI");
        findPlayers();
        //playerListItem.GetComponent<TextMeshProUGUI>();
        score.text = "";
        for (int i = 0; i < players.Length; i++)
        {

            /*for (int j = 0; j < playerName.Length; j++)
            {
                score.text = score.text + "\n" + "Player" + i + " : " + players[i].GetComponent<scoreAdder>().score;
                //score.text = score.text + "\n" + playerName[j].GetComponent<UIcode>().playerDisplayName + " : " + players[i].GetComponent<scoreAdder>().score;
            }*/

            score.text = score.text + "\n" + playerName[i].GetComponent<UIcode>().playerDisplayName + " : " + players[i].GetComponent<scoreAdder>().score;
            //scores = playerListItem[i].GetComponents<TextMeshProUGUI>();
            //score.text = "Player" + i + " : " + players[i].GetComponent<scoreAdder>().score;

            //score.text = score.text + "\n" + players[i].GetComponent<UIcode>().playerDisplayName + " : " + players[i].GetComponent<scoreAdder>().score;

            //players[i].getchild
            //Debug.Log("Player" + i + " : " + players[i].GetComponent<scoreAdder>().score);

        }

        
        


        //scores = playerListItem.GetComponent<TextMeshProUGUI>();

        //Scores = playerListItem.GetComponent<TextMeshProUGUI>();
        //scores = (playerListItem.GetComponent<TextMeshProUGUI>());

        /*for (int i = 0; i < players.Length; i++)
        {
            //Instantiate(playerListItem, playerListContent);
            //TextMeshProUGUI[] scores = playerListItem[i].GetComponents<TextMeshProUGUI>();

            for (int j = 0; j < playerListItem.Length; j++)
            {
                TextMeshProUGUI[] scores = playerListItem[j].GetComponents<TextMeshProUGUI>();
                scores[j].text = "Skor P1 : " + players[j].GetComponent<scoreAdder>().score;
                //scores[1].text = "Skor P1 : " + players[1].GetComponent<scoreAdder>().score;
            }
            //scores[i].text = "Skor P1 : " + players[i].GetComponent<scoreAdder>().score;

            //scores.text = "Skor P1: " + players[0].GetComponent<scoreAdder>().score;
            //scores2.text = "Skor P2 : " + players[1].GetComponent<scoreAdder>().score;
        }*/
    }

    [Client]
    public IEnumerator updateDataPlayer2()
    {
        //players = GameObject.FindGameObjectsWithTag("localUI");
        findPlayers();
        //playerListItem.GetComponent<TextMeshProUGUI>();
        score.text = "";
        for (int i = 0; i < players.Length; i++)
        {

            /*for (int j = 0; j < playerName.Length; j++)
            {
                score.text = score.text + "\n" + "Player" + i + " : " + players[i].GetComponent<scoreAdder>().score;
                //score.text = score.text + "\n" + playerName[j].GetComponent<UIcode>().playerDisplayName + " : " + players[i].GetComponent<scoreAdder>().score;
            }*/

            score.text = score.text + "\n" + playerName[i].GetComponent<UIcode>().playerDisplayName + " : " + players[i].GetComponent<scoreAdder>().score;
            //scores = playerListItem[i].GetComponents<TextMeshProUGUI>();
            //score.text = "Player" + i + " : " + players[i].GetComponent<scoreAdder>().score;

            //score.text = score.text + "\n" + players[i].GetComponent<UIcode>().playerDisplayName + " : " + players[i].GetComponent<scoreAdder>().score;

            //players[i].getchild
            //Debug.Log("Player" + i + " : " + players[i].GetComponent<scoreAdder>().score);

        }
        yield return new WaitForSeconds(2f);
    }
    
}
