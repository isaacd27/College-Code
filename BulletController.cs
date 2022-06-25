using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float speed = 5f;
    float timeline;
    float maxTime = 2f;
    

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        timeline -= Time.deltaTime;

        if (timeline < 0)
            gameObject.SetActive(false);
    }

    public void ShootBullet(Vector3 position)
    {
        this.transform.SetPositionAndRotation(position, Quaternion.identity);
        gameObject.SetActive(true);
        timeline = maxTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("gets here!");
        gameObject.SetActive(false);
    }
    
}
