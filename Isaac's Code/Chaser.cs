using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : BasicEnemy
{
    // public int hp = 1;
    // Start is called before the first frame update
    public float explodedelay;
    public float exploderad;
    public bool exploding;
    public SphereCollider explosion;
    public float explodingtime;
    // public GameObject explosionparticles
    void Start()
    {
        Player = FindObjectOfType<CharacterController3D>().gameObject;
        Debug.Assert(Player);
        explosion.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (exploding)
        {
            explodedelay -= Time.deltaTime;
           
            if (explodedelay <= 0f)
            {
                explosion.enabled = true;
                //Instantiate(explosionparticles, this.transform);
                explodingtime -= Time.deltaTime;
                if(explodingtime <= 0f)
                {
                    GameObject.Destroy(this);
                    //GameObject.Destroy(explosionparticles);
                }
            }
            else
            {
                Movement();
            }
        }
        else
        {
            Movement();
        }
    }

   new protected  void Movement()
    {



        float boom = Vector3.Distance(transform.position, Player.transform.position);
       
        if (boom <= exploderad)
        {
            Vector2 targetDirection = transform.position - Player.transform.position;
            Vector2 movement = targetDirection * speed;
            Vector2 newPos = GetComponent<Rigidbody2D>().position + movement * Time.fixedDeltaTime;
            transform.position = newPos;

            /*  if (this.transform.position.x > Player.transform.position.x)
              {
          
                  transform.position += new Vector3(-speed * Time.deltaTime, 0f);
              }
              else if (this.transform.position.x < Player.transform.position.x)
              {
               
                  transform.position += new Vector3(speed * Time.deltaTime, 0f);
              }

              if (this.transform.position.z > Player.transform.position.z)
              {
                  transform.position += new Vector3(0f, -speed * Time.deltaTime);
              }
              else if (transform.position.z < Player.transform.position.z)
              {
                  transform.position += new Vector3(0f, speed * Time.deltaTime);
              }*/
        }
        else
        {
            exploding = true;
            
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Change this for check type instead + set collision 2d matrix layer 
        if (collision.gameObject.CompareTag("Projectile"))
        {
            hp -= 1;
            if (hp <= 0)
            {
                //Destroy(this.gameObject);
            }
        }else if (collision.gameObject.CompareTag("Danger"))
        {
            Debug.Log("Touched a trap");
            //exploding = true;
        }
        
    }
    
}





