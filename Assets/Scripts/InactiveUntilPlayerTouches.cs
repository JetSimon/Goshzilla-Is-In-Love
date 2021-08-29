using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveUntilPlayerTouches : MonoBehaviour
{
    Rigidbody rb;

    public string sfx = "";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player")
            Activate();
    }

    public void Activate()
    {
        if(rb == null || rb.isKinematic == false) return;

        if(sfx != "") GameManager.gameManager.PlaySound(sfx);
        rb.isKinematic = false;
        
        gameObject.tag = "Player";
    }
}
