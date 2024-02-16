using UnityEngine;

namespace InfinityPBR.Systems
{
    public class InteractiveDoor : MonoBehaviour
    {
        //Script checks if the door can be opened or closed
        public bool m_doorState = false;
        public AudioSource m_audioSource;
        public float desiredAngle = 0f;
        public float doorSpeed = 200f;

        private void Start()
        {
            if (m_audioSource == null)
            {
                m_audioSource = gameObject.AddComponent<AudioSource>();
            }

            m_audioSource.maxDistance = 15f;
            m_audioSource.dopplerLevel = 0f;
            m_audioSource.spatialBlend = 1f;
            m_audioSource.rolloffMode = AudioRolloffMode.Linear;
            m_audioSource.playOnAwake = false;
        }

        private void Update()
        {
            /*
            if (transform.eulerAngles.y != desiredAngle)
            {
                float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, desiredAngle, doorSpeed * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, newAngle, 0);
            }
            */
        }

        public void PlaySound(AudioClip audioClip, float volume = 1f)
        {
            if (m_audioSource != null)
            {
                if (audioClip == null)
                {
                    return;
                }

                m_audioSource.PlayOneShot(audioClip, volume);
            }
        }
    }
}