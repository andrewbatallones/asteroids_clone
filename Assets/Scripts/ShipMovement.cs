using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    PlayerController controls;
    Rigidbody2D rb2;

    public Transform bullet;
    public Transform bulletLocation;
    public ParticleSystem thrusters;
    public LevelManager levelManager;
    public AudioSource shootSound;
    
    [SerializeField]
    public float speed = 1f;
    [SerializeField]
    public float rotationSpeed = 1f;
    [SerializeField]
    public float reloadSpeed = 1f;

    private Vector2 turnDirection = new Vector2(0, 0);
    private float thrust = 0f;
    private float reloadTimer = 0f;


    private void Awake()
    {
        controls = new PlayerController();
        thrusters.Stop();

        SetControls();
    }

    private void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Turn();
        Move();
        Reload();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.UI.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
        controls.UI.Disable();
    }


    // Custom Functions
    public void EnableControls()
    {
        controls.Gameplay.Enable();
    }

    public void DisableControls()
    {
        controls.Gameplay.Disable();
    }

    private void SetControls()
    {
        // Thrusters 
        controls.Gameplay.Thruster.performed += _ => thrust = _.ReadValue<float>();
        controls.Gameplay.Thruster.canceled += _ => thrust = _.ReadValue<float>();

        // Turning
        controls.Gameplay.Turn.performed += _ => turnDirection = _.ReadValue<Vector2>();
        controls.Gameplay.Turn.canceled += _ => turnDirection = _.ReadValue<Vector2>();

        // Shooting
        controls.Gameplay.Fire.performed += _ => Fire();


        // Pause
        controls.UI.Puase.performed += _ => Puase();
    }

    private void Turn()
    {
        transform.Rotate(new Vector3(0, 0, -1 * turnDirection.x * rotationSpeed), Space.World);
    }

    private void Move()
    {
        if (thrust != 0f)
        {
            rb2.velocity = transform.up * -thrust * speed;
            thrusters.Play();
        }
        else
        {
            thrusters.Stop();
        }
    }

    private void Reload()
    {
        if (reloadTimer != 0)
            reloadTimer = Mathf.Max(0, reloadTimer - 0.01f);
    }

    private void Fire()
    {
        if (reloadTimer == 0)
        {
            Instantiate(bullet, bulletLocation.transform.position, transform.rotation);
            shootSound.Play();
            reloadTimer = reloadSpeed;
        }
        else
            Debug.Log("reloading..");
    }

    private void Puase()
    {
        // Puase
        if (Time.timeScale != 0)
        {
            controls.Gameplay.Disable();
            levelManager.Puase();
        }
        // Unpause
        else
        {
            controls.Gameplay.Enable();
            levelManager.Unpuase();
        }
    }
}
