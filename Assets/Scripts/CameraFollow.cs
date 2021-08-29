using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothTime;
    public GameObject target;
    private Vector3 velocity = Vector3.zero, offset;

    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position - offset, ref velocity, smoothTime);
    }
}
