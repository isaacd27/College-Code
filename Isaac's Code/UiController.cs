using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    //public Text BombText;

    //public GameObject[] Lives;
    public Slider slider;


    public void SetHealth(int Health)
    {
        slider.value = Health;
    }
    //int currentLivesIndex = 2; 

    // Update is called once per frame
    void Update()
    {
       /* if(GameStateManager.Instance != null)
        {
            //BombText.text = GameStateManager.Instance.getBombs().ToString("D2");
            int currlives = GameStateManager.Instance.getlives();
           // Debug.Log(currlives);
            if(currlives/2 -1 != currentLivesIndex)
            {
                Lives[currentLivesIndex].SetActive(false);
                if (currentLivesIndex > 0)
                    currentLivesIndex -= 1;


            }
        }*/



    }
}
