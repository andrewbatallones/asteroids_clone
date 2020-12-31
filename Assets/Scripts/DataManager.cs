using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    public float maxHealth = 100.0f;
    [SerializeField]
    public float damage = 50.0f;

    public float health;

    void Start()
    {
        health = maxHealth;
    }
}
