using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float walkSpeed = 3f;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    
    Rigidbody2D rb;
    TouchingDirections touchingDirections;


    Animator animator;
    public Damageable damageable;

    public enum WalkableDirection {Right , Left };

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if(value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }

    }

    public bool _hasTarget = false;
    public float walkStopRate = 0.1f;

    public bool HasTarget { get
        {
            return _hasTarget;
        } private set
        {
            
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        } }


    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
       
    }

    // Update is called once per frame

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall )
        {
            FlipDirection();
        }

        if (canMove) 
        {
            rb.linearVelocity = new Vector2(walkSpeed * walkDirectionVector.x , rb.linearVelocityY);
        }
        else if(!canMove)
        {
            rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, walkStopRate), rb.linearVelocity.y);
        }

       

    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }else if( WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("current values are not right or left");
        }
    }

    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}
