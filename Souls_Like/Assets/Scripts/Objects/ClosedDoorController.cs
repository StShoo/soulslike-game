using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;

public class ClosedDoorController : DoorController
{
    public bool playerHasAKey;
    protected override void Interact(PlayerManager player)
    {
        if (playerHasAKey)
        {
            base.Interact(player);
        }
        else
        {
            Debug.Log("Locked. Maybe there are a key somewhere?");
        }
    }
}
