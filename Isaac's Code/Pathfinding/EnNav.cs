using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnNav : MonoBehaviour
{
    public GameObject[] Navpoint;
    public NavMeshAgent agent;

    private void Start()
    {
        if (agent == null)
        {
            agent = this.gameObject.GetComponent<NavMeshAgent>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        foreach (GameObject g in Navpoint)
        {

            agent.SetDestination(g.transform.position);
        }
        //agent.SetDestination(Navpoint.position)
    }

}
