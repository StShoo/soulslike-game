using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayerEvent : BaseEventTrigger
{
    [SerializeField]
    protected GameObject panel;
    [SerializeField]
    protected GameObject gameOverText;

    protected Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void StartEvent()
    {
        animator.SetBool("Attack", true);
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
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
