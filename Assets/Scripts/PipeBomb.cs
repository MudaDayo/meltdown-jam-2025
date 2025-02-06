using UnityEngine;

public class PipeBomb : MonoBehaviour
{
    [SerializeField] private float _fuseTime;
    [SerializeField] private float _explosionDuration = 0.1f;
    [SerializeField] private GameObject _explosionPrefab;
    void Start()
    {
        Invoke("Explode", _fuseTime);
    }

    void Explode()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Invoke("Kill", _explosionDuration);
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
