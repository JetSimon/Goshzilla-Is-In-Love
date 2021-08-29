using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool inUI = false;
    public static GameManager gameManager;
    public int attraction = 0;
    public int todoLevel = 0;

    bool paused = false;

    public ActiveWhenTag player;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(!inUI && Input.GetButtonDown("Cancel"))
        {
            Pause.pauseMenu.TogglePause();
            paused = !paused;
        }

        if(paused && Input.GetKeyDown("z"))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("Main Menu");
        }
    }

    public void PlaySound(string s)
    {
        PlaySound(s, 1f);
    }

    public void PlaySound(string s, float pitch)
    {
        //print("trying to play sound " + s);
        Transform g = GameObject.Find("SFX").transform.Find(s);
        if(g == null) return;
        AudioSource sfx = g.GetComponent<AudioSource>();
        if(sfx == null || sfx.isPlaying) return;
        sfx.pitch = pitch;
        sfx.Play();
    }

    public void StopSound(string s)
    {
        Transform g = GameObject.Find("SFX").transform.Find(s);
        if(g == null) return;
        AudioSource sfx = g.GetComponent<AudioSource>();
        if(sfx == null) return;
        sfx.Stop();
    }

    public void EndGame()
    {
        if(attraction <= -1)
            SceneManager.LoadScene("BadEnding");
        else if(attraction > -1 && attraction <= 1)
            SceneManager.LoadScene("NeutralEnding");
        else
            SceneManager.LoadScene("GoodEnding");
    }
}
