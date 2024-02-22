using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTowerShooting : MonoBehaviour
{
    [SerializeField] private SphereCollider detectionCollider;
    [SerializeField] private float detectionMaximumRadius = 15f;
    [SerializeField] private String[] tagsToCheck;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawn;
    // BULLET BEHAVIOR
    [SerializeField] private IProjectile.ProjectileType type;
    [SerializeField] private float baseDamage;
    [SerializeField] private float projectileSpeed = 10f;
    private List<objData> objectsToTrack;
    private List<GameObject> projectilePool;
    private struct objData {
        public GameObject obj;
        public bool isTracking;
    }

    void Start() {
        detectionCollider.radius = detectionMaximumRadius;
        objectsToTrack = new List<objData>();
        projectilePool = new List<GameObject>();
    }
    void Update() {
        foreach (objData candidate in objectsToTrack.ToArray()) {
            if (candidate.obj == null) {
                int objIndex = objectsToTrack.IndexOf(candidate);
                objectsToTrack.Remove(candidate); // remove nullable object + bullet
                Destroy(projectilePool[objIndex]);
                projectilePool.RemoveAt(objIndex);
            }
            else if (!candidate.isTracking) {
                // shoot to object
                int index = objectsToTrack.IndexOf(candidate);
                objData tempCandidate = candidate;
                objectsToTrack.Remove(candidate);
                tempCandidate.isTracking = true;
                objectsToTrack.Add(tempCandidate);
                GameObject instantiated = Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity);
                instantiated.GetComponent<IceProjectileBehavior>().SetProperties(type, baseDamage, projectileSpeed);
                projectilePool.Add(instantiated);
            }
        }
    }
    void FixedUpdate() {
        foreach(GameObject proj in projectilePool.ToArray()) {
            int index = projectilePool.IndexOf(proj);
            moveToObject(objectsToTrack[index].obj, proj, projectileSpeed);
            if (proj.GetComponent<IceProjectileBehavior>().markForDestroy) { // for ice towers
                GameObject temp = proj;
                projectilePool.Remove(proj);
                Destroy(temp);
            }
        }
    }
    void OnTriggerEnter(Collider col) {
        foreach (String tag in tagsToCheck) {
            if (col.gameObject.tag.Equals(tag)) { // if gameobject has a specific tag, we track it
                objData candidate = new objData();
                candidate.obj = col.gameObject;
                candidate.isTracking = false;
                objectsToTrack.Add(candidate);
            }
        }
    }
    private static void moveToObject(GameObject target, GameObject move, float speed) {
        if (target != null) {
            Vector3 direction = target.transform.position - move.transform.position;
            direction.Normalize();
            float moveDistance = speed * Time.fixedDeltaTime;
            Vector3 newPosition = move.transform.position + (direction * moveDistance);
            move.transform.position = newPosition;
        }
    }
}
