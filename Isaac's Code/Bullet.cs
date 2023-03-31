using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody r;
    public float speed = 5f; 
    public int damage;
    // Start is called before the first frame update
    private void Awake()
    {
        r = GetComponent<Rigidbody>();

        
        
        // GetComponent<GunFace>().onShoot += Projectile.onShoot;
    }
    void Start()
    {
        
    }

    public void setDirection(Vector3 dir)
    {
        transform.up = dir;
        r.velocity = new Vector3(dir.x * speed, dir.y * speed,dir.z*speed);
        // speed = speed * dir;
    }

    // Update is called once per frame
    void Update()
    {
         // transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyController>() != null)
        {
            EnemyController e = collision.gameObject.GetComponent<EnemyController>();
            e.damage(damage);

        }
        Destroy(this.gameObject);
    }
}

