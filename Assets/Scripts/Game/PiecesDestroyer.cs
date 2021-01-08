using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) //To remove the pieces that remain after log's destroy.
    { 
        Destroy(other.gameObject);
    }
}
