using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float percentOfJourneyToComplete = 0.99f;
    [HideInInspector] public bool markForDestroy = false;
    private GameObject target;
    private float startTime;
    private float journeyLength;
    void Awake() {
        target = null;
    }
    void FixedUpdate()
    {
        if (target != null) {
            journeyLength = Vector3.Distance(this.gameObject.transform.position, target.transform.position);
            float distanceCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(this.gameObject.transform.position, target.transform.position, fractionOfJourney);
            if (fractionOfJourney > percentOfJourneyToComplete) markForDestroy = true;
        }
    }
    public void SetProjectileTarget(GameObject target) {
        this.target = target;
        initiateTargeting();
    }
    private void initiateTargeting() {
        startTime = Time.time;
    }

}
