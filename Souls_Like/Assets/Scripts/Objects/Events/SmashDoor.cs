using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;

public class SmashDoor : BaseEventTrigger
{
    [SerializeField] protected GameObject doorToSmash;
    [SerializeField] protected GameObject monster;
    [SerializeField] protected AudioSource doorSmash;

    protected override void StartEvent()
    {
        doorToSmash.GetComponent<Animator>().SetBool("BreakOut", true);


        doorSmash.PlayDelayed(0.5f);
        monster.SetActive(true);
    }


}
