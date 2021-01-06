using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeHitForMenu : MonoBehaviour
{
    private Rigidbody2D knifeRB;
    private BoxCollider2D knifeCollider;


    private void Awake()
    {
        knifeRB = GetComponent<Rigidbody2D>();
        knifeCollider = GetComponent<BoxCollider2D>();
    }
    public void ShootKnife()
    {
        knifeRB.AddForce(new Vector2(0, 40), ForceMode2D.Impulse);
        knifeRB.gravityScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

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
        Handheld.Vibrate();
    }
}
