using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moffy : MonoBehaviour
{
    float startY, rotX, rotZ;
    public float speed = 20f;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rotX = transform.eulerAngles.x;
        rotZ = transform.eulerAngles.z;
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameManager.todoLevel < 1) return;
        transform.LookAt(player.transform);
        if(Vector3.Distance(player.transform.position, transform.position) > 35f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        transform.eulerAngles = new Vector3(rotX, transform.eulerAngles.y, rotZ);
    }
}
