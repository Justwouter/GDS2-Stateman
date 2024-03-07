using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Bullet : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(despawnCooldown());
    }

    // Update is called once per frame
    void Update() {

    }


    IEnumerator despawnCooldown() {
        yield return new WaitForSecondsRealtime(5);
        Destroy(gameObject);
    }
}
