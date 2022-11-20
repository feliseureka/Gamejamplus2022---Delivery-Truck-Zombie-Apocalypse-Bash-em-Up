using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] float spawnArea_height = 1f;
    [SerializeField] float spawnArea_width = 1f;
    [SerializeField] float waitTime = 2.5f;
    public GameObject checkPoint;

    private ScoreManager scoreManager;

    void Start(){
        scoreManager = GameObject.Find("ScoreManager").transform.GetComponent<ScoreManager>();
    }

    public void Spawn(){
        Vector3 rsp = new Vector3(Random.Range(-spawnArea_width, spawnArea_width), 1, Random.Range(-spawnArea_height, spawnArea_height));
        GameObject c = Instantiate(checkPoint);
        c.transform.position = rsp;
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            other.transform.GetComponent<PlayerStats>().recoverHealth();
            Spawn();
            scoreManager.increaseScore(10);
            Destroy(gameObject);
        }
    }


    IEnumerator Wait(Collider other) {
        yield return new WaitForSeconds(waitTime);
        other.transform.GetComponent<PlayerStats>().recoverHealth();
        Spawn();
        Destroy(gameObject);
        
    }
}
