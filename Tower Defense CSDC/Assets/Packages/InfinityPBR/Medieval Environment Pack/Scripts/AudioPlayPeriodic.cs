using UnityEngine;

namespace InfinityPBR
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayPeriodic : MonoBehaviour
    {
        public AudioClip[] audioClips;
        public bool randomizeClips = true;

        public float frequencyMin = 5f;
        public float frequencyMax = 20f;
        public float chanceOfPlaying = 60; // Out of 100

        private float counter;
        private float stopCounter;
        private float clipLength;
        
        private AudioSource _audioSource;
    
        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            counter = Reset();
            if (_audioSource.clip)
                clipLength = _audioSource.clip.length;
        }
        
        void Update()
        {
            if (!_audioSource || audioClips.Length == 0)
                return;
            
            counter -= Time.deltaTime;
            stopCounter -= Time.deltaTime;
            if (counter <= 0)
                TryPlayAudio();
            if (stopCounter <= 0)
                _audioSource.Stop();
        }

        private void TryPlayAudio()
        {
            counter = Reset();
            if (Random.Range(0, 100) <= chanceOfPlaying)
            {
                stopCounter = clipLength;
                _audioSource.Play();
            }
        }

        private float Reset()
        {
            _audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            return Random.Range(frequencyMin, frequencyMax);
        }

        private void OnValidate()
        {
            _audioSource = GetComponent<AudioSource>();
            if (frequencyMax < frequencyMin)
                frequencyMax = frequencyMin;
            if (_audioSource.clip)
            {
                if (_audioSource.clip.length > frequencyMin)
                    frequencyMin = _audioSource.clip.length + 0.1f;
            }
        }
    } 
}

