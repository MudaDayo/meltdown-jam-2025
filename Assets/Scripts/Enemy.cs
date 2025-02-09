using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField]
    private float timer, timeToChangeDirection, travelTime, travelTimer;

    [SerializeField] private PlayerRecorder _playerRecorder;
    private Vector3 _lastPosition;
    private Vector3 _targetPosition;

    public bool changingDirection = false;
    public bool traveling = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        /*
        if(traveling){
        timer += Time.deltaTime;
        if(timer > timeToChangeDirection)
        {
            timer = 0;
            traveling = false;
            changingDirection = true;
        }
        rb.linearVelocity = transform.forward * _movementSpeed;
        }
        else if(changingDirection){
            travelTimer += Time.deltaTime;
            transform.LookAt(player.transform);
            if(travelTimer > travelTime)
            {
                travelTimer = 0;
                changingDirection = false;
                traveling = true;
            }
        } 
        */
        timer += Time.deltaTime;
        if (timer >= travelTimer)
        {
            timer = 0;
            travelTime = 0;
            _lastPosition = transform.position;
            _targetPosition = _playerRecorder.Positions.Peek();
            _targetPosition.z = _lastPosition.z;
        }
        FollowPlayerRecorder();
    }

    void FollowPlayerRecorder()
    {
        travelTime += (1f /  travelTimer) * Time.deltaTime;
        if (_playerRecorder.IsReady())
        {
            //rb.MovePosition(rb.position + _playerRecorder.Positions.Peek() * timer);
            if (timer >= travelTimer)
                rb.position = _targetPosition;
            else
                rb.position = Vector3.Lerp(_lastPosition, _targetPosition, travelTime);
        }
        else
        {
            rb.linearVelocity = transform.InverseTransformPoint(player.transform.position).normalized * Time.deltaTime * _movementSpeed;
        }
    }
}
