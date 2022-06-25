using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnArea : MonoBehaviour
{
    public UnityEvent Respawn;

    private void Start()
    {

        if (Respawn == null) {
            Respawn = new UnityEvent();
        }

       
    }
    private void OnTriggerEnter(Collider other)
    {

        Respawn.Invoke();
    }
}
