using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private Vector3 BulletPos;
    [SerializeField] private EnemySoldier enemySoldier;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Mine"))
        {
            BulletPos = collision.transform.position.normalized;
            enemySoldier.Death(600f, BulletPos);
        }

        if (collision.transform.CompareTag("Vehicle"))
        {
            BulletPos = collision.transform.position.normalized;
            enemySoldier.Death(1200f, BulletPos);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Bullet"))
        {
            BulletPos = other.transform.position;
            enemySoldier.Death(250f, BulletPos);
        }

        if (other.CompareTag("Mine"))
        {
            BulletPos = other.transform.position;
            enemySoldier.Death(600f, BulletPos);
        }

        if (other.CompareTag("Rocket"))
        {
            BulletPos = other.transform.position;
            enemySoldier.Death(800f, BulletPos);
        }
    }
}
