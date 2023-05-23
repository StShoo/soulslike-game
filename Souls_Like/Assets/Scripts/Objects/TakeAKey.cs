using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;

public class TakeAKey : InteractableObject
{
    public GameObject closedDoor;
    private ClosedDoorController closedDoorController;
    public AudioSource keysSound;

    private void Awake()
    {
        closedDoorController = closedDoor.GetComponent<ClosedDoorController>();
    }
    protected override void Interact(PlayerManager player)
    {
        closedDoorController.playerHasAKey = true;
        keysSound.Play();
        Destroy(gameObject);
    }
}
