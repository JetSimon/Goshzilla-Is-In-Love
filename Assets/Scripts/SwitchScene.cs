using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void SceneSwitch(string s)
    {
        SceneManager.LoadScene(s);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
