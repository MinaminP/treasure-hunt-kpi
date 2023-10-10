using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skorCHecker : NetworkBehaviour
{
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        scoreAdder UI = GetComponent<scoreAdder>();
        UI.enabled = true;
    }
}

