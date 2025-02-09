using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Transform _pointerTarget;

    private void Update()
    {
        if (_pointerTarget == null) return;
        transform.right = _pointerTarget.position - transform.position;
    }
}
