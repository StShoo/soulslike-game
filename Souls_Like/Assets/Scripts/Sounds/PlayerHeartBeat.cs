using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;

public class PlayerHeartBeat : MonoBehaviour
{

    [SerializeField] protected LayerMask detectionLayer;
    [SerializeField] protected AudioSource slowHeartBeat;
    [SerializeField] protected AudioSource fastHeartBeat;
    [SerializeField] protected AudioSource veryFastHeartBeat;

    public bool monsterIsFar;
    public bool monsterIsClose;
    public bool monsterIsVeryClose;

    void FixedUpdate()
    {
        FindMonster();

        if(!monsterIsFar && !monsterIsClose && !monsterIsVeryClose)
        {
            slowHeartBeat.Pause();
            fastHeartBeat.Pause();
            veryFastHeartBeat.Pause();
        } else if (monsterIsFar)
        {
            slowHeartBeat.Play();
            fastHeartBeat.Pause();
            veryFastHeartBeat.Pause();
        } else if (monsterIsClose) 
        {
            slowHeartBeat.Pause();
            fastHeartBeat.Play();
            veryFastHeartBeat.Pause();
        }
        else
        {
            slowHeartBeat.Pause();
            fastHeartBeat.Pause();
            veryFastHeartBeat.Play();
        }
    }

    public void FindMonster()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,
            20f, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            MonsterManager monster = colliders[i].transform.GetComponent<MonsterManager>();

            if (monster != null)
            {
                Vector3 targetDirection = transform.position - monster.transform.position;
                float distanceToTarget = Mathf.Sqrt(targetDirection.x * targetDirection.x +
                                                    targetDirection.z * targetDirection.z);

                if (distanceToTarget > 15f)
                {
                    monsterIsFar = false;
                    monsterIsClose = false;
                    monsterIsVeryClose = false;
                } else if (distanceToTarget > 10f && distanceToTarget < 15f)
                {
                    monsterIsFar = true;
                    monsterIsClose = false;
                    monsterIsVeryClose = false;
                } else if (distanceToTarget > 5f && distanceToTarget < 10f)
                {
                    monsterIsFar = false;
                    monsterIsClose = true;
                    monsterIsVeryClose = false;
                }
                else
                {
                    monsterIsFar = false;
                    monsterIsClose = false;
                    monsterIsVeryClose = true;
                }
            }
        }
    }
}