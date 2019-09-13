using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementController : MonoBehaviour {
    public float rotationSpeed = 70;
    public static float maxSpeed = 15;
    const float rotationSpeedAuto = 0.05f;
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

        //back and forth motion
        moveSpeed += Input.GetAxis("Vertical") / 5;
        moveSpeed = Mathf.Clamp(moveSpeed, 0, maxSpeed);
        speedBar.setSpeedBar(moveSpeed);

        Vector3 motion = new Vector3();
        motion += ship.forward * moveSpeed;

        //return to height 0 if not there
        motion.y -= transform.position.y;

        motion *= Time.deltaTime;
        characterController.Move(motion);

        //character auto rotation
        if (autoToggle.isOn) {
            Quaternion target = Quaternion.LookRotation(targetPosition - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, target, rotationSpeedAuto);
        }

        //character manual rotation
        //Vector3 rotation = new Vector3();
        //rotation.y += Input.GetAxis("Horizontal") * rotationSpeed;

        //rotation *= Time.deltaTime;
        //ship.Rotate(rotation);
    }
}
