using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class UIcode : NetworkBehaviour
{
    



    public InputField nameField;

    public bool isMaster;

    public GameObject siCanvas;

    
    public TextMeshProUGUI playerNameUI;

    [SyncVar(hook = "DisplayPlayerName")] public string playerDisplayName;
    // Start is called before the first frame update
    void Start()
    {
        //siCanvas.GetComponent<playerList>().findPlayers();
        //siCanvas.GetComponent<playerList>().updateDataPlayer2();
        //StartCoroutine(siCanvas.GetComponent<playerList>().updateDataPlayer2());
    }

    

    public override void OnStartClient()
    {
        base.OnStartClient();
        //siCanvas.GetComponent<playerList>().updateDataPlayer2();
        //StartCoroutine(siCanvas.GetComponent<playerList>().updateDataPlayer2());
        //siCanvas.GetComponent<playerList>().findPlayers();
        Debug.Log("Just Joined bro");
        //DisplayPlayerName("default", nameField.text);
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        //DisplayPlayerName("default", nameField.text);
    }

    // Update is called once per frame
    void Update()
    {
        siCanvas = GameObject.FindWithTag("canvas");
        if (isLocalPlayer)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                CmdSendName(nameField.text);
                //siCanvas.GetComponent<playerList>().updateDataPlayer2();
                //StartCoroutine(siCanvas.GetComponent<playerList>().updateDataPlayer2());
                //CmdSendNameServer(nameField.text);
            }
            
            //SendRPCInternal("CmdSendName");
        }       

        
    }

    [Command]
    public void CmdSendName(string playerName)
    {
        playerDisplayName = playerName;

        // no rpc needed because the magic of SyncVar.
        // a syncvar listen to data that is on the server
        // thus, we change data on the server and ALL clients update the name with the use of the hook.
    }

    [Server]
    public void CmdSendNameServer(string playerName)
    {
        playerDisplayName = playerName;
    }

    public void DisplayPlayerName(string oldName, string newName)
    {
        Debug.Log("Player changed name from " + oldName + " to " + newName);

        playerNameUI.text = newName;
    }
}
