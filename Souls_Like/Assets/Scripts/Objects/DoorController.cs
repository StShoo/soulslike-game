using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : InteractableObject
{
    private Animator doorAnimator;
    private bool isOpen = false;
    protected bool notAvaliable = false;

    private void Awake()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
    }

    protected void PlayAnimation()
    {
        if (!isOpen)
        {
            doorAnimator.Play("OpenDoor");
            isOpen = true;
        }
        else
        {
            doorAnimator.Play("CloseDoor");
            isOpen = false;
        }
    }

    protected override void Interact(PlayerManager player)
    {
        if (!notAvaliable)
        {
            PlayAnimation();
            notAvaliable = true;
            StartCoroutine(Timedelay());
        }
    }

    protected IEnumerator Timedelay()
    {
        yield return new WaitForSeconds(1);
        notAvaliable = false;
    }
}
