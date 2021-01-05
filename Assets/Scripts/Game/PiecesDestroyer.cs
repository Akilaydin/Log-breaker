using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesDestroyer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.name == "Log piece"){
            Destroy(other.gameObject);
        }
    }
}
