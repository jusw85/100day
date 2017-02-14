using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour {

    public GameObject spawnType;
    public bool runForever = false;
    public int spawnInstances = 1;
    public float spawnInterval = 5f;

    private bool isRunning = false;

    private void Update() {
        if (!isRunning && (runForever || spawnInstances > 0)) {
            StartCoroutine(SpawnCoroutine());
        }
    }

    private IEnumerator SpawnCoroutine() {
        isRunning = true;
        while (runForever || spawnInstances-- > 0) {
            Spawn();
            yield return new WaitForSeconds(spawnInterval);
        }
        isRunning = false;
    }

    public void Spawn() {
        Instantiate(spawnType, transform.position, Quaternion.identity);
        //GameObject obj = Instantiate(spawnType, transform.position, Quaternion.identity);
    }

}
