using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public virtual State Tick(MonsterManager monsterManager)
    {
        return this;
    }
}
