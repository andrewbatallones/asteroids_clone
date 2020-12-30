using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipManager : MonoBehaviour
{
    public HealthBar healthBar;
    public LevelManager levelManager;
    public GameObject lostMenu;

    private DataManager data;
    
    void Start()
    {
        data = GetComponent<DataManager>();
        healthBar.SetMaxHealth(data.maxHealth);
    }

    private void Update()
    {
        if (data.health <= 0)
        {
            levelManager.SetSpawn(false);
            lostMenu.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DataManager otherManager = other.gameObject.GetComponent<DataManager>();

        if (otherManager != null)
            otherManager.health -= data.damage;

        healthBar.SetHealth(data.health);
    }
}
