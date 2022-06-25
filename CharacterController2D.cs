using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    //Speed in Units per second
    public float speed = 5f;



    public GameObject ShotPrefab;

    //Seconds between shots
    public float ShotCooldown = 0.5f;

    float currentShotCooldown = 0;


    // Start is called before the first frame update
    void Start()
    {
        currentShotCooldown = ShotCooldown;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Raw input, this sets our movement vectors betweeen -1 and 1
        // in both the horizontal and vertial directions
        Vector2 RawInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //If you want tight controls
       //Vector2 RawInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //We normalize the direction vector to force it to be unit length!
        //We check to see if the magintude is greater than one, so that we allow
        // for direction vector values of less then one.
        if(RawInput.magnitude>1)
            RawInput.Normalize();


        transform.Translate(RawInput * speed * Time.deltaTime);

        //Decrement our timer
        currentShotCooldown -= Time.deltaTime;

        //check if our timer is ready and if fire has been pressed
        if (Input.GetButton("Fire1") && currentShotCooldown < 0)
            Fire();

    }

    private void Fire()
    {
        //On fire reset our timer
        currentShotCooldown = ShotCooldown;
        //Fire Bullet(in this case, we just instatiate, but we could call the pool instead)
        GameObject.Instantiate(ShotPrefab, transform.position, transform.rotation);
    }
}
