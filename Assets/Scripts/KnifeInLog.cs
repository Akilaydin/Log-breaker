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
            Debug.Log("Knife hit spawned knife");
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x - Random.Range(1,4), -Random.Range(0,4));
            GameController.instance.GameOver(false);
        }
    }
}
