using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableMonstrObject : MonoBehaviour
{
    [SerializeField] private GameObject monster;

    private void Awake()
    {
        monster.SetActive(false);
        StartCoroutine(Timedelay());
    }

    protected IEnumerator Timedelay()
    {
        do
        {
            yield return new WaitForSeconds(10);
            monster.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            monster.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            monster.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            monster.SetActive(false);
        } while (true);
    }
}
