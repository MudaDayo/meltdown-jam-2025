using UnityEngine;
using System.Collections.Generic;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> _triggerables = null;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _newClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _triggerables != null)
        {
            other.GetComponent<PlayerController>().panicMode = true;

            foreach (GameObject triggerable in _triggerables)
            {
                if (!triggerable.activeSelf)
                    triggerable.SetActive(true);
            }
            if (_newClip != null)
            {
                _audioSource.clip = _newClip;
                _audioSource.Play();
            }
        }
    }
}
