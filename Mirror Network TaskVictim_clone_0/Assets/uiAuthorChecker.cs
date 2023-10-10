using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class uiAuthorChecker : NetworkBehaviour
{
    public GameObject InputField;
    //playerList plyrlst;
    //public GameObject siCanvas;
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        //playerList plyrlst = GetComponent<playerList>();
        //plyrlst.findPlayers();
        //plyrlst.updateDataPlayer();

        UIcode UI = GetComponent<UIcode>();
        UI.enabled = true;

        InputField.SetActive(true);
    }
}
