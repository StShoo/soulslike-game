using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;

public class SmashDoor : BaseEventTrigger
{
    [SerializeField] protected GameObject doorToSmash;

    protected override void StartEvent()
    {
        doorToSmash.GetComponent<Animator>().SetBool("BreakOut", true);
    }
}
