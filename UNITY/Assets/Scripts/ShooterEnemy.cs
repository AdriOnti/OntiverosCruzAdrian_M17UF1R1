using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    public GameObject projectile;
    public Transform shootTarget;

    public void FixedUpdate()
    {
        Vector3 direccion = shootTarget.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direccion);

        projectile.SetActive(true);
        projectile.GetComponent<Rigidbody2D>().velocity = direccion.normalized * 10f;
    }
}