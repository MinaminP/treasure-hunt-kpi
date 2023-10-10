using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static Scoreboard;
using static ScoreBoardNew;

public class ScoreBoardNew : NetworkBehaviour
{
    /*
    public TextMeshProUGUI scoreboardText;

    // List of player data
    public List<PlayerData> playerDataList = new List<PlayerData>();

    [ClientRpc]
    // Update the scoreboard UI
    public void UpdateScoreboard()
    {
        playerDataList.Sort((a, b) => b.PlayerScore.CompareTo(a.PlayerScore));

        string scoreboardString = "Scoreboard:\n";
        foreach (PlayerData playerData in playerDataList)
        {
            scoreboardString += $"{playerData.PlayerName}: {playerData.PlayerScore}\n";
        }

        scoreboardText.text = scoreboardString;
    }

    [Command(requiresAuthority = false)]
    // Add a player to the scoreboard
    public void AddPlayer(PlayerData playerData)
    {
        playerDataList.Add(playerData);
        //UpdateScoreboard();
    }

    [Command(requiresAuthority = false)]
    // Remove a player from the scoreboard
    public void RemovePlayer(PlayerData playerData)
    {
        playerDataList.Remove(playerData);
        //UpdateScoreboard();
    }

    private void Update()
    {
        UpdateScoreboard();
    }*/

    [SyncVar(hook = nameof(OnCountChanged))] public int playerCountTotal = 0;
    [SyncVar(hook = nameof(OnStatusCountChanged))] public int playerReadyTotal = 0;

    //public CountdownTimer ctimer;


    private List<PlayerScore> playerScores = new List<PlayerScore>();
    //public Text scoreText; // UI Text untuk menampilkan skor
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI statusText;

    public TextMeshProUGUI summaryGreen;
    public TextMeshProUGUI summaryBlue;
    public TextMeshProUGUI summaryRed;

    [System.Serializable]
    public class PlayerScore
    {
        public string playerName;
        public int score;
        public bool isReady;
        public string tim;
    }

    [Command(requiresAuthority = false)]
    public void UpdateScore(string playerName, int scoreChange)
    {
        PlayerScore playerScore = playerScores.Find(x => x.playerName == playerName);

        if (playerScore != null)
        {
            playerScore.score = scoreChange;
            //playerName = playerScore.playerName;
        }
        else
        {
            
            Debug.LogWarning("Player not found in the scoreboard: " + playerName);
        }

        //playerScore.score += scoreChange;

        // Memanggil Rpc untuk mengirim pembaruan skor kepada semua pemain
        RpcUpdateScore(playerScores.OrderByDescending(x => x.score).ToList());

        RpcUpdateScoreSummary(playerScores.OrderByDescending(x => x.score).ToList());
        //RpcUpdateScoreSummary(playerScores.OrderByDescending(x => x.score).ToList(), playerTeams.OrderByDescending(x => x.teamScore).ToList());
    }

    [Command(requiresAuthority = false)]
    public void addPlayer(string playerName, bool status, string tim)
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
            isReady = status,
            tim = tim
            //TeamName = Team
        };
        playerScores.Add(newPlayerScore);

        // Memanggil Rpc untuk mengirim pembaruan skor kepada semua pemain
        RpcUpdateScore(playerScores.OrderByDescending(x => x.score).ToList());

        RpcUpdateStatus(playerScores.OrderByDescending(x => x.score).ToList());

        RpcUpdateScoreSummary(playerScores.OrderByDescending(x => x.score).ToList());

        //playerCountTotal++;

        //playerScores.Clear();
    }

    [Command(requiresAuthority = false)]
    public void UpdateName(string oldName, string newName)
    {
        PlayerScore playerScore = playerScores.Find(x => x.playerName == oldName);

        if (playerScore != null)
        {
            playerScore.playerName = newName;
        }
        else
        {
            Debug.LogWarning("Player not found in the scoreboard: " + oldName);
        }

        // Memanggil Rpc untuk mengirim pembaruan nama kepada semua pemain
        RpcUpdateScore(playerScores.OrderByDescending(x => x.score).ToList());
        RpcUpdateStatus(playerScores.OrderByDescending(x => x.score).ToList());
        RpcUpdateScoreSummary(playerScores.OrderByDescending(x => x.score).ToList());

        //RpcUpdateTotalScore(teamName, GetTotalScoreForTeam(teamName));
    }

    public void UpdateSummaryTeam(string playerName, string newTim)
    {
        PlayerScore playerScore = playerScores.Find(x => x.playerName == playerName);

        if (playerScore != null)
        {
            playerScore.tim = newTim;
        }
        else
        {
            Debug.LogWarning("Player not found in the scoreboard: " + playerName);
        }
        RpcUpdateScore(playerScores.OrderByDescending(x => x.score).ToList());
        RpcUpdateStatus(playerScores.OrderByDescending(x => x.score).ToList());
        RpcUpdateScoreSummary(playerScores.OrderByDescending(x => x.score).ToList());
    }


    [Command(requiresAuthority = false)]
    public void UpdateStatus(string playerName, bool status)
    {
        PlayerScore playerScore = playerScores.Find(x => x.playerName == playerName);

        if (playerScore != null)
        {
            playerScore.isReady = status;
            //playerReadyTotal++;
        }
        else
        {
            Debug.LogWarning("Player not found in the scoreboard: " + playerName);
        }

        // Memanggil Rpc untuk mengirim pembaruan nama kepada semua pemain
        RpcUpdateScore(playerScores.OrderByDescending(x => x.score).ToList());
        RpcUpdateStatus(playerScores.OrderByDescending(x => x.score).ToList());
        RpcUpdateScoreSummary(playerScores.OrderByDescending(x => x.score).ToList());

        //RpcUpdateTotalScore(teamName, GetTotalScoreForTeam(teamName));
    }


    [ClientRpc]
    private void RpcUpdateScore(List<PlayerScore> playerScoreList)
    {
        // Update UI Text hanya pada klien
        if (scoreText != null)
        {
            string scoreString = "Scores:\n";

            foreach (var playerScore in playerScoreList)
            {
                scoreString += playerScore.playerName + playerScore.tim + ": " + playerScore.score + "\n";
            }

            scoreText.text = scoreString;
        }

        //Debug.Log("Total score : " + totalScore);

    }

    [ClientRpc]
    private void RpcUpdateScoreSummary(List<PlayerScore> playerScoreList)
    {
        // Update UI Text hanya pada klien
        if (summaryGreen != null)
        {
            string scoreString = "Green:\n";
            string scoreString2 = "Red:\n";
            string scoreString3 = "Blue:\n";

            foreach (var playerScore in playerScoreList)
            {
                if (playerScore.tim == "Green")
                {
                    scoreString += playerScore.playerName + ": " + playerScore.score + "\n";
                }

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


            summaryGreen.text = scoreString;
            summaryRed.text = scoreString2;
            summaryBlue.text = scoreString3;
            
        }

        //Debug.Log("Total score : " + totalScore);

    }

    [ClientRpc]
    private void RpcUpdateStatus(List<PlayerScore> playerScoreList)
    {
        // Update UI Text hanya pada klien
        if (statusText != null)
        {
            string scoreString = "Lobby Manager\n";

            foreach (var playerScore in playerScoreList)
            {
                //scoreString += playerScore.playerName + ": " + playerScore.isReady + "\n";
                if (playerScore.isReady == true)
                {
                    scoreString += playerScore.playerName + ": " + "Ready\n";
                }else if (playerScore.isReady == false)
                {
                    scoreString += playerScore.playerName + ": " + "Waiting\n";
                }
            }

            statusText.text = scoreString;
        }

        //Debug.Log("Total score : " + totalScore);

    }



    //team scoreboard

    private List<PlayerTeam> playerTeams = new List<PlayerTeam>();
    //public Text scoreText; // UI Text untuk menampilkan skor
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI topTeamScoreText;

    [System.Serializable]
    public class PlayerTeam
    {
        public string teamName;
        public int teamScore;
        //public string TeamName;
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

    
    private PlayerTeam GetTopTeam()
    {
        return playerTeams.OrderByDescending(x => x.teamScore).FirstOrDefault();
    }

    [Command(requiresAuthority = false)]
    public void UpdateTopTeamScore()
    {
        //RpcUpdateTopTeamScore();
        RpcUpdateTeamScore2(playerTeams);

    }

    [Command(requiresAuthority = false)]
    public void UpdatePreWinner()
    {
        RpcUpdatePreWin(playerTeams);
    }

    
    public void benerUpdate()
    {
        RpcUpdateTopTeamScore();
    }

    [ClientRpc]
    public void RpcUpdateTopTeamScore()
    {
        PlayerTeam topTeam = playerTeams.OrderByDescending(x => x.teamName).FirstOrDefault();
        if (topTeam != null)
        {
            topTeamScoreText.text = "Winner : " + topTeam.teamName + " - " + topTeam.teamScore.ToString();
            if (topTeam.teamScore >= 5)
            {
                Debug.Log(topTeam.teamName + "Just passed target score");
            }
            
        }
        else
        {
            topTeamScoreText.text = "No Top Team";
            //topTeamScoreText.text = "Winner : " + topTeam.teamName + " - " + topTeam.teamScore.ToString();
        }
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
            }
            topTeamScoreText.text = score.teamName + " Win";
        }

        //Debug.Log("Total score : " + totalScore);

    }

    [ClientRpc]
    private void RpcUpdatePreWin(List<PlayerTeam> playerTeamList)
    {
        PlayerTeam score = playerTeamList.OrderByDescending(x => x.teamScore).FirstOrDefault();
        // Update UI Text hanya pada klien
        if (topTeamScoreText != null)
        {
            topTeamScoreText.text = "Winner : " + score.teamName;
        }
    }





    //Lobby Board

    public void OnCountChanged(int oldCount, int newCount)
    {

    }

    public void OnStatusCountChanged(int oldCount, int newCount)
    {

    }
}
