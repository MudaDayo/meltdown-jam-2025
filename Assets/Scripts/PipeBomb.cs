using UnityEngine;

public class PipeBomb : MonoBehaviour
{
    [SerializeField] private float _fuseTime;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionDuration = 0.1f;
    [SerializeField] private Transform _explosionTransform;
    [SerializeField] private Collider _explosionCollider;
    void Start()
    {
        Invoke("Explode", _fuseTime);
    }

    void Explode()
    {
        _explosionCollider.enabled = true;
        _explosionTransform.gameObject.SetActive(true);
        Invoke("Kill", _explosionDuration);
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 explosionDirection = transform.InverseTransformPoint(other.transform.position).normalized;
            Debug.Log(explosionDirection);
            other.GetComponent<Rigidbody>().AddForce(explosionDirection * _explosionForce);
        }
    }
}
