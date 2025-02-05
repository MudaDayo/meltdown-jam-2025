using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Rigidbody rb;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerLagBehind");
    }

    void Update()
    {
        transform.LookAt(player.transform);
        rb.linearVelocity = transform.forward * _movementSpeed;
    }
}
