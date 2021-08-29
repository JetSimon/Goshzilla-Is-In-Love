using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static Pause pauseMenu;
    
    void Awake()
    {
        pauseMenu = this;
        gameObject.SetActive(false);
    }

    public void TogglePause()
    {
        GetComponent<Animator>().SetTrigger("Popup");
        gameObject.SetActive(!gameObject.activeSelf);
        if (Time.timeScale == 1.0f)
                Time.timeScale = 0f;
        else
            Time.timeScale = 1.0f;
    }
}
