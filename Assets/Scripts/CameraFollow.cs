using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed = 0.1f;
    [SerializeField] private Transform _target;
 
    private Vector3 _offset;
 
    private void Start()
    {
        _offset = transform.position;        
    }
 
    private void FixedUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, _target.position + _offset, _smoothSpeed);
    }
}
