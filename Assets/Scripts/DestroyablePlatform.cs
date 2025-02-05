using UnityEngine;

public class DestroyablePlatform : MonoBehaviour
{
    [SerializeField] private float _timeToDie;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Invoke("Kill", _timeToDie);
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
