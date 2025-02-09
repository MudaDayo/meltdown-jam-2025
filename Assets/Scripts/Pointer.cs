using UnityEngine;
using System.Collections.Generic;

public class Pointer : MonoBehaviour
{
    [SerializeField] private List<Transform> _pointerTargets;
    private int _currentTargetIndex = 0;
    [SerializeField] private float _triggerRange;

    private void Update()
    {
        if (_pointerTargets == null) return;
        if (Vector3.Distance(transform.position, _pointerTargets[_currentTargetIndex].position) < _triggerRange)
        {
            _currentTargetIndex += 1;
            if (_currentTargetIndex > _pointerTargets.Count)
            {
                _currentTargetIndex = _pointerTargets.Count - 1;
                gameObject.SetActive(false);
            }
        }
        transform.right = _pointerTargets[_currentTargetIndex].position - transform.position;
    }
}
