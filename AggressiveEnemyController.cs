using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveEnemyController : EnemyController
{

    public float speed = 5f;
    CharacterController controller;
    // public float rotationSpeed;

    // public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        float str = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
        Vector3 newpos = this.transform.position;

        controller.Move(transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime);
    }
}
