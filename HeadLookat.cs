using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLookat : MonoBehaviour
{
    public Transform Target;

    public Vector3 HeadRotationOffset;
   
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Target);
        transform.rotation *= Quaternion.Euler(HeadRotationOffset);
    }
}
