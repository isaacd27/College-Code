using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public BulletManager bulletManager;
    private float speed = 2f; 

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime * Input.GetAxis("Horizontal"));
        transform.Translate(Vector3.up * speed * Time.deltaTime * Input.GetAxis("Vertical"));

        if(Input.GetButtonDown("Fire1"))
        {
            bulletManager.shootBullet(transform.position);
        }

    }
}
