using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    public Vector3 rotationSpeed;

    
    public float velocity;

    // Update is called once per frame
    void Update()
    {
        
        float rollDelta = Time.deltaTime * rotationSpeed.z * Input.GetAxis("Horizontal")*-1;
        float pitchDelta = Time.deltaTime * rotationSpeed.x * Input.GetAxis("Vertical");
        transform.rotation  *= Quaternion.Euler(pitchDelta,0, rollDelta);

        transform.Translate(Vector3.forward * velocity * Time.deltaTime);
    }
}
