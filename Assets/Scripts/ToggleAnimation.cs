using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAnimation : MonoBehaviour
{
    public string variableName, inputName;
    private Animator ani;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameManager.inUI && Input.GetButtonDown(inputName))
        {   
            GameManager.gameManager.PlaySound("whoosh");
            ani.SetBool(variableName, !ani.GetBool(variableName));        
        }
    }
}
