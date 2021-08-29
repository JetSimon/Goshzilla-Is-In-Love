using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWhenTag : MonoBehaviour
{
    public string objTag;
    public GameObject obj;

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == objTag && !other.gameObject.GetComponent<Hotspot>().dead && GameManager.gameManager.todoLevel >= other.gameObject.GetComponent<Hotspot>().minimumLevel)
        {
            obj.SetActive(true);
            other.gameObject.GetComponent<Hotspot>().activated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == objTag)
        {
            obj.SetActive(false);
            other.gameObject.GetComponent<Hotspot>().activated = false;
        }
    }
}
