using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    //public GameObject coinprefab;
  public GameObject Player;
    public float speed = 0.3f;

    public int hp = 1;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<CharacterController3D>().gameObject;
        Debug.Assert(Player);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    protected virtual void Movement()
    {
        if (this.transform.position.x > Player.transform.position.x)
        {
            //transform.localScale = new Vector3((float)-1.943782, transform.localScale.y);
            transform.position += new Vector3(-speed * Time.deltaTime, 0f);
        }
        else if (this.transform.position.x < Player.transform.position.x)
        {
            //transform.localScale = new Vector3((float)1.943782, transform.localScale.y);
            transform.position += new Vector3(speed * Time.deltaTime, 0f);
        }
        else if (this.transform.position.x == Player.transform.position.x)
        {
            if (this.transform.position.y > Player.transform.position.y)
            {
                transform.position += new Vector3(0f, -speed * Time.deltaTime);
            }
            else if (transform.position.y < Player.transform.position.y)
            {
                transform.position += new Vector3(0f, speed * Time.deltaTime);
            }
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            hp -= 1;
            
            if (hp <= 0)
            {
                //temp = GameObject.Instantiate(coinprefab, new Vector3(this.transform.position.x + d.x, this.transform.position.y + d.y), this.transform.rotation);
                //Scoremanager.setScore(Score)
                Destroy(this.gameObject);
            }
          
        }
    }
}
