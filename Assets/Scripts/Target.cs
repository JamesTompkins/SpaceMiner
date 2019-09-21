using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Target : MonoBehaviour {
    public float maxHealth, health;
    public float size;

    private void Start() {
        health = maxHealth;
    }

    public abstract bool Damage(float amount);
}
