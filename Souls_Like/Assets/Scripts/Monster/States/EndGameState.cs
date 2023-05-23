using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SG;

public class EndGameState : IdleState
{
    public GameObject panel;
    public GameObject gameOverText;
    /*public AudioSource deathMusic;*/

    protected override void MoveTowardsNextIdleMovementPoint(MonsterManager monsterManager)
    {
        if (stateChangedFlag)
        {
            i = newIndexOfPoint;
            stateChangedFlag = false;
        }
        Debug.Log("Index: " + i);

        monsterManager.animator.SetFloat("Vertical", 1, 0.2f, Time.deltaTime);

        monsterManager.monsterNavMeshAgent.enabled = true;
        monsterManager.monsterNavMeshAgent.SetDestination(movementPathPoints[i].transform.position);

        monsterManager.transform.rotation = Quaternion.Slerp(monsterManager.transform.rotation,
            monsterManager.monsterNavMeshAgent.transform.rotation,
            monsterManager.rotationSpeed / Time.deltaTime);

        Vector3 pointDirection = transform.position - movementPathPoints[i].transform.position;
        float distanceToPoint = Mathf.Sqrt(pointDirection.x * pointDirection.x +
                                           pointDirection.z * pointDirection.z);


        if (distanceToPoint <= 1f)
        {
            if (i != movementPathPoints.Length - 1)
            {
                i++;
            }
            else
            {
                StopMovement();
                
                StartCoroutine(Timedelay());
            }
        }
    }

    protected IEnumerator Timedelay()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("Die", true);
        yield return new WaitForSeconds(5);
        animator.SetBool("Rise", true);
        yield return new WaitForSeconds(0.5F);
        panel.GetComponent<Animator>().SetBool("GoDark", true);
        yield return new WaitForSeconds(2);
        gameOverText.GetComponent<Animator>().SetBool("DeathScreen", true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
