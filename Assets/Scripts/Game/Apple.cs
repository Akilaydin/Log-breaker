using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{ 
    void Start()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("Log").transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Knife")
        {
            GameController.instance.OnAppleHit();
            Destroy(gameObject);
        }
    }
    
}
