using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossoutTodo : MonoBehaviour
{
    Transform[] children;
    public static CrossoutTodo crossoutTodo;
    // Start is called before the first frame update
    void Start()
    {
        children = GetComponentsInChildren<Transform>();
        foreach(Transform child in children) 
        {
            if(child != transform)   
                child.gameObject.SetActive(false);
        }
        crossoutTodo = this;
    }

    public void UpdateTodo()
    {
        for(int i = 1; i < children.Length - 1; i++)
        {
            children[i].gameObject.SetActive(GameManager.gameManager.todoLevel >= i);
        }
    }
}
