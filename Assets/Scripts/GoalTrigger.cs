using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _triggerable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_triggerable.activeSelf)
            _triggerable.SetActive(true);
    }
}
