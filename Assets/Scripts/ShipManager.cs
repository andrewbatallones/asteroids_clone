using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipManager : MonoBehaviour
{
    public HealthBar healthBar;
    public LevelManager levelManager;
    public GameObject lostMenu;
    public ParticleSystem explosion;
    public ShipMovement shipControls;

    private DataManager data;
    private AudioSource explosionSound;
    private bool lostTriggered;
    
    void Start()
    {
        data = GetComponent<DataManager>();
        explosionSound = GetComponent<AudioSource>();
        healthBar.SetMaxHealth(data.maxHealth);

        lostTriggered = false;
    }

    private void Update()
    {
        if (data.health <= 0 && !lostTriggered)
        {
            lostTriggered = true;
            levelManager.SetSpawn(false);
            
            StartCoroutine("GameLost");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DataManager otherManager = other.gameObject.GetComponent<DataManager>();

        if (otherManager != null)
            otherManager.health -= data.damage;

        healthBar.SetHealth(data.health);
    }

    IEnumerator GameLost()
    {
        shipControls.DisableControls();
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<SpriteRenderer>());

        explosionSound.Play();
        explosion.Play();
        yield return new WaitForSeconds(explosion.main.duration);

        lostMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
