using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] AudioClip enemyDieSFX;
    
    NPlayer player;
    Rigidbody2D rigid;
    float xSpeed;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<NPlayer>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }
    
    void Update()
    {
        rigid.velocity = new Vector2(xSpeed, 0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            AudioSource.PlayClipAtPoint(enemyDieSFX, Camera.main.transform.position);
            FindObjectOfType<GameManager>().PlusScore();
            Destroy(col.gameObject);
        }
        Destroy(gameObject);
    }
}
