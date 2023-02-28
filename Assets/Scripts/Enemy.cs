using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    BoxCollider2D box;
    
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        rigid.velocity = new Vector2(moveSpeed, 0);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        moveSpeed = -moveSpeed;
    }
}
