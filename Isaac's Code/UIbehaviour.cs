using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIbehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator die1;
    public Animator die2;

    public void loadLevel(string levelname = "default")
    {
        SceneManager.LoadScene(levelname);
    }

  

    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame

}
