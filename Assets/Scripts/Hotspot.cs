using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotspot : MonoBehaviour
{
    public int minimumLevel = 0, reward = 0;
    public Node node;
    public bool activated = false;

    public bool dead = false;

    // Update is called once per frame
    void Update()
    {
        if(!dead && Input.GetButtonDown("Fire1") && activated && !GameManager.gameManager.inUI && GameManager.gameManager.todoLevel >= minimumLevel)
        {
            
            GameManager.gameManager.todoLevel += reward;
            CrossoutTodo.crossoutTodo.UpdateTodo();
            DialogueManager.dialogueManager.SetNode(node);
            dead = true;
        }
    }
}
