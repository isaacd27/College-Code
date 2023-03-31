using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseNav : MonoBehaviour
{
    
    // public Gameobject Navpoint
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
        if (Input.GetMouseButton(1)) 
        {
            Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(movePosition, out var hitinfo))
            {
                agent.SetDestination(hitinfo.point); 
            }
        }
        //agent.SetDestination(Navpoint.position)
    }
}
