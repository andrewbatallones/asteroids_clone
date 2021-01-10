using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{    
    [SerializeField]
    public float speed = 1f;
    [SerializeField]
    public float damage = 1f;
    [SerializeField]
    public float lifespan = 1f;


    private Rigidbody2D rb2;
    private ParticleSystem explosion;
    private AudioSource explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        explosion = GetComponent<ParticleSystem>();
        explosionSound = GetComponent<AudioSource>();

        rb2.velocity = transform.up * -speed;

        Invoke("Explode", lifespan);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        DataManager data = other.gameObject.GetComponent<DataManager>();

        if (data != null)
            data.health -= damage;

        if (other.gameObject.tag != "Missile")
            Explode();
    }

    private void Explode()
    {
        Destroy(rb2);
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<SpriteRenderer>());
        explosionSound.Play();
        explosion.Play();
        Destroy(this.gameObject, explosion.main.duration);
    }
}
