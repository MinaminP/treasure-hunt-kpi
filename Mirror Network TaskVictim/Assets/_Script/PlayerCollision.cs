using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCollision : NetworkBehaviour
{
    public RandomSpawnTreasure random;
    public PlayerDataNew playerData;

    private void Start()
    {
        random = GameObject.FindWithTag("random").GetComponent<RandomSpawnTreasure>();
        playerData = GetComponent<PlayerDataNew>();
    }

    // Start is called before the first frame update
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        interact interact = other.GetComponent<interact>();
        if (other.CompareTag("Treasure"))
        {
            if (isLocalPlayer)
            {
                if (interact.isActive)
                {
                    random.notifText.gameObject.SetActive(true);
                    if (playerData.PlayerTeamName == "Red")
                    {
                        if (interact.isRedFirst)
                        {
                            random.notifText.text = "Press \"E\" to collect the treasure";
                        }
                        else if (interact.isBlueFirst)
                        {
                            random.notifText.text = "Gather all your team to collect the treasure";
                        }
                    }

                    if (playerData.PlayerTeamName == "Blue")
                    {
                        if (interact.isBlueFirst)
                        {
                            random.notifText.text = "Press \"E\" to collect the treasure";
                        }
                        else if (interact.isRedFirst)
                        {
                            random.notifText.text = "Gather all your team to collect the treasure";
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        interact interact = other.GetComponent<interact>();
        if (other.CompareTag("Treasure"))
        {
            if (isLocalPlayer)
            {
                if (interact.isActive)
                {
                    random.notifText.gameObject.SetActive(true);
                    if (playerData.PlayerTeamName == "Red")
                    {
                        if (interact.isRedFirst)
                        {
                            random.notifText.text = "Press \"E\" to collect the treasure";
                        }
                        else if (interact.isBlueFirst)
                        {
                            random.notifText.text = "Gather all your team to collect the treasure";
                        }
                    }

                    if (playerData.PlayerTeamName == "Blue")
                    {
                        if (interact.isBlueFirst)
                        {
                            random.notifText.text = "Press \"E\" to collect the treasure";
                        }
                        else if (interact.isRedFirst)
                        {
                            random.notifText.text = "Gather all your team to collect the treasure";
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Treasure"))
        {
            if(isLocalPlayer)
            {
                random.notifText.gameObject.SetActive(false);
            }
        }
    }
}
