using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int Maxhp;
    int hp;

    int State;

    EnemyController target = null;

    public float maxtime;
    float time;

    public float bulletinterval;

    const int SEARCH = 0;
    const int DESTROY = 1;

    public Bullet bulletprefab;
    TowerSensor t;

    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        hp = Maxhp;
        t = this.GetComponent<TowerSensor>();
        if (t == null)
        {
            Debug.LogError("no Testcone!");
            this.gameObject.AddComponent<TowerSensor>();
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case SEARCH:
                check();
                break;
            case DESTROY:
                Destroy();
                break;

        }
    }

    private void Destroy()
    {
        if (t.getTar() != null)
        {

            target = t.getTar().gameObject.GetComponent<EnemyController>();
        }
        else
        {
            time = 0f;
            State = SEARCH;
            return;
        }
        Debug.Log("Destroy");
        GameObject Target = target.gameObject;
        Quaternion targetRotation = Quaternion.LookRotation(Target.transform.position - transform.position);
        float str = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
        //rotation animation here
        //transform.LookAt(Target.transform);
        time += Time.deltaTime;

        if (time >= bulletinterval)
        {

            time = 0f;

            Bullet temp = Instantiate(bulletprefab, new Vector3(this.transform.position.x + transform.forward.x, this.transform.position.y + transform.forward.y), this.transform.rotation);
            temp.transform.position = this.transform.position + this.transform.forward * 0.4f * Mathf.Sign(this.transform.localScale.x);

            temp.setDirection(transform.forward);


        }


        // transform.Translate(0, 0, movespeed * Time.deltaTime);

    }

    void check()
    {
        if (t.getTar() != null)
        {
            target = t.getTar().gameObject.GetComponent<EnemyController>();
        }
        else
        {
            time = 0f;
            State = SEARCH;
        }
        //Debug.Log("Search");
        if (target != null)
        {
            time += Time.deltaTime;
            if (time >= maxtime)
            {

                State = DESTROY;
                time = 0f;
            }
        }
    }
}
