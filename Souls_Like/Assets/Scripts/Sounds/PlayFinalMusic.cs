using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFinalMusic : MonoBehaviour
{
    [SerializeField] AudioSource finalMusic;

    private void Awake()
    {
        StartCoroutine(Timedelay());
    }

    protected IEnumerator Timedelay()
    {
        yield return new WaitForSeconds(4);
        finalMusic.Play();
    }
}
