using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation3D : MonoBehaviour
{

    public Transform Target;

    public Vector3 EulerAngles;

    public int RotationStyle = 0;

    // Update is called once per frame
    void Update()
    {
        //Euler Angles
        if (RotationStyle == 0)
        {

            //By default Quaternions are

            //Quaternion.identity;
            //Euler Rotation, by default, is in Z, then X, then Y 
            transform.rotation = Quaternion.Euler(EulerAngles);
        }
        else if (RotationStyle == 1)
        {
            transform.LookAt(Target);
        }
        //this is if you do each Euler angle individually.
        else if (RotationStyle == 2)
        {
            transform.rotation = Quaternion.AngleAxis(EulerAngles.y, Vector3.up);
            transform.rotation *= Quaternion.AngleAxis(EulerAngles.x, Vector3.right);
            transform.rotation *= Quaternion.AngleAxis(EulerAngles.z,Vector3.forward);
            
            

        }

    }
}
