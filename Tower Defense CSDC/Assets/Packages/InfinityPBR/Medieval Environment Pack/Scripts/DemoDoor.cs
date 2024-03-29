using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  InfinityPBR
{
    public class DemoDoor : MonoBehaviour
    {
        public AnimationClip open;
        public AnimationClip close;
        private Animation animation;

        private void Start()
        {
            animation = GetComponent<Animation>();
        }

        public void Open()
        {
            if (!animation)
                return;

            animation.clip = open;
            animation.Play();
        }
        
        public void Close()
        {
            if (!animation)
                return;

            animation.clip = close;
            animation.Play();
        }
    }
}

