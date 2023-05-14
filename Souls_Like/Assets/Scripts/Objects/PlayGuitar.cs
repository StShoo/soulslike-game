using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SG;

public class PlayGuitar : InteractableObject
{
    [SerializeField] protected GameObject panel;

    public bool isElectricBoxOn = false;

    protected override void Interact(PlayerManager player)
    {

        if (isElectricBoxOn)
        {
            panel.GetComponent<Animator>().SetBool("goDark", true);
            StartCoroutine(Timedelay());
        }
        else
        {
            Debug.Log("Need to turn power on.");
        }
    }

    protected IEnumerator Timedelay()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
