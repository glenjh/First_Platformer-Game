using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoin : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    bool gotCoin = false;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !gotCoin)
        {
            gotCoin = true;
            FindObjectOfType<GameManager>().PlusScore();
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
