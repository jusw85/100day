using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

    private Dictionary<int, Queue<ObjectInstance>> poolDictionary = new Dictionary<int, Queue<ObjectInstance>>();

    public void CreatePool(GameObject prefab, int poolSize) {
        int poolKey = prefab.GetInstanceID();

        if (!poolDictionary.ContainsKey(poolKey)) {
            poolDictionary.Add(poolKey, new Queue<ObjectInstance>());

            GameObject poolHolder = new GameObject(prefab.name + " pool");
            poolHolder.transform.parent = transform;

            for (int i = 0; i < poolSize; i++) {
                ObjectInstance newObject = new ObjectInstance(Instantiate(prefab) as GameObject);
                poolDictionary[poolKey].Enqueue(newObject);
                newObject.SetParent(poolHolder.transform);
            }
        }
    }

    public GameObject ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation) {
        int poolKey = prefab.GetInstanceID();
        if (poolDictionary.ContainsKey(poolKey)) {
            ObjectInstance objectToReuse = poolDictionary[poolKey].Dequeue();
            poolDictionary[poolKey].Enqueue(objectToReuse);

            objectToReuse.Reuse(position, rotation);
            return objectToReuse.gameObject;
        }
        return null;
    }

    public class ObjectInstance {

        public GameObject gameObject;
        public Transform transform;
        private PoolObject poolObjectScript;

        public ObjectInstance(GameObject gameObject) {
            this.gameObject = gameObject;
            transform = gameObject.transform;
            poolObjectScript = gameObject.GetComponent<PoolObject>();

            gameObject.SetActive(false);
        }

        public void Reuse(Vector3 position, Quaternion rotation) {
            gameObject.SetActive(true);
            transform.position = position;
            transform.rotation = rotation;

            if (poolObjectScript != null) {
                poolObjectScript.OnObjectReuse();
            }
        }

        public void SetParent(Transform parent) {
            transform.parent = parent;
        }
    }
}
