using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;

public class TakeAKey : InteractableObject
{
    public GameObject closedDoor;
    private ClosedDoorController closedDoorController;
    private void Awake()
    {
        closedDoorController = closedDoor.GetComponent<ClosedDoorController>();
    }
    protected override void Interact(PlayerManager player)
    {
        closedDoorController.playerHasAKey = true;
        Destroy(gameObject);
    }
}
