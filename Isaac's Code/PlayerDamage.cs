using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    //this can be implemented into the character con
    //float rolltime
    //public float maxroll
    //bool rollin
    int hp;
    public int maxhp;
    public HPbar Health;

    public void Start()
    {
        hp = maxhp;
        Health.SetMaxHP(maxhp);
    }

    public void Update()
    {
        Health.SetHp(hp);

        if(hp >= 0)
        {
            Debug.Log("Dead");
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyShot>() != null)
        {
            EnemyShot e = collision.gameObject.GetComponent<EnemyShot>();
            //if (!rollin){
            hp -= 1; //change 1 to enemyshot.damage
            Debug.Log("Hit by a shot");
            //}
        }else if (collision.gameObject.GetComponent<BasicEnemy>() != null)
        {
            Debug.Log("Hit by an enemy");
        }
    else if (collision.gameObject.CompareTag("Danger"))
        {
            Debug.Log("Hit by a hazard");
        }




    }

}
