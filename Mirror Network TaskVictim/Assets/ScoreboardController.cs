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

    [SyncVar(hook = nameof(OnRedScoreChanged))] public int RedScore;

    [SyncVar(hook = nameof(OnBlueScoreChanged))] public int BlueScore;

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
        //gameObject.name = gameObject.GetComponent<NetworkMatch>().matchId.ToString();
    }

    void Update()
    {
        RedScoreText.text = "Red : " + RedScore;
        BlueScoreText.text = "Blue : " + BlueScore;
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

    [Command(requiresAuthority = false)]
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
            if (score.teamScore >= 5)
            {
                Debug.Log(score.teamName + "Just passed target score");
                topTeamScoreText.text = score.teamName + " Win";
            }
            
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


}
