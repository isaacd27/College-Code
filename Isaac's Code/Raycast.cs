using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Raycast : MonoBehaviour
{
    public Transform lookat;
    public LayerMask mask;

    int state;

    const int WALK = 0;
    const int ATTACK = 1;



    public float rotationSpeed;

    public float movespeed = 3;

    public Transform target;


    public float detectdist;

    float watchtimer;

    public List<Transform> waypoints;
    List<Transform> pointlist = new List<Transform>();

    private const int count = 10;

    int currpoint;

    int waypoint;
    // Start is called before the first frame update
    void Start()
    {
        var gameObjects = new GameObject[count];
        var Objects = new Object[count];
        for (var i = 0; i < count; ++i)
        {
            gameObjects[i] = new GameObject();
           // Objects[i] = FindObjectsOfType(Object);
        }
    }
    void nav()
    {
        RaycastHit hitinfo = new RaycastHit();
        if (Physics.Raycast(transform.position, target.position - transform.position, out hitinfo))
        {
            if (hitinfo.transform.tag == "Player")
            {
              //  state = STATE_CHASING;
                return;
            }
        }

        if (waypoint == -1)
        {
            float distance = -1;
            foreach (Transform t in pointlist)
            {
                float tempdist = Vector3.Distance(transform.position, t.position);
                if (distance == -1 || tempdist < distance)
                {
                    distance = tempdist;
                    waypoint = pointlist.IndexOf(t);
                }
            }




        }

        Quaternion targetRotation = Quaternion.LookRotation(pointlist[waypoint].position - transform.position);
        float str = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
        transform.Translate(0, 0, movespeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pointlist[waypoint].position) <= 1)
        {
            int leftnavpoint = waypoint - 1;

            if (leftnavpoint < 0)
            {
                leftnavpoint = pointlist.Count - 1;
            }

            int rightnavpoint = waypoint + 1;

            if (rightnavpoint >= pointlist.Count)
            {
                rightnavpoint = 0;
            }

            if (Vector3.Distance(target.transform.position, pointlist[leftnavpoint].position) < Vector3.Distance(target.transform.position, pointlist[leftnavpoint].position))
            {
                waypoint = leftnavpoint;
            }
            else
            {
                waypoint = rightnavpoint;
            }
        }


    }


    private void Navigate1()
    {
        Ray cansee = new Ray(transform.position, transform.forward);
        RaycastHit obs = new RaycastHit();

        Physics.Raycast(cansee, out obs, detectdist * 1.5f);

        Object o = obs.transform.gameObject.GetComponent<Object>();

        if (o == null)
        {
            Debug.LogWarning("Wtf?");
            //state = STATE_PATROL;
        }
        else
        {
            Debug.Log("ok");
        }

        //Transform target2;

        List<Transform> pointlist = new List<Transform>();
        //  Debug.Log(o.transform.GetChildCount());
      //  Debug.Log(o.getlistcoutn());
       /* for (int i = 0; i >= o.getlistcoutn() - 1; i++)
        {
            Debug.Log(i);
            pointlist[i] = o.getlist(i);
        }
        */
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        Transform pmin = null;

        foreach (Transform t in pointlist)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }

        if (tMin != null && Vector3.Distance(this.transform.position, tMin.position) <= detectdist * 0.1)
        {
            Quaternion targetRotation = Quaternion.LookRotation(tMin.position - transform.position);
            float str = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);

            Gizmos.DrawLine(transform.position, tMin.position);

            transform.Translate(0, 0, movespeed * Time.deltaTime);
        }
        else if (tMin != null && Vector3.Distance(this.transform.position, tMin.position) > detectdist * 0.1)
        {
            foreach (Transform t in pointlist)
            {
                float dist = Vector3.Distance(t.position, target.position);
                if (dist < minDist)
                {
                    pmin = t;
                    minDist = dist;
                }
            }
        }
        else if (pmin != null)
        {
            Transform check = pmin;
            Quaternion targetRotation = Quaternion.LookRotation(pmin.position - transform.position);
            float str = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);

            Gizmos.DrawLine(transform.position, pmin.position);

            transform.Translate(0, 0, movespeed * Time.deltaTime);

            foreach (Transform t in pointlist)
            {
                float dist = Vector3.Distance(t.position, target.position);
                if (dist < minDist)
                {
                    pmin = t;
                    minDist = dist;
                }
            }

            if (pmin == check)
            {
                float thing = Vector3.Distance(transform.position, target.position);


                if (thing <= detectdist)
                {
                    //state = STATE_CHASING;
                }
                else
                {
                   // state = STATE_PATROL;
                }
            }

        }



        if (Vector3.Distance(transform.position, target.position) > detectdist + detectdist * 0.5f)
        {
            //state = STATE_PATROL;
            // startwatch();
        }
    }
    private void PATROL()
    {
        if (currpoint >= 0 && waypoints != null && currpoint < waypoints.Count)
        {
            Quaternion targetRotation = Quaternion.LookRotation(waypoints[currpoint].position - transform.position);
            float str = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
            transform.Translate(0, 0, movespeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, waypoints[currpoint].position) <= 2)
            {
                currpoint++;
                if (currpoint >= waypoints.Count)
                {
                    currpoint = 0;
                }


            }
        }



    }
    // Update is called once per frame
    void Update()
    {


        //foreach Object o in SceneManager.GetActiveScene;

        Ray cansee = new Ray(transform.position, transform.forward);
        RaycastHit hitinfo = new RaycastHit();
        if (Physics.Raycast(cansee, out hitinfo, 100f, mask.value))
        {
           evaluate(hitinfo);
                           // transform.LookAt(lookat);
            

        }
        else
        {
            state = WALK;
        }

    }
    void evaluate(RaycastHit obs)
    {
        state = ATTACK;
        Object o = obs.transform.gameObject.GetComponent<Object>();

        Ray sight2 = new Ray(transform.position, transform.right);
        Vector3 left = new Vector3(-transform.right.x, transform.right.y, transform.right.z);
        Ray sight3 = new Ray(transform.position, left);

        RaycastHit see2 = new RaycastHit();
        RaycastHit see3 = new RaycastHit();

        if (Physics.Raycast(sight2, out see2, 100f, mask.value))
        {
            Object o2 = see2.transform.gameObject.GetComponent<Object>();
            if (Physics.Raycast(sight3, out see3, 100f, mask.value))
            {
                Object o3 = see2.transform.gameObject.GetComponent<Object>();

                int i1;
                int i2;
                int i3;

                i1 = o.GetPriority();
                i2 = o2.GetPriority();
                i3 = o3.GetPriority();
                if (i1 > i2 && i1 > i3)
                {
                    transform.LookAt(o.gameObject.transform);
                }
                else if (i2 > i1 && i2 > i3)
                {
                    transform.LookAt(o2.gameObject.transform);
                }
                else if (i3 > i1 && i3 > i2)
                {
                    transform.LookAt(o3.gameObject.transform);
                }
                else
                {
                    transform.LookAt(o.gameObject.transform);
                }
            }
            else
            {
                int i1;
                int i2;


                i1 = o.GetPriority();
                i2 = o2.GetPriority();
                if (i1 > i2)
                {
                    transform.LookAt(o.gameObject.transform);
                }
                else if (i2 > i1)
                {
                    transform.LookAt(o2.gameObject.transform);
                }

            }

        }

    }


}
