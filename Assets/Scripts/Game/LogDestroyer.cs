using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogDestroyer : MonoBehaviour
{
    [SerializeField]
    private float explosionForce;
    [SerializeField]
    private float torqueForce;
    public void DestroyLog()
    {
        List<Rigidbody2D> rbs = new List<Rigidbody2D>();
        rbs.AddRange(gameObject.GetComponentsInChildren<Rigidbody2D>()); //Getting all parts of the log
        gameObject.transform.DetachChildren(); //Removing the transform dependency of the pieces to log

        foreach (var rb in rbs) //One by one adding force and torque to the pieces.
        {
            rb.gravityScale = 1.4f;
            rb.AddTorque(torqueForce);
            rb.AddForce(new Vector2(Random.Range(-explosionForce, explosionForce), Random.Range(-explosionForce, explosionForce)));
            Destroy(rb.gameObject, 5f);
        }
    }

    private void Start()
    {
        DestroyLog();
    }
}
