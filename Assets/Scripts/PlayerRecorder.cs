using System.Collections.Generic;
using UnityEngine;

public class PlayerRecorder : MonoBehaviour
{
    [SerializeField] private int _maxPositions;
    [SerializeField] private Transform _playerTransform;
    private Queue<Vector3> _positions = new Queue<Vector3>();

    public Queue<Vector3> Positions { get { return _positions; } }

    // Update is called once per frame
    void LateUpdate()
    {
        _positions.Enqueue(_playerTransform.position);
        if (_positions.Count > _maxPositions)
            _positions.Dequeue();
    }

    public bool IsReady()
    {
        if (_positions.Count >= _maxPositions)
            return true;
        else
            return false;
    }
}
