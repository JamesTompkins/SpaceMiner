using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour {
    public bool active = false;

    private LineRenderer lineRenderer;
    private Transform shootPoint;
    private MiningLaser parent;
    private float shotLengthSeconds = 2, timeLastShot;
    private float damage;

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        shootPoint = transform.GetChild(0);
        parent = transform.parent.GetComponent<MiningLaser>();
        damage = parent.damagePerBarrel;
    }

    public void Activate() {
        active = true;
        lineRenderer.enabled = true;
    }

    public void Deactivate() {
        active = false;
        lineRenderer.enabled = false;
    }


    void Update() {
        if (!active) {
            return;
        }

        if (parent.target == null) {
            Deactivate();
            return;
        }

        //in case laser hits something before asteroid
        RaycastHit hit;
        bool rayHit = Physics.Raycast(shootPoint.position, -transform.up, out hit);
        Asteroid asteroidHit = null;

        if (!rayHit) {
            asteroidHit = parent.target;
        }

        //the point where the raycast hit, not neccessarily an asteroid
        Debug.Log(hit.transform.gameObject);
        asteroidHit = hit.transform.GetComponent<Asteroid>();

        //if object hit is asteroid, damage it
        if (asteroidHit) {
            float damagePerFrame = damage / shotLengthSeconds * Time.deltaTime;
            //Debug.Log(asteroidHit.Mine(damagePerFrame) + " " + asteroidHit.transform.position);
            if (asteroidHit.Mine(damagePerFrame)) {
                Deactivate();
                return;
            }
        }

        //Updating LineRenderer
        lineRenderer.SetPosition(0, shootPoint.position);
        lineRenderer.SetPosition(1, hit.point);
        lineRenderer.startWidth = parent.width;
        lineRenderer.endWidth = parent.width;
    }
}
