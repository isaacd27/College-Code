using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class CharacterController3D : MonoBehaviour
{
    //Unity units per second
    public float Speed = 5f;

    public float JumpVelocity = 5f;
    public KeyCode lockcam;
    public Vector3 currentVelocity;

    public bool ShowOnGround;
    public Bullet bulletprefab;
    public Animator anim;

    //reference to the character Controller attached to our game object
    CharacterController controller;

    public Transform cameraTarget;

    public float cameraDistance = 5;
    public float defaultPitch = 30;
    private float cameraPitch;
    private float cameraYaw = 0;
    bool cameralock = false;

    public float cooldown;
    float cooltime;

    float hp;
    public float maxhp;

    float rolltime = 0f;
    public float maxroll;

    public float maxinvinc;
    float invinctime;

    public float ylimit;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if(controller == null)
        {
            this.gameObject.AddComponent<CharacterController>();
        }

        anim = GetComponent<Animator>();
        if(anim == null)
        {
            Debug.LogError("no animator on player!");
        }
        hp = maxhp;
        cameraTarget = transform;
        cameraPitch = defaultPitch;
       // setMaterial(pigMaterial);
    }

    public void setMaterial(Material material)
    {
        GetComponent<MeshRenderer>().material = material;
    }

    // Update is called once per frame
    void Update()
    {
        cooltime += Time.deltaTime;

        updateController();
        if (!cameralock)
        {
            updateCamera();
        }

        if(rolltime < 0f)
        {
            rolltime -= 1* Time.deltaTime;
        }


        if(invinctime < 0f)
        {
            invinctime -= 1 * Time.deltaTime;
        }


        if(transform.position.y < ylimit)
        {
            transform.position = Vector3.zero;
            Debug.Log("Fell through a hole");
        }
    }

    private void updateCamera()
    {
        if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
        {
            cameraPitch += Input.GetAxis("Mouse Y") * -2.0f;
            cameraPitch = Mathf.Clamp(cameraPitch, -10.0f, 80f);
            cameraYaw += Input.GetAxis("Mouse X") * 5.0f;
            cameraYaw = cameraYaw % 360.0f;
        }
        else
        {
            cameraYaw = Mathf.LerpAngle(cameraYaw, cameraTarget.eulerAngles.y, 5.0f * Time.deltaTime);
            cameraPitch = Mathf.LerpAngle(cameraPitch, cameraTarget.eulerAngles.x + defaultPitch, 5.0f * Time.deltaTime);
        }

        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            cameraDistance -= Input.GetAxis("Mouse ScrollWheel") * 5.0f;
            cameraDistance = Mathf.Clamp(cameraDistance, 2.0f, 12.0f);
        }
        //the following if statement will lock the camera to 90 degree angles
        /*
        if (cameraYaw % 90 != 0 && cameraYaw != 0f)
        {
            cameraYaw = MathF.Round(cameraYaw / 90) * 90;
        }
        //uncomment it to enable
        */
        Vector3 newCameraPosition = cameraTarget.position + (Quaternion.Euler(cameraPitch, cameraYaw, 0) * Vector3.back * cameraDistance);
        Camera.main.transform.position = newCameraPosition;
        Camera.main.transform.LookAt(cameraTarget.position);
    }

    private void updateController()
    {
        Vector2 RawInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (RawInput.magnitude > 1)
        {
            RawInput.Normalize();
            //walking animation here!
            //eg. anim.setbool("walk", true)
        }//idle animation in the else!

        ShowOnGround = controller.isGrounded;

       /* if (controller.isGrounded && Input.GetButtonDown("Jump"))
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
        }*/

        if (Input.GetKeyDown(lockcam))
        {
            cameralock = !cameralock;

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rolltime = maxroll;
        }

        if (Input.GetMouseButton(1)) //add && !cameralock if you want to change this
        {
            transform.rotation = Quaternion.Euler(0, cameraYaw, 0); // face the same way as the camera
        }


        if (Input.GetAxis("Fire1") != 0 && cooltime > cooldown && GameManager.instance.currentGameState == GameState.PlayMode)
        {

            Bullet temp = Instantiate(bulletprefab, new Vector3(this.transform.position.x + transform.forward.x, this.transform.position.y + transform.forward.y), this.transform.rotation);
            temp.transform.position = this.transform.position + this.transform.forward * 0.4f * Mathf.Sign(this.transform.localScale.x);
            cooltime = 0f;
            temp.setDirection(transform.forward);
            //attack animation here!
            //e.g anim.setfloat("Attack",time)
            //you can then have it transition into a reload with cooldown as the speed modifier
        }

        Vector3 RelativeInput = new Vector3(RawInput.x, 0, RawInput.y);
        Vector3 moveDirection = transform.TransformDirection(RelativeInput);


        controller.Move(moveDirection * Speed * Time.deltaTime + currentVelocity * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (rolltime >= 0f && invinctime >= 0f)
        {
            if (collision.gameObject.GetComponent<EnemyShot>() != null)
            {
                EnemyShot e = collision.gameObject.GetComponent<EnemyShot>();
                //if (!rollin){
                hp -= 1; //change 1 to enemyshot.damage
                Debug.Log("Hit by a shot");
                invinctime = maxinvinc;
                //}
            }
            else if (collision.gameObject.GetComponent<BasicEnemy>() != null)
            {
                Debug.Log("Hit by an enemy");
                invinctime = maxinvinc;
            }
            else if (collision.gameObject.CompareTag("Danger"))
            {
                Debug.Log("Hit by a hazard");
                invinctime = maxinvinc;
            }
        }
        else
        {
            Debug.Log("rollin");
        }
       



    }

}
