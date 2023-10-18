using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace Mirror.Examples.MultipleMatch
{
    public class RoomGUI : MonoBehaviour
    {
        public GameObject playerList;
        public GameObject playerPrefab;
        public GameObject cancelButton;
        public GameObject leaveButton;
        public Button startButton;
        public bool owner;
        public Button timerDefine5;
        public Button timerDefine10;
        public Button timerDefine15;

        //[SyncVar(hook = "UpdateLocalName")] public string localName;
        //[SyncVar(hook = "UpdateLocalTime")] public float time;

        //[SyncVar(hook = nameof(UpdateLocalTime))] public float time;

        private void Start()
        {
            //localName = LocalPlayerData.playerUserName;
            //changeName();
        }


        
        public void changetimer(float timer)
        {
            //time = timer;
            LocalPlayerData.gametimer = timer;
        }

        [ClientCallback]
        public void RefreshRoomPlayers(PlayerInfo[] playerInfos)
        {
            foreach (Transform child in playerList.transform)
                Destroy(child.gameObject);

            startButton.interactable = false;
            bool everyoneReady = true;

            timerDefine5.interactable = false;
            timerDefine10.interactable = false;
            timerDefine15.interactable = false;

            
            foreach (PlayerInfo playerInfo in playerInfos)
            {
                GameObject newPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
                newPlayer.transform.SetParent(playerList.transform, false);
                newPlayer.GetComponent<PlayerGUI>().SetPlayerInfo(playerInfo);

                if (!playerInfo.ready)
                    everyoneReady = false;
            }

            startButton.interactable = everyoneReady && owner && (playerInfos.Length > 1);

            timerDefine5.interactable = owner;
            timerDefine10.interactable = owner;
            timerDefine15.interactable = owner;
        }

        [ClientCallback]
        public void SetOwner(bool owner)
        {
            this.owner = owner;
            LocalPlayerData.isOwner = owner;
            cancelButton.SetActive(owner);
            leaveButton.SetActive(!owner);
        }

      

        public void setBlue()
        {
            LocalPlayerData.playerTeam = "Blue";

            //bluButton.SetActive(false);
            //redButton.SetActive(false);
        }

        public void setRed()
        {
            LocalPlayerData.playerTeam = "Red";

            //bluButton.SetActive(false);
            //redButton.SetActive(false);
        }
    }
}
