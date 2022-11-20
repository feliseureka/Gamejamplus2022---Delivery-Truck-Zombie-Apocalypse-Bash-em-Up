using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectPlacer : MonoBehaviour {

    public List<GameObject> objToPlace;
    public List<int> count;
    public float lim;

    private void Start() {
        for (int i = 0; i < objToPlace.Count; i++) {
            int c = count[i];
            var o = objToPlace[i];
            for (int k = 0; k < c; k++) {
                Instantiate(o, new Vector3(Random.Range(-lim, lim), Random.Range(1, 4), Random.Range(-lim, lim)), Quaternion.Euler(0, Random.Range(0, 360), 0));
            }
        }
    }
}
