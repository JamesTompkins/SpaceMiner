﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Target {
    public float maxStartingHealth, minStartingHealth;
    public GameObject particles;

    private Vector3 startingScale;
    private float maxRotation = 5f, minSize = 0.2f;
    public Vector3 rotation;

    void Start() {
        //randomizing size and rotation
        Quaternion offset = new Quaternion {
            x = Random.Range(0f, 1f),
            y = Random.Range(0f, 1f),
            z = Random.Range(0f, 1f)
        };
        transform.rotation = offset;

        rotation = new Vector3 {
            x = Random.Range(-maxRotation, maxRotation),
            y = Random.Range(-maxRotation, maxRotation),
            z = Random.Range(-maxRotation, maxRotation)
        };

        startingScale = transform.localScale;
        startingScale.x = Random.Range(0.8f, 2.5f);
        startingScale.y = Random.Range(0.8f, 2.5f);
        startingScale.z = Random.Range(0.8f, 2.5f);

        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        size = Mathf.Max(collider.radius, collider.height);

        transform.localScale = startingScale;
        maxHealth = Random.Range(minStartingHealth, maxStartingHealth);
        health = maxHealth;
    }

    void Update() {
        transform.Rotate(rotation * Time.deltaTime);
    }


    //returns if the asteroid was destroyed
    public override bool Damage(float amount) {
        if (amount > health) {
            amount = health;
        }

        health -= amount;

        //shrinks asteroid as its health decreases
        float multiplier = health / maxHealth;
        multiplier = Mathf.Clamp(multiplier, minSize, 1);
        transform.localScale = (startingScale) * multiplier;

        if (health <= 0) {
            Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
            if (asteroids.Length < 2) {
                FindObjectOfType<AsteroidCreator>().CreateAsteroids();
            }

            Instantiate(particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
