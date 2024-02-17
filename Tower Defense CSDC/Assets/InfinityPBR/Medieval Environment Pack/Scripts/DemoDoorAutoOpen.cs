using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfinityPBR
{
    public class DemoDoorAutoOpen : MonoBehaviour
    {
        public DemoDoor[] doors;
        public LayerMask layerMask = -1;

        public void OnTriggerEnter(Collider other)
        {
            Debug.Log("Enter");
            if(1 << other.gameObject.layer != 0)
            {
                Debug.Log("Player Entered");
                Open();
            }
        }

        public void OnTriggerExit(Collider other)
        {
            Debug.Log("Exit");
            if(1 << other.gameObject.layer != 0)
            {
                Debug.Log("Player Exited");
                Close();
            }
        }

        private void Open()
        {
            foreach (DemoDoor demoDoor in doors)
            {
                if(demoDoor.gameObject.activeSelf)
                    demoDoor.Open();
            }
        }
        
        private void Close()
        {
            foreach (DemoDoor demoDoor in doors)
            {
                if(demoDoor.gameObject.activeSelf)
                    demoDoor.Close();
            }
        }
    }
}

