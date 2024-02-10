using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShootingScript : MonoBehaviour
{
    [SerializeField] private Collider towerRadius;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawn;
    private List<GameObject> objPool;

    void Start() {
        objPool = new List<GameObject>();
    }
    void Update() {
        foreach (GameObject obj in objPool.ToArray()) {
            if (obj.GetComponent<HomingProjectile>().markForDestroy) {
                objPool.Remove(obj);
                Destroy(obj);
            }
        }
    }
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag.Equals("Enemy")) {
            GameObject temp = Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
            temp.GetComponent<HomingProjectile>().SetProjectileTarget(col.gameObject);
            objPool.Add(temp);
        }
    }
}
