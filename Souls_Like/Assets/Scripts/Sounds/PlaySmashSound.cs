using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySmashSound : MonoBehaviour
{
    [SerializeField] AudioSource smash;

    private void Awake()
    {
        StartCoroutine(Timedelay());
    }

    protected IEnumerator Timedelay()
    {
        yield return new WaitForSeconds(0.5f);
        smash.Play();
    }
}
