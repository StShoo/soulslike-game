using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;

public class SmashDoor : BaseEventTrigger
{
    [SerializeField] protected GameObject doorToSmash;
    [SerializeField] protected GameObject monster;

    protected override void StartEvent()
    {
        doorToSmash.GetComponent<Animator>().SetBool("BreakOut", true);
        monster.SetActive(true);
    }
}
