using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : NetworkBehaviour
{
    // Buat enum untuk mewakili tim
    public enum Team
    {
        Red,
        Blue
    }

    // Dictionary untuk menyimpan daftar pemain dalam setiap tim
    private Dictionary<NetworkIdentity, Team> playerTeams = new Dictionary<NetworkIdentity, Team>();

    // Method ini akan memasukkan pemain ke dalam tim berdasarkan tag
    public void SetPlayerTeam(NetworkIdentity player, Team team)
    {
        playerTeams[player] = team;
        RpcUpdatePlayerTeam(player.gameObject, team);
    }

    // Method RPC untuk memperbarui tim pemain di seluruh jaringan
    [ClientRpc]
    private void RpcUpdatePlayerTeam(GameObject playerObj, Team team)
    {
        // Set warna atau tampilkan informasi tim di sini
        PlayerData playerData = playerObj.GetComponent<PlayerData>();
        if (playerData != null)
        {
            //playerData.SetTeamColor(team);
        }
    }
}
