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

    public bool changingDirection = false;
    public bool traveling = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
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
        FollowPlayerRecorder();
        
    }

    void FollowPlayerRecorder()
    {
        if (_playerRecorder.IsReady())
        {
            rb.MovePosition(_playerRecorder.Positions.Peek());
        }
    }
}
