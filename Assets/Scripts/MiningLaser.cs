using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningLaser : Mount {
    public float damagePerBarrel = 10;
    public float width;

    private Slider healthBar;
    private Barrel[] barrels;
    private PlayerController playerController;

    private bool firing = false;
    private float shotLengthSeconds = 2, timeLastShot, maxLineWidth = 0.1f;

    private void Start() {
        barrels = GetComponentsInChildren<Barrel>();
        playerController = FindObjectOfType<PlayerController>();
        healthBar = GameObject.Find("HealthSlider").GetComponent<Slider>();
    }

    private void Update() {
        HandleFiring();
    }

    //function called when laser is fired
    public override void Fire(Target targetedObj) {
        if (!firing && targetedObj.gameObject) {
            target = targetedObj;
            firing = true;

            healthBar.maxValue = targetedObj.maxHealth;

            StartLaser();
        }
    }

    private void HandleFiring() {
        if (!firing) {
            return;
        }

        //time since last shot
        float timeSinceLastShot = Time.time - timeLastShot;

        if (timeSinceLastShot > shotLengthSeconds || target == null) {
            //Stop loop and hide lasers
            firing = false;
            foreach(Barrel barrel in barrels) {
                barrel.Deactivate();
            }
            return;
        }

        //laser shrinks over time from maxWidth to 0
        float widthRatio = 1 - (timeSinceLastShot / shotLengthSeconds);
        playerController.ShowCooldown(widthRatio);

        //providing shrinking width to each barrel
        width = widthRatio * maxLineWidth;
        width = Mathf.Clamp(width, 0, maxLineWidth);

        healthBar.value = target.health;

        //handling linerenderer and rotation of turrets
        transform.LookAt(target.transform.position);
    }

    //starts timer for linerenderer and aims the laser where it should be
    private void StartLaser() {
        timeLastShot = Time.time;
        foreach (Barrel barrel in barrels) {
            barrel.Activate();
        }
    }

}
