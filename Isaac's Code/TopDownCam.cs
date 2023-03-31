using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCam : MonoBehaviour
{

    public Camera topdownCam;
    public float cameraSpeed;
    Vector3 intialposition;
    // Start is called before the first frame update
    void Start()
    {
        if (topdownCam == null)
        {
            Debug.LogWarning("Camera not found, attempting to get component");
            Camera tempcam = GetComponent<Camera>();

            if (tempcam == null)
            {
                Debug.LogError("TOPDOWNCAM: camera could not be found");
                return;
                //Destroy(this);
            }
            else
            {
                topdownCam = tempcam;
            }

        }

        intialposition = topdownCam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.currentGameState == GameState.BuildingMode)
        {
            Vector2 RawInput = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 RelativeInput = new Vector3(RawInput.x, 0, RawInput.y);
            Vector3 moveDirection = transform.TransformDirection(RelativeInput);

            if (Input.GetMouseButton(1)) 
            {
                topdownCam.transform.position = topdownCam.transform.position + (moveDirection * cameraSpeed * Time.deltaTime);
            }
                
        }else if (GameManager.instance.currentGameState != GameState.BuildingMode && topdownCam.transform.position != intialposition)
        {
            topdownCam.transform.position = intialposition;
        }
    }
}
