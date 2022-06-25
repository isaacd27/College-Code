using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolEnemyController : EnemyController
{
    public float directionPeriodDuration = 100f;
    private float directionPeriod = 0;
    private Vector3 directionVector = new Vector3(0, 0, 0);

    protected override void doMotion()
    {
        directionPeriod -= Time.deltaTime;

        if (directionPeriod < 0)
        {
            directionPeriod = directionPeriodDuration;
            directionVector.x = Random.Range(-speed, speed);
            directionVector.y = -0.1f * speed;
            directionVector.z = Random.Range(-speed, speed);
        }

        transform.Translate(directionVector);
    }

    protected override void reactToCollision(Collision2D collision)
    {
        //check if what we are colliding with has a bulletscript as a component
        //if it does then it must be a bullet and we will respawn this enemy up 12 units
        if (collision.collider.gameObject.GetComponent<BulletController>() != null)
        {
            speed = 0;
        }
    }

}
