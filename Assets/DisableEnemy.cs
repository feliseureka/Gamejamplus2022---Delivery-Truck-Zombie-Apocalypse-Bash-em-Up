using System.Collections;
using UnityEngine;

public class DisableEnemy : MonoBehaviour
{
    GameObject enemyActivator;
    EnemyActivator enemyActivatorScripts;

    private void Start()
    {
        enemyActivator = GameObject.Find("EnemyActivator");
        enemyActivatorScripts = enemyActivator.GetComponent<EnemyActivator>();

        enemyActivatorScripts.activatorEnemies.Add(new ActivatorEnemy { Object = this.gameObject, enemyPos = transform.position });
    }
}
