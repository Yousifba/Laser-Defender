using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSensitivity = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 500;

    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 12f;
    [SerializeField] float projectileFiringPeriod = 0.05f;
    [SerializeField] GameObject laserProjectile;

    [Header("Particles")]
    [SerializeField] GameObject explosionParticles;
    [SerializeField] float explosionDuration = 1f;

    [Header("Audio")]
    [SerializeField] [Range(0f, 1f)] float deathSFXVolume = 1f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] [Range(0f, 1f)] float laserSFXVolume = 0.4f;
    [SerializeField] AudioClip hitSFX;

    Coroutine firingCoroutine;
    Level level;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundries();
        level = FindObjectOfType<Level>();
    }

     // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1") && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserProjectile, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, projectileSpeed);
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserSFXVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void SetUpMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSensitivity;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSensitivity;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, deathSFXVolume);
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        var explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(explosion, explosionDuration);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        level.WaitAndLoad();
    }
}
