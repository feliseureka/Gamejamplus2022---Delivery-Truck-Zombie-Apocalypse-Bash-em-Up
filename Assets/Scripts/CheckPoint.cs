using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] float spawnArea_height = 1f;
    [SerializeField] float spawnArea_width = 1f;
    [SerializeField] float waitTime = 2.5f;
    public GameObject checkPoint;
    public float timer = 60;
    public TMP_Text scoreGet;

    private ScoreManager scoreManager;

    void Start(){
        scoreManager = GameObject.Find("ScoreManager").transform.GetComponent<ScoreManager>();
        timer = 60;
    }

    void FixedUpdate(){
        if(timer > 0f){
            timer -= Time.deltaTime;
            scoreGet.text = timer.ToString("F2");
        }else if(timer < 0f){
            timer = 0;
        }
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
            int scoreLast = (int)(timer);
            timer = 60f;
            scoreManager.increaseScore(scoreLast);
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
