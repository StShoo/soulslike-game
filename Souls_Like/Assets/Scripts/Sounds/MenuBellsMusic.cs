using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBellsMusic : MonoBehaviour
{
    [SerializeField] private AudioSource straitBells;
    [SerializeField] private AudioSource rewerbBells;

    private void Awake()
    {
        PlayBells(straitBells);
        StartCoroutine(Timedelay());
    }

    protected IEnumerator Timedelay()
    {
        do
        {
            yield return new WaitForSeconds(16);
            PlayBells(rewerbBells);
            yield return new WaitForSeconds(16);
            PlayBells(straitBells);
        } while (true);
    }

    private void PlayBells(AudioSource musicToPlay)
    {
        musicToPlay.Play();
    }
}
