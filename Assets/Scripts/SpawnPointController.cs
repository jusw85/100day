using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour {

    public GameObject spawnType;

    private void Awake() {
        GameObject obj = Instantiate(spawnType, transform.position, Quaternion.identity);
        //obj.GetComponent<DudeController>().respawnPoint = transform.position;
    }

}
