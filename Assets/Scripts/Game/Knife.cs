using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField]
    private Vector2 throwForce;
    private bool isActive = true;
    private Rigidbody2D knifeRB;
    private BoxCollider2D knifeCollider;
   

    private void Awake()
    {
        knifeRB = GetComponent<Rigidbody2D>();
        knifeCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isActive == true && GameController.instance.canThrow == true)
        {
            knifeRB.AddForce(throwForce, ForceMode2D.Impulse);
            knifeRB.gravityScale = 1;
            GameController.instance.gameUI.DecrementKnives();
            StartCoroutine(GameController.instance.MakeDelayForThrow());
        }
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isActive == false)
        {
            return;
        }

        isActive = false;

        if (other.collider.tag == "Log")
        {
            GetComponent<ParticleSystem>().Play();

            knifeRB.velocity = new Vector2(0, 0);
            knifeRB.bodyType = RigidbodyType2D.Kinematic;
            this.transform.SetParent(other.collider.transform);

            knifeCollider.offset = new Vector2(knifeCollider.offset.x, -0.45f);
            knifeCollider.size = new Vector2(knifeCollider.size.x, 1.1f);

            GameController.instance.OnAccurateKnifeHit();
        }
        else if (other.collider.tag == "Knife")
        {
            knifeRB.velocity = new Vector2(knifeRB.velocity.x, -2);
            GameController.instance.GameOver(false);
        }
        Handheld.Vibrate();
    }
}
