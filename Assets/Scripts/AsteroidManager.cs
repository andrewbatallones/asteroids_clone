using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField]
    public float rotationSpeed = 1.0f;
    [SerializeField]
    public float speed = 1.0f;
    [SerializeField]
    public int scoreValue = 0;

    public Transform leftSpawn;
    public Transform rightSpawn;
    public Transform smallAsteroid;


    private Rigidbody2D rb2;
    private DataManager data;
    private Camera cam;
    private float rotationCoef;


    public void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        data = GetComponent<DataManager>();
        cam = Camera.main;
        rotationCoef = Random.Range(-1.0f, 1.0f);

        InitialVelocity();
    }

    private void Update()
    {
        if (data.health <= 0)
        {
            UpdateScore();

            if (leftSpawn != null && rightSpawn != null)
            {
                Instantiate(smallAsteroid, rightSpawn.position, rightSpawn.rotation);
                Instantiate(smallAsteroid, leftSpawn.position, rightSpawn.rotation);
            }

            data.RemoveSelf();
        }
    }

    private void FixedUpdate()
    {
        Spin();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DataManager otherManager = other.gameObject.GetComponent<DataManager>();

        if (otherManager != null && other.gameObject.tag != this.tag)
            otherManager.health -= data.damage;
    }


    private void UpdateScore()
    {
        LevelManager LevelManager = cam.GetComponent<LevelManager>();

        if (LevelManager != null)
            LevelManager.SetScore(scoreValue);
    }

    private void Spin()
    {
        transform.Rotate(new Vector3(0, 0, rotationCoef * rotationSpeed), Space.World);
    }

    private void InitialVelocity()
    {
        float randX = Random.Range(-1.0f, 1.0f);
        float randY = Random.Range(-1.0f, 1.0f);

        rb2.velocity = new Vector2(randX, randY) * speed;
    }
}
