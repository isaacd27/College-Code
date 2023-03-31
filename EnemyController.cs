using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public FieldOfView Targ;

    GameObject target = null;

    public float movespeed;
    public float rotationSpeed;

    public float maxtime;
    float time;

    int state;
    const int STATE_WALK = 0;
    const int STATE_ATTACK = 1;

    public GameObject[] patrolPoints;
    public NavMeshAgent agent;


    public GameObject playerRef;

    public int Maxhp;
    int hp;
    bool first = false;
    Vector3 targetr = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {

        agent.speed = movespeed;
        agent.angularSpeed = rotationSpeed;

        hp = Maxhp;
        Targ = this.GetComponent<FieldOfView>();
        if (Targ == null)
        {
            Debug.LogError("no Testcone!");
            return;
        }
        playerRef = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {



        switch (state)
        {

            case STATE_WALK:
                check();
                break;
            case STATE_ATTACK:
                movement();
                //walk animation here!, eg anim.setbool("walk", true); recommend adding it in movement)
                // Debug.Log(state);
                break;
            default:
                check();
                // Debug.Log(state);
                break;
        }



        //Debug.Log(state);


        if (hp <= 0)
        {
            Destroy(gameObject);
        }


    }

    public void damage(int Damage)
    {
        hp -= Damage;
    }

    void check()
    {
        if (Targ.getTar() != false)
        {
            // Target = Targ.getTar();
            time += Time.deltaTime;
            if (time >= maxtime)
            {
                //like a filling awareness meter
                //that'd be cool
                //not gonna implement tho
                state = STATE_ATTACK;
                //play sfx here
            }
        }
        else
        {
            time = 0f;
            if (patrolPoints.Length != 0)
            {

                //TODO: fix so it doesn't get stuck, and so that it goes to each point in order.



                for (int i = 0; i < patrolPoints.Length; i++)
                {
                    int check = i + 1;

                    if (check < patrolPoints.Length)
                    {
                        if (transform.position.x == patrolPoints[i].transform.position.x && transform.position.z == patrolPoints[i].transform.position.z)
                        {
                            Debug.Log("test");
                            targetr = patrolPoints[i + 1].transform.position;
                        }
                        else if (first == false)
                        {

                            targetr = patrolPoints[0].transform.position;
                            first = true;
                            //Debug.Log(first);
                        }



                    }
                    else
                    {
                        if (transform.position.x == patrolPoints[i].transform.position.x && transform.position.z == patrolPoints[i].transform.position.z)
                        {
                            targetr = patrolPoints[0].transform.position;
                        }

                    }
                }

                agent.SetDestination(targetr);

                Quaternion targetRotation = Quaternion.LookRotation(targetr - transform.position);
                float str = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
                //Debug.Log(targetr);



            }
            state = STATE_WALK;
        }




    }

    void movement()
    {

        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        float str = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);

        transform.LookAt(target.transform);

        transform.Translate(0, 0, movespeed * Time.deltaTime);



        if (Targ.getTar() == false)
        {


            //possible to make it search for you, for a bit? via the last location it saw you 
            //would be nice 
            //probably not gonna happen
            state = STATE_WALK;
            time = 0f;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Sycthe>() != null)
        {
            damage(1);
        }
    }


}