using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    public Animator anim;
    bool attack;
    public int Damage;
    public float animtime;
    [Tooltip("Time for animation")]
    float timeforanim = 0f;

    public float Timeforanim { get => timeforanim; set => timeforanim = value; }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if(anim == null)
        {
            Debug.LogWarning("no animator: melee");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //anim.setbool("attack",attack) //change attack to the correct var, delete the first set of slashes;

        if (Input.GetAxis("Fire2") != 0 && GameManager.instance.currentGameState == GameState.PlayMode) //set to whatever
        {
            Timeforanim = animtime;
            if(Timeforanim > 0f) //just trying to make sure that the attack can connect throughout the whole animation
            {
                attack = true;
                Timeforanim -= Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.Escape)) //debug feature
                {
                    Timeforanim = -1f;
                    return;                
                }
            }
           

        }
        else
        {
            attack = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(attack && other.GetComponent<EnemyController>() != null)
        {
            EnemyController e = other.GetComponent<EnemyController>();

            e.damage(Damage);

        }
    }
}
