using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class CharacterController3D : MonoBehaviour
{
    //Unity units per second
    public float Speed = 5f;

    public float JumpVelocity = 5f;



    public Vector3 currentVelocity;

    public bool ShowOnGround;

    //reference to the character Controller attached to our game object
    CharacterController controller;
    Camera maincam;

    GameObject camtarget;

    public float camdist = 5f;

    public float defpitch = 30f;

     float campitch = 30f;

   float camyaw = 0f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        maincam = Camera.main;
        camtarget = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        updateCamera();
    }

    private void updateCamera()
    {

        if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
        {
            campitch += Input.GetAxis("Mouse Y") * 2f;
            campitch = Mathf.Clamp(campitch, -10f, 80f);

            camyaw += Input.GetAxis("Mouse X") * 5f;
            camyaw = camyaw % 360f;

        }
        else
        {
            camyaw = Mathf.LerpAngle(camyaw, transform.eulerAngles.y, 5f * Time.deltaTime);
            campitch = Mathf.LerpAngle(campitch, defpitch, 2f * Time.deltaTime);
        }
        Vector3 newcampos = camtarget.transform.position + (Quaternion.Euler(campitch, camyaw, 0) * Vector3.back * camdist);
        maincam.transform.position = newcampos;
        maincam.transform.LookAt(camtarget.transform);

        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            camdist -= Input.GetAxis("Mouse ScrollWheel") * 5f;
            camdist = Mathf.Clamp(camdist, 2f, 15f);
        }


    }
    private void movement()
    {
        Vector2 RawInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (RawInput.magnitude > 1)
        {
            RawInput.Normalize();
        }

        ShowOnGround = controller.isGrounded;

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            currentVelocity.y += JumpVelocity;
        }
        else if (controller.isGrounded)
        {
            currentVelocity.y = Physics.gravity.y * Time.deltaTime;
        }
        else if (!controller.isGrounded)
        {
            currentVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        if (Input.GetMouseButton(0))
        {
            transform.rotation = Quaternion.Euler(0, camyaw, 0);
           
        }

        Vector3 RelativeInput = new Vector3(RawInput.x, 0, RawInput.y);
        Vector3 direction = transform.TransformDirection(RelativeInput);


        controller.Move(direction * Speed * Time.deltaTime + currentVelocity * Time.deltaTime);

       
    }
}
