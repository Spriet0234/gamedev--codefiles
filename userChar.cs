using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userChar : MonoBehaviour
{
    [SerializeField] float speed = 10.0f; 
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject projectilePrefab; 
    [SerializeField] Transform firePoint; 
    [SerializeField] float projectileSpeed = 10f; 

    [SerializeField] AudioSource walkingSound; // Reference to the AudioSource for walking sound
    [SerializeField] AudioSource shootingSound;

    private float lastShotTime; // Time since the last shot was fired
    private float shootCooldown = .5f; // 1 second cooldown

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        walkingSound.Stop();
        shootingSound.Stop();
        lastShotTime = -shootCooldown; // Ensure the player can shoot immediately upon starting
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time - lastShotTime >= shootCooldown)
        {
            Shoot();
            lastShotTime = Time.time; // Update the lastShotTime to the current time
        }
    }

    public void Movement(Vector3 direction)
    {
        rb.velocity = direction * speed;

        if (direction != Vector3.zero && !walkingSound.isPlaying)
        {
            walkingSound.Play();
        }
        else if (direction == Vector3.zero && walkingSound.isPlaying) 
        {
            walkingSound.Stop();
        }
    }
    void Shoot()
{
    // Calculate direction from the fire point to the mouse position
    Vector3 shootingDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
    shootingDirection.z = 0; // Ensure there is no movement in the z-axis
    shootingDirection.Normalize(); // Normalize the vector to a unit vector

    // Calculate the angle in radians
    float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;

    // Instantiate the projectile at the fire point with rotation towards the shooting direction
    Quaternion rotation = Quaternion.Euler(0f, 0f, angle - 90); // Adjusting by 90 degrees if needed, depending on your sprite orientation
    GameObject projectile = Instantiate(projectilePrefab, firePoint.position, rotation);

    Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();

    // Set the projectile to move in the calculated direction
    rbProjectile.velocity = shootingDirection * projectileSpeed;

    shootingSound.Play();
}

}