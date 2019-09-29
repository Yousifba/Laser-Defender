using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] int health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Projectile")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject laserProjectile;

    [Header("Particles")]
    [SerializeField] GameObject explosionParticles;
    [SerializeField] float explosionDuration = 1f;

    [Header("Audio")]
    [SerializeField] [Range(0f, 1f)] float deathSFXVolume = 0.6f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip laserSFX;
    [SerializeField] [Range(0f, 1f)] float laserSFXVolume = 0.4f;

    // Cached
    GameSession gameSession;

    void Start()
    {
        //gameSession = FindObjectOfType<GameSession>();
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            Fire();
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(laserProjectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, - projectileSpeed);
        AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, laserSFXVolume);
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
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().addToScore(scoreValue);
        var explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(explosion, explosionDuration);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }
}
