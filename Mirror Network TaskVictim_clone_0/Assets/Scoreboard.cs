using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class Scoreboard : NetworkBehaviour
{
    private List<PlayerScore> playerScores = new List<PlayerScore>();
    //public Text scoreText; // UI Text untuk menampilkan skor
    public TextMeshProUGUI scoreText;

    [System.Serializable]
    public class PlayerScore
    {
        public string playerName;
        public int score;
    }

    [Command(requiresAuthority = false)]
    public void UpdateScore(string playerName, int scoreChange)
    {
        PlayerScore playerScore = playerScores.Find(x => x.playerName == playerName);

        if (playerScore != null)
        {
            playerScore.score += scoreChange;
        }
        else
        {
            PlayerScore newPlayerScore = new PlayerScore
            {
                playerName = playerName,
                score = scoreChange
            };
            playerScores.Add(newPlayerScore);
        }

        // Memanggil Rpc untuk mengirim pembaruan skor kepada semua pemain
        RpcUpdateScore(playerScores.OrderByDescending(x => x.score).ToList());
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
                scoreString += playerScore.playerName + ": " + playerScore.score + "\n";
            }

            scoreText.text = scoreString;
        }
    }

  
}
