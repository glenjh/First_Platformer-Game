using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPlayer : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer sprite;
    CapsuleCollider2D capsuleCollider2D;
    BoxCollider2D playerFeet;

    public float moveSpeed = 5f;
    public float jumpForce = 20f;
    public float climbSpeed = 3f;
    public float normalGravity = 8f;
    public float knockBackForce = 20f;
    public bool isAlive = true;
    public bool isHit = false;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform gunPos;
    [SerializeField] AudioClip gunFireSFX;
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip dieSFX;

    void Init()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        playerFeet = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        Init();
    }
    
    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        move();
        Jump();
        Climb();
        Fire();
    }

    void move()
    {
        // Player move
        float x = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(x * moveSpeed, rigid.velocity.y);
        
        //Flip Sprites && Animation change
        bool isMoving = Mathf.Abs(rigid.velocity.x) > 0;
        if (isMoving)
        {
            anim.SetBool("isRuning", true);
            transform.localScale = new Vector2(Mathf.Sign(rigid.velocity.x), 1);
        }
        else
        {
            anim.SetBool("isRuning", false);
        }
    }

    void Jump()
    {
        // Jump && If Player is not touching the platform, do not jump again
        if (Input.GetButtonDown("Jump") && playerFeet.IsTouchingLayers(LayerMask.GetMask("Platform")))
        {
            AudioSource.PlayClipAtPoint(jumpSFX, Camera.main.transform.position);
            rigid.velocity += new Vector2(0, jumpForce);
        }
    }

    void Climb()
    {
        if (!capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rigid.gravityScale = normalGravity;
            anim.SetBool("isClimbing", false);
            return;
        }
        
        rigid.gravityScale = 0;
        float h = Input.GetAxisRaw("Vertical");
        bool isClimbing = Mathf.Abs(h) > 0;

        anim.SetBool("isClimbing", isClimbing);
        rigid.velocity = new Vector2(rigid.velocity.x, climbSpeed * h);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy" && !isHit)
        {
            isAlive = false;
            rigid.AddForce(new Vector2(Mathf.Sign(rigid.velocity.x)* -10, jumpForce), ForceMode2D.Impulse);
            isHit = true;
            anim.SetTrigger("onDamaged");
            AudioSource.PlayClipAtPoint(dieSFX, Camera.main.transform.position);
            FindObjectOfType<GameManager>().DecreaseLife();
        }
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioSource.PlayClipAtPoint(gunFireSFX, Camera.main.transform.position);
            Instantiate(bullet, gunPos.position, transform.rotation);
        }
    }
}
