using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using System.Linq;

public class ScoreboardController : NetworkBehaviour
{
    private List<PlayerTeam> playerTeams = new List<PlayerTeam>();
    //public Text scoreText; // UI Text untuk menampilkan skor
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI topTeamScoreText;

    public TextMeshProUGUI RedScoreText;
    public TextMeshProUGUI BlueScoreText;
    //[SyncVar(hook = nameof(OnObjectNameChanged))]
    //public string ObjectName = gameObject.GetComponent<NetworkMatch>().matchId.ToString();

    public GameObject scoreBoardUI;

    [SyncVar(hook = nameof(OnRedScoreChanged))] public int RedScore;

    [SyncVar(hook = nameof(OnBlueScoreChanged))] public int BlueScore;

    [SyncVar(hook = nameof(OnEndChanged))] public bool isEnd = false;

    [System.Serializable]
    public class PlayerTeam
    {
        public string teamName;
        public int teamScore;
        //public string TeamName;
    }

    //start
    private void Start()
    {
        gameObject.name = "board" + gameObject.GetComponent<NetworkMatch>().matchId.ToString();
        //GameObject.Find("player" + gameObject.GetComponent<NetworkMatch>().matchId.ToString()).GetComponent<PlayerDataNew>();
        //GameObject.FindGameObjectsWithTag("Player");
        PlayerDataNew[] playerss = FindObjectsOfType<PlayerDataNew>();
        for (int i = 0; i < playerss.Length; i++)
        {
            if (playerss[i].name == "player" + gameObject.GetComponent<NetworkMatch>().matchId.ToString())
            {
                playerss[i].GetComponent<PlayerDataNew>().scoreboardController = this;
            }
        }
    }

    private void Update()
    {
        //scoreBoardUI.SetActive(false);
        if (Input.GetKey(KeyCode.Tab))
        {
            Debug.Log("Tab Pressed");
            //scoreBoardUI.SetActive(true);
        }
        RedScoreText.text = "Red : " + RedScore;
        BlueScoreText.text = "Blue : " + BlueScore;
        if (isEnd == true)
        {
            //gameObject.GetComponent<Animator>().Play("endgame");
        }
    }

    public void showw()
    {
        gameObject.GetComponent<Animator>().Play("endgame");
    }

    [Command(requiresAuthority = false)]
    public void setObjectName()
    {
        //ObjectName = gameObject.GetComponent<NetworkMatch>().matchId.ToString();
        //gameObject.name = ObjectName;
    }

    public void UpdateTeamScore(string teamName, int scoreChange)
    {
        PlayerTeam playerTeam = playerTeams.Find(x => x.teamName == teamName);

        if (playerTeam != null)
        {
            playerTeam.teamScore += scoreChange;
            //RpcUpdateTopTeamScore();
            //playerName = playerScore.playerName;
        }
        else
        {

            PlayerTeam newTeamScore = new PlayerTeam
            {
                teamName = teamName,
                teamScore = scoreChange
            };
            playerTeams.Add(newTeamScore);
        }

        //playerScore.score += scoreChange;

        // Memanggil Rpc untuk mengirim pembaruan skor kepada semua pemain
        RpcUpdateTeamScore(playerTeams.OrderByDescending(x => x.teamScore).ToList());


        //RpcUpdateTopTeamScore();
        //RpcUpdateTeamScore(newTeamScore.OrderByDescending(x => x.teamScore).FirstOrDefault());
    }


    [ClientRpc]
    private void RpcUpdateTeamScore(List<PlayerTeam> playerTeamList)
    {
        // Update UI Text hanya pada klien
        if (scoreText2 != null)
        {
            string scoreString = "Scores:\n";

            foreach (var playerTeamScore in playerTeamList)
            {
                scoreString += playerTeamScore.teamName + ": " + playerTeamScore.teamScore + "\n";
            }

            scoreText2.text = scoreString;
        }

        //Debug.Log("Total score : " + totalScore);

    }

    
    public void UpdateTopTeamScore()
    {
        //RpcUpdateTopTeamScore();
        RpcUpdateTeamScore2(playerTeams);

    }

    [ClientRpc]
    private void RpcUpdateTeamScore2(List<PlayerTeam> playerTeamList)
    {
        PlayerTeam score = playerTeamList.OrderByDescending(x => x.teamScore).FirstOrDefault();
        // Update UI Text hanya pada klien
        if (topTeamScoreText != null)
        {
     
            Debug.Log(score.teamName + "Just passed target score");
            topTeamScoreText.text = score.teamName + " Win";

        }

        //Debug.Log("Total score : " + totalScore);

    }

    [Command(requiresAuthority = false)]
    public void addRedScore()
    {
        
        //RedScore++;
        
        
        //RedScoreText.text = "Red : " + RedScore;
    }

    [Command(requiresAuthority = false)]
    public void addBlueScore()
    {
        //BlueScore++;
        
        //BlueScoreText.text = "Blue : " + BlueScore;
    }

    public void OnObjectNameChanged(string oldName, string newName)
    {
      
    }

    public void OnRedScoreChanged(int oldScore, int newScore)
    {
        //Debug.Log(PlayerName + " score updated from " + oldScore + " to " + newScore);
        //scoreboardController.UpdateTopTeamScore();
        //scoreboardController.UpdateScore(PlayerName, newScore);

        //RedScoreText.text = "Red : " + RedScore;
        Debug.Log("Score Updated");
    }

    public void OnBlueScoreChanged(int oldScore, int newScore)
    {
        //Debug.Log(PlayerName + " score updated from " + oldScore + " to " + newScore);
        //scoreboardController.UpdateTopTeamScore();
        //scoreboardController.UpdateScore(PlayerName, newScore);
        //BlueScoreText.text = "Blue : " + BlueScore;
        Debug.Log("Score Updated");
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private List<PlayerScore> playerScores = new List<PlayerScore>();
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI summaryRed;
    public TextMeshProUGUI summaryBlue;

    [System.Serializable]
    public class PlayerScore
    {
        public string playerName;
        public int score;
        public string tim;
    }



    public void addPlayer(string playerName, string tim)
    {
        // Cek apakah pemain sudah ada dalam daftar
        if (playerScores.Exists(x => x.playerName == playerName))
        {
            Debug.LogWarning("Player already exists in the scoreboard: " + playerName);
            return;
        }

        // Tambahkan pemain baru dengan skor awal 0
        PlayerScore newPlayerScore = new PlayerScore
        {
            playerName = playerName,
            score = 0,
            tim = tim
            //TeamName = Team
        };
        playerScores.Add(newPlayerScore);

        // Memanggil Rpc untuk mengirim pembaruan skor kepada semua pemain
        //RpcUpdateScore(playerScores.OrderByDescending(x => x.score).ToList());

        //RpcUpdateStatus(playerScores.OrderByDescending(x => x.score).ToList());

        RpcUpdateScoreSummary(playerScores.OrderByDescending(x => x.score).ToList());

        //playerCountTotal++;

        //playerScores.Clear();
    }

    public void UpdateScore(string playerName, int scoreChange)
    {
        PlayerScore playerScore = playerScores.Find(x => x.playerName == playerName);

        if (playerScore != null)
        {
            playerScore.score += scoreChange;
            //playerName = playerScore.playerName;
        }
        else
        {

            Debug.LogWarning("Player not found in the scoreboard: " + playerName);
        }

        //playerScore.score += scoreChange;

        // Memanggil Rpc untuk mengirim pembaruan skor kepada semua pemain
        //RpcUpdateScore(playerScores.OrderByDescending(x => x.score).ToList());

        RpcUpdateScoreSummary(playerScores.OrderByDescending(x => x.score).ToList());
        //RpcUpdateScoreSummary(playerScores.OrderByDescending(x => x.score).ToList(), playerTeams.OrderByDescending(x => x.teamScore).ToList());
    }

    [ClientRpc]
    private void RpcUpdateScoreSummary(List<PlayerScore> playerScoreList)
    {
        // Update UI Text hanya pada klien
        if (summaryRed != null)
        {
            //string scoreString = "Green:\n";
            string scoreString2 = "Red:\n";
            string scoreString3 = "Blue:\n";

            foreach (var playerScore in playerScoreList)
            {
                
                if (playerScore.tim == "Red")
                {
                    scoreString2 += playerScore.playerName + ": " + playerScore.score + "\n";
                }

                if (playerScore.tim == "Blue")
                {
                    scoreString3 += playerScore.playerName + ": " + playerScore.score + "\n";
                }


                //scoreString += playerScore.playerName + ": " + playerScore.score + "\n";
                //Debug.Log("tim " + playerScore.tim);
                //scoreString += playerScore.playerName + ": " + playerScore.score + "\n";
            }


            //summaryGreen.text = scoreString;
            summaryRed.text = scoreString2;
            summaryBlue.text = scoreString3;

        }

        //Debug.Log("Total score : " + totalScore);

    }

    public void OnEndChanged(bool oldVal, bool newVal)
    {

    }


}
