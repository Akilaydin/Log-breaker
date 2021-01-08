using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeInLog : MonoBehaviour
{
    void Start()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("Log").transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Knife")
        {
            other.GetComponent<Rigidbody2D>().gravityScale = 1;
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x - Random.Range(1,4), -Random.Range(0,4));
            // other.transform.Rotate(new Vector3(0,0,180));
            //other.GetComponent<Rigidbody2D>().AddForce(new Vector3(0,15,0),ForceMode2D.Impulse);
            GameController.instance.GameOver(false);
        }
    }
}
