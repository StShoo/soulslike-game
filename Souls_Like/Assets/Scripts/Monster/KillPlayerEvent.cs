using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayerEvent : BaseEventTrigger
{
    [SerializeField]
    protected GameObject panel;
    protected GameObject gameOverText;

    protected override void StartEvent()
    {
        panel.GetComponent<Animator>().SetBool("goDark", true);
        StartCoroutine(Timedelay());
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartEvent();
        }
    }
    protected IEnumerator Timedelay()
    {
        yield return new WaitForSeconds(1);
        gameOverText.GetComponent<Animator>().SetBool("DeathScreen", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
