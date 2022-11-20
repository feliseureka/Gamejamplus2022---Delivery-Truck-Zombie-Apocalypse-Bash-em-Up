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
    PlayerStats playerStats;
    private MilestoneSystem milestone;
    private ScoreManager sc;

    [SerializeField] private float topSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float ignoreDistanceSq;
    [SerializeField] EnemyType enemyType;
    [SerializeField] int attack;
    [SerializeField] int health;
    [SerializeField] float knockBackForce = 30f;

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

    }

    IEnumerator NormalEnemyColl() {
        TakeDamage(playerStats.atk);
        yield return new WaitForSeconds(2f);
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    void EnemyBoomColl(Rigidbody player) {

        player.AddForce((player.position - transform.position).normalized * knockBackForce, ForceMode.Impulse);
        playerStats.TakeDamage(attack * 2);
        Die();

    }

    private void Die() {
        sc.increaseScore(1);
        MilestoneSystem.increaseProgress();
        Destroy(gameObject);
    }

    public void ChangeDiff(int level){
        health *= 1+((level-1)/2);
        attack *= 1+((level-1)/2);
        knockBackForce += (level - 1) * 2f;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            var p = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(p.velocity * 2, ForceMode.Impulse);

            switch (enemyType) {
                case EnemyType.Boommer:
                    EnemyBoomColl(p);
                    break;
                case EnemyType.Normal:
                    StartCoroutine(NormalEnemyColl());
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            player = other.transform;
            playerStats = other.GetComponent<PlayerStats>();
        }
    }
}