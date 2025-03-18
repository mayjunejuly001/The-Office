using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{

    public float flightSpeed = 2f;
    public DetectionZone biteDetectionZone;
    Rigidbody2D rb;

    Damageable damageable;

    public List<Transform> waypoints;

     Animator animator;

    Transform nextWaypoint;
    int waypointNum = 0;

    public bool _hasTarget;
    public float waypointReachedDistance = 0.1f;

    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {

            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }
    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }
    private void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;

    }
    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }

    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (canMove)
            {
                Flight();
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
            }
        }
    }
    
    

    private void Flight()
    {
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.linearVelocity = directionToWaypoint * flightSpeed;
        UpdateDirection();
        if (distance <= waypointReachedDistance)
        {
            waypointNum++;
            if (waypointNum >= waypoints.Count)
            {
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }


    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;
        if (transform.localScale.x > 0)
        {
            if (rb.linearVelocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            if (rb.linearVelocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }
}
