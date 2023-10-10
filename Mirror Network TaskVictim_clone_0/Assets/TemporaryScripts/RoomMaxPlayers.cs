using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace Mirror.Examples.MultipleMatch
{
    public class RoomMaxPlayers : NetworkBehaviour
    {
        public static RoomMaxPlayers instance;

        [SyncVar(hook = nameof(SetMaxPlayers))]
        public int maxRoomPlayers;
        public Button maxButton;

        void Start()
        {
            instance = this;
        }

        public void inputMaxPlayerValue(string userInput)
        {
            maxRoomPlayers = int.Parse(userInput);
            if (maxRoomPlayers > 60)
            {
                maxButton.interactable = false;
            }
            else
            {
                maxButton.interactable = true;
            }

            Debug.Log(userInput);
        }

        public void SetMaxPlayers(int oldVal, int newVal)
        {
            newVal = maxRoomPlayers;
            maxRoomPlayers = newVal;
        }
    }
}
