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
        rbs.AddRange(gameObject.GetComponentsInChildren<Rigidbody2D>());
        gameObject.transform.DetachChildren();
        foreach (var rb in rbs)
        {
            rb.gravityScale = 1.4f;
            rb.AddTorque(torqueForce);
            rb.AddForce(new Vector2(Random.Range(-explosionForce, explosionForce), Random.Range(-explosionForce, explosionForce)));
        }
    }

    private void Start()
    {
        DestroyLog();
    }
}
