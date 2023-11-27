using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Mirror.Examples.MultipleMatch
{
    public class PlayerGUI : MonoBehaviour
    {
        public Text playerName;
        public TextMeshProUGUI playerNameTMP;
        public GameObject playerReadyIcon;

        //public loginController loginController;

        //[SyncVar(hook = nameof(SetPlayerName))]public string PlayerUserName;

        private void Start()
        {
            //localName = PlayerUserName;
            //CmdSetPlayerName(localName);
            //CmdSetPlayerName(localName);
            //Debug.Log("Name Should Be : " + localName);
        }

        [ClientCallback]
        public void SetPlayerInfo(PlayerInfo info)
        {
            //playerName.text = $"Player {info.playerIndex}";
            playerNameTMP.text = $"{info.playerName}";
            //CmdSetPlayerName(info.playerName);
            //loginController = GameObject.Find("Canvas (2)").GetComponent<loginController>();
            //playerName.text = loginController.PlayerUserName;
            //playerName.color = info.ready ? Color.green : Color.white;
            if (info.ready)
            {
                playerReadyIcon.SetActive(true);
            } else
            {
                playerReadyIcon.SetActive(false);
            }
        }

        /*[Command(requiresAuthority = false)]
        public void CmdSetPlayerName(string name)
        {
            PlayerUserName = name;
            Debug.Log(PlayerUserName);
        }

        public void SetPlayerName(string oldValue, string newValue)
        {
            newValue = PlayerUserName;
            Debug.Log(newValue);
            playerName.text = newValue;
        }*/
    }
}