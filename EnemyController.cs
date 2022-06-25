using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float frequency;
    public float speed;
    private float lifeline;

    void Update()
    {
        doMotion();
    }

    protected virtual void doMotion()
    {
        lifeline += Time.deltaTime;
        transform.Translate(Vector3.down * Time.deltaTime * speed + Vector3.right * Mathf.Sin(frequency * lifeline) * speed * 0.01f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        reactToCollision(collision);
    }

    protected virtual void reactToCollision(Collision2D collision)
    {
        //check if what we are colliding with has a bulletscript as a component
        //if it does then it must be a bullet and we will respawn this enemy up 12 units
        if (collision.collider.gameObject.GetComponent<BulletController>() != null)
        {
            transform.Translate(Vector3.up * 12);
            transform.Translate(Vector3.left * transform.position.x);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Killzone")
        {

            transform.Translate(Vector3.up * 12);
        }
    }
}
