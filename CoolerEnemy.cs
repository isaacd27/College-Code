using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolerEnemy :EnemyController
{
    public float directionPeriodDuration = 100f;
    private float directionPeriod = 0;
    private Vector3 directionVector = new Vector3(0, 0, 0);
    // Start is called before the first frame update

    protected override void doMotion()
    {
        directionPeriod -= Time.deltaTime;

        if (directionPeriod < 0)
        {
          
            directionVector.x = Random.Range(-100, 100);
            directionVector.y = -0.1f * speed;
           
        }

        transform.Translate(directionVector);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
