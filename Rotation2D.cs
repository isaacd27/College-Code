using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation2D : MonoBehaviour
{
    public Transform Target;

    public float RotationSpeed = 15;
    public float Speed = 5f;
    public float stopDistance = 0.2f;

    public int RotationStyle = 0;

    public bool moveTowards = false;
    public float angleOffset = 90;


    // Update is called once per frame
    void Update()
    {
        //Basic rotation, controls the change in rotation as degrees
        if (RotationStyle == 0)
        {

            //rotate the object based around the Z axis(for 2D) in Degrees per second.
            transform.Rotate(Vector3.back, Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime);
        }
        //create a direction vector and have the object point towards it, using the up vector
        else if (RotationStyle == 1)
        {

            //create a direction vector

            //The end point is the first element when subtracting
            Vector2 direction = Target.position - transform.position;

            //since our graphic is vertically aligned, we can set based off of the 'up' direction
            transform.up = direction;

            //This also works in 3D, as all axis operate effectively indepently.

            //if movetowards is set, we can have the object move towards our target
            if(moveTowards)
            {
                //you can use the
                Vector3 normalizedDirection = direction.normalized;
                
                //without normalizing, your ship will change speed based off of the distance
                //transform.position = transform.position + new Vector3(direction.x,direction.y)*Speed*Time.deltaTime;
                //with  normalizing our ship maintains a constant speed.
                transform.position = transform.position + normalizedDirection * Speed * Time.deltaTime;

            }

        }
        //this controls if you want the object to face the mouse;
        else if(RotationStyle == 2)
        {
            Vector3 MouseInput = Input.mousePosition;
            MouseInput = Camera.main.ScreenToWorldPoint(MouseInput);
            Vector2 direction = MouseInput - transform.position;
            //is equivelent to 
            /*Vector2 direction = new Vector2(
                MouseInput.x - transform.position.x,
                MouseInput.y - transform.position.y);*/

            //Since we know the oppososite(y) and adjacent(x) we can use arcTan to figure out our
            // angle value, we need to convert back into degrees as our result is in radians.
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angleOffset;

            transform.rotation= Quaternion.Euler(0, 0, angle);

            if (moveTowards)
            {
                //this will remove the jitter
                if(stopDistance <= direction.magnitude)
                    transform.Translate(Vector3.up * Time.deltaTime * Speed);
            }
        }


        
    }
}
