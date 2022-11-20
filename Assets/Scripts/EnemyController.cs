using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyType
{
    Boommer,
    Normal
}

public class EnemyController : MonoBehaviour {

    private SphereCollider detectionArea;
    private Transform player;
    private Rigidbody rb;
    bool isBoom;
    bool isNormal;
    PlayerStats playerStats;
    private MilestoneSystem milestone;
    private ScoreManager sc;

    [SerializeField] private float topSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float ignoreDistanceSq;
    [SerializeField] EnemyType enemyType;
    [SerializeField] int attack;
    [SerializeField] int health;

    private void Awake() {
        detectionArea = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        milestone = GameObject.Find("RManager").transform.GetComponent<MilestoneSystem>();
        sc = GameObject.Find("ScoreManager").transform.GetComponent<ScoreManager>();
    }

    private void FixedUpdate() {
        if (!player || Time.frameCount % 16 == 0) { return; }
        var dist = player.position - transform.position;
        rb.velocity = Vector3.Lerp(rb.velocity, topSpeed * dist.normalized, acceleration * Time.fixedDeltaTime);
        if (dist.sqrMagnitude > ignoreDistanceSq) {
            player = null;
        }

        switch (enemyType)
        {
            case EnemyType.Boommer:
                EnemyBoom();
                break;
            case EnemyType.Normal:
                StartCoroutine(NormalEnemy());
                break;
            default:
                break;
        }
    }

    IEnumerator NormalEnemy()
    {
        if (isNormal)
        {
            isNormal = false;
            TakeDamage(playerStats.atk);
            yield return new WaitForSeconds(2f);
            isNormal = true;
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    void EnemyBoom()
    {
        if (isBoom)
        {
            isBoom = false;
            playerStats.TakeDamage(attack * 2);
            Die();
        }
    }

    private void Die() {
        sc.increaseScore(1);
        MilestoneSystem.increaseProgress();
        Destroy(gameObject);
    }

    public void ChangeDiff(int level){
        health = health * (1+((level-1)/2));
        attack = attack * (1+((level-1)/2));
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            var sp = collision.gameObject.GetComponent<Rigidbody>().velocity;
            rb.AddForce(sp * 2, ForceMode.Impulse);
            isBoom = true;
            isNormal = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isNormal = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            player = other.transform;
            playerStats = other.GetComponent<PlayerStats>();
        }
    }
}