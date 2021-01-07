using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogDestroyer : MonoBehaviour
{
    [SerializeField]
    private float explosionForce;
    public void DestroyLog() {
        List<Rigidbody2D> rbs = new List<Rigidbody2D>();
        Debug.Log(gameObject.GetComponentsInChildren<Rigidbody2D>().Length);
        rbs.AddRange(gameObject.GetComponentsInChildren<Rigidbody2D>());
        gameObject.transform.DetachChildren();
        foreach(var rb in rbs){
            rb.AddForce(new Vector2(Random.Range(-explosionForce,explosionForce),Random.Range(-explosionForce,explosionForce)));
        }
        
    }
}
