using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform player;

    private void Start()
    {
        player = GetComponent<Transform>();
    }
}