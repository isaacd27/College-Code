using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    public EnemyController Target; 
    public Transform TowerPivot;

    public float Damage;
    public float Firerate;
    public float Range;

    public LayerMask EnemiesMask;

    private float Delay;
    // Start is called before the first frame update
    void Start()
    {
        Delay = 1 / Firerate;
    }

    // Update is called once per frame
    public void tick()
    {
        if(Target != null)
        {
            TowerPivot.transform.rotation = Quaternion.LookRotation(Target.transform.position - transform.position);
        }
    }
}
