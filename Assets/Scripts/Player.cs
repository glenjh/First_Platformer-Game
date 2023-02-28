using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerStates
{
    Idle,
    Run,
    Climb
}

public class Player : MonoBehaviour
{
    public Animator anim;
    public CapsuleCollider2D capsuleCollider2D;
    public BoxCollider2D playerFeet;
    public Rigidbody2D rigid;
    public SpriteRenderer sprite;

    public float runSpeed = 4;
    public float jumpForce = 5;
    public float climbSpeed = 3;
    public Vector2 move;
    public Vector2 climb;
    public PlayerStates PS;
    public PlayerState _state;

    void Init()
    {
        anim = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        playerFeet = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        _state = new IdleState();
    }
    void Start()
    {
        Init();
    }
    
    void Update()
    {
        Debug.Log(PS);
        Jump();
        StateUpdate();
        _state.Update(this);
    }

    void HandleInput()
    {
        PlayerState temp;
        temp = _state.HandleInput(PS);

        if (temp != null)
        {
            _state.Exit(this);
            _state = null;
            _state = temp;
            _state.Enter(this);
        }
    }

    void StateUpdate()
    {
        Climb();
        Move();
        HandleInput();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        
        if (x < 0)
        {
            sprite.flipX = true;
        }
        else if (x > 0)
        {
            sprite.flipX = false;
        }
        
        move = new Vector2(x * runSpeed, rigid.velocity.y);
        
        if (PS != null)
        {
            if (x != 0)
            {
                PS = PlayerStates.Run;
            }
            else
            {
                PS = PlayerStates.Idle;
            }
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && playerFeet.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Climb()
    {
        if (playerFeet.IsTouchingLayers(LayerMask.GetMask("Ladder")) && PS != null)
        {
            PS = PlayerStates.Climb;
        }
    }
    
}
