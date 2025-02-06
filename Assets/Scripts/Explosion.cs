using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField] private float _explosionForce, _explodeTime;

    void Start()
    {
        Invoke("Kill", _explodeTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Vector3 explosionDirection = transform.InverseTransformPoint(other.transform.position).normalized;
            // Debug.Log(explosionDirection);
            // other.GetComponent<Rigidbody>().AddForce(explosionDirection * _explosionForce);

            other.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, 0f, 0f, ForceMode.VelocityChange);
        }
        if(other.CompareTag("Breakable"))
        {
            other.GetComponent<DestroyablePlatform>().Kill();
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
