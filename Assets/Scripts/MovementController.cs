using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviour {
    public static float maxSpeed = 15;
    const float rotationSpeedAuto = 0.05f, maxObjectDistance = 10;
    float moveSpeed = 0;
    public Vector3 targetPosition = Vector3.zero;

    private Transform ship;
    CharacterController characterController;
    PlayerController playerController;
    Toggle autoToggle;

    //UI Elements
    private SpeedBar speedBar;

    void Start() {
        characterController = FindObjectOfType<CharacterController>();
        playerController = GetComponent<PlayerController>();
        ship = characterController.transform;
        speedBar = FindObjectOfType<SpeedBar>();
        autoToggle = GameObject.Find("Auto Rotate Toggle").GetComponent<Toggle>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        //TODO: raycast ahead too slow when close
        Vector3 offset = targetPosition - transform.position;
        float distanceBasedSpeed = Mathf.Abs(maxObjectDistance -  Vector3.Magnitude(offset));

        //back and forth motion
        moveSpeed += Input.GetAxis("Vertical") / 5;
        float distanceSpeed = Mathf.Min(distanceBasedSpeed, maxSpeed);
        moveSpeed = Mathf.Clamp(moveSpeed, 0, distanceSpeed);
        speedBar.setSpeedBar(moveSpeed);

        Vector3 motion = new Vector3();
        motion += ship.forward * moveSpeed;

        //return to height 0 if not there
        motion.y -= transform.position.y;

        motion *= Time.deltaTime;
        characterController.Move(motion);

        //character auto rotation
        if (autoToggle.isOn) {
            Quaternion target = Quaternion.LookRotation(offset);
            transform.rotation = Quaternion.Lerp(transform.rotation, target, rotationSpeedAuto);
        }
    }
}
