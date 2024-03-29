using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// This code is modified from: https://doctorllama.wordpress.com/2014/07/29/how-to-create-a-3d-sound-of-a-large-area-e-g-river-in-unity-3d/

namespace InfinityPBR
{
    public class FollowPlayerOnWaypoints : MonoBehaviour {
        
        public GameObject waypointParent;
        private List<Transform> waypoints = new List<Transform>();
        private Vector3[] waypointsPos;
        
        private Transform player;
        private Transform thisTransform;
        private Vector3 newPos;

        public float speedMod = 20f;
        public bool limitToAudioDistance = false;
        private AudioSource _audioSource;
 
        void Awake()
        {
            if (limitToAudioDistance)
            {
                _audioSource = GetComponent<AudioSource>();
                if (!_audioSource)
                {
                    Debug.LogError("No AudioSource found!");
                    limitToAudioDistance = false;
                }
            }

            foreach (Transform  child in waypointParent.transform)
            {
                waypoints.Add(child.transform);
            }
            
            waypointsPos = new Vector3[waypoints.Count];
            for (var i = 0; i < waypoints.Count; i++) {
                waypointsPos[i] = waypoints[i].position;
            }
 
            player = GameObject.FindGameObjectWithTag ("Player").transform;
            thisTransform = transform;
        }
        
        void Update () {
            waypointsPos = waypointsPos.OrderBy((d) => (d - player.position).sqrMagnitude).ToArray();

            if (PlayerIsOutOfAudioRange(waypointsPos[0], waypointsPos[1]))
            {
                return;
            }
            
            // get the two closest waypoints and find a point in between them
            newPos = Vector3.Lerp(thisTransform.position,
                ClosestPointOnLine(waypointsPos[0], waypointsPos[1], player.position), Time.deltaTime * speedMod);
            
            thisTransform.position = newPos;
        }

        private bool PlayerIsOutOfAudioRange(Vector3 pos1, Vector3 pos2)
        {
            if (!limitToAudioDistance)
                return false;

            if (Vector3.Distance(player.position, pos1) > _audioSource.maxDistance)
                return true;
            
            if (Vector3.Distance(player.position, pos2) > _audioSource.maxDistance)
                return true;
            
            return false;
        }
 
        // thanks to: http://forum.unity3d.com/threads/<span class="skimlinks-unlinked">math-problem.8114/#post-59715</span>
        Vector3 ClosestPointOnLine(Vector3 vA, Vector3 vB, Vector3 vPoint)
        {
            var vVector1 = vPoint - vA;
            var vVector2 = (vB - vA).normalized;
 
            var d = Vector3.Distance(vA, vB);
            var t = Vector3.Dot(vVector2, vVector1);
 
            if (t <= 0)
                return vA;
 
            if (t >= d)
                return vB;
 
            var vVector3 = vVector2 * t;
 
            var vClosestPoint = vA + vVector3;
 
            return vClosestPoint;
        }
    }

}
