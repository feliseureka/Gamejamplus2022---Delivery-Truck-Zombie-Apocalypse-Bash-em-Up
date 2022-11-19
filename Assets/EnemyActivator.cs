using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    [SerializeField] int distanceFromPlayer;
    GameObject player;
    public List<ActivatorEnemy> activatorEnemies;

    private void Start()
    {
        player = GameObject.Find("Car");
        activatorEnemies = new List<ActivatorEnemy>();

        StartCoroutine(CheckActivation());
    }

    IEnumerator CheckActivation()
    {
        List<ActivatorEnemy> removeList = new List<ActivatorEnemy>();

        if(activatorEnemies.Count > 0)
        {
            foreach  (ActivatorEnemy enemy in activatorEnemies)
            {
                if (Vector3.Distance(player.transform.position, enemy.enemyPos) > distanceFromPlayer)
                {
                    if (enemy.Object == null)
                        removeList.Add(enemy);
                    else
                        enemy.Object.SetActive(false);
                }
                else
                {
                    if (enemy.Object == null)
                        removeList.Add(enemy);
                    else
                        enemy.Object.SetActive(true);
                }
            }
        }

        yield return new WaitForSeconds(0.01f);

        if (removeList.Count > 0)
        {
            foreach (ActivatorEnemy enemy in removeList)
            {
                activatorEnemies.Remove(enemy);
            }
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(CheckActivation());
    }
}

[System.Serializable]
public class ActivatorEnemy
{
    public GameObject Object;
    public Vector3 enemyPos;
}
