using UnityEngine;

public class PlayerPreviousPosition : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed = 1f;
    [SerializeField] private Transform _target;
 
     void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, _target.position, _smoothSpeed);
    }
}
