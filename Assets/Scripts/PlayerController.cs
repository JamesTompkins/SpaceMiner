using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    //general public variables
    public GameObject[] mounts;

    //UI
    private GameObject cooldownPanel;
    private float buttonWidth; //full width of cooldown panels

    //general private variables
    private MiningLaser[] lasers; //all the laser components
    private MountPoint[] mountPoints; //all of the transforms defined as mount points
    private Asteroid target;
    private TargetSymbol targetSymbol;

    void Start() {
        InitializeVariables();

        CreateMounts();
    }

    void InitializeVariables() {
        //finding all mount points
        mountPoints = GetComponentsInChildren<MountPoint>();
        //initializing size of laser storage
        lasers = new MiningLaser[mountPoints.Length];

        //finding UI elements
        cooldownPanel = GameObject.Find("CooldownPanel");
        buttonWidth = cooldownPanel.transform.parent.GetComponent<RectTransform>().rect.width;

        targetSymbol = FindObjectOfType<TargetSymbol>();
        targetSymbol.gameObject.SetActive(false);
    }

    void CreateMounts() {
        for (int i = 0; i < mountPoints.Length; i++) {
            lasers[i] = mountPoints[i].setMount(mounts[i]).GetComponent<MiningLaser>(); ;
        }
    }

    void Update() {
        //space pressed - shoot lasers
        if (Input.GetAxis("Fire1") > 0) {
            Mine();
        }

        //t pressed - target closest
        if (Input.GetAxis("TargetClosest") > 0) {
            TargetClosest();
        }

        //mouse 0 pressed - target closest
        if (Input.GetAxis("Target") > 0) {
            TargetClicked();
        }
    }

    void TargetClicked() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.tag == "Clickable") {
                setTarget(hit.collider.GetComponent<Asteroid>());
            }
        }
    }

    void TargetClosest() {
        float leastDistance = float.MaxValue;
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

        //searching for closest asteroid
        for (int i = 0; i < asteroids.Length; i++) {
            Asteroid asteroid = asteroids[i];
            float currentDistance = Vector3.Distance(transform.position, asteroid.transform.position);

            if (currentDistance < leastDistance) {
                leastDistance = currentDistance;
                setTarget(asteroid);
            }
        }
    }
    
    void setTarget(Asteroid asteroid) {
        targetSymbol.gameObject.SetActive(true);
        targetSymbol.setTarget(asteroid.gameObject, asteroid.maxDimension);
        target = asteroid;
    }

    void Mine() {
        //shooting each laser at the asteroid
        if (target) {
            foreach (MiningLaser miningLaser in lasers) {
                miningLaser.Fire(target);
            }
        } else {
            FlashCooldown();
        }

    }

    public void ShowCooldown(float ratio) {
        float panelRight = buttonWidth * ratio;
        cooldownPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(-panelRight, 0);
    }

    private void FlashCooldown() {

    }
}
