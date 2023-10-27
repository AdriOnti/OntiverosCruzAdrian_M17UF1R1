using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform shooterEnemy;
    void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        transform.position = shooterEnemy.position;
    }
}
