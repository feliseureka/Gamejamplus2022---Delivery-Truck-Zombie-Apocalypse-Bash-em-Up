using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSystem.Instance.PlayZombie();
    }
}
