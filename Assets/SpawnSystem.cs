using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] float spawnArea_height = 1f;
    [SerializeField] float spawnArea_width = 1f;
    int length;
    float time;

    [SerializeField] GameObject[] spawn;
    [SerializeField] float probality;
    [SerializeField] int spawnCount;

    private int maxSpawn = 10000;
    public int zombieLevel = 1;

    private void Start()
    {
        length = spawn.Length;
        Spawn();
        StartCoroutine(spawnUpdate());
    }

    IEnumerator spawnUpdate(){
        spawnCount = Random.Range(0, 100);
        yield return new WaitForSeconds(1);
        if(maxSpawn >= spawn.Length){
            Spawn();
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(spawnUpdate());
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > 5f)
        {
            time = 0;
            zombieLevel++;
        }

    }

    void Spawn()
    {
        if (Random.value > probality) { return; }

        for (int i = 0; i < spawnCount; i++)
        {
            int id = Random.Range(0, length);
            GameObject go = Instantiate(spawn[id]);
            Transform t = go.transform;
            t.GetComponent<EnemyController>().ChangeDiff(zombieLevel);
            t.SetParent(transform);

            Vector3 position = transform.position;

            position.x += Random.Range(-spawnArea_width, spawnArea_width);
            position.z += Random.Range(-spawnArea_height, spawnArea_height);

            t.position = position;
           
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea_width * 2,  2, spawnArea_height * 2));
    }

}
