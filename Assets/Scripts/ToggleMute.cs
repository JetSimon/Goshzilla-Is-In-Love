using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMute : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("m"))
            AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
}
