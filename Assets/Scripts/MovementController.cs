using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
    public float moveSpeed, rotationSpeed;

    private Transform ship;
    CharacterController characterController;
    // Start is called before the first frame update
    void Start() {
        characterController = FindObjectOfType<CharacterController>();
        ship = characterController.transform;
    }

    // Update is called once per frame
    void FixedUpdate() {
        //back and forth motion
        Vector3 motion = new Vector3();
        motion += ship.forward * moveSpeed * Input.GetAxis("Vertical");

        //return to height 0 if not there
        motion.y -= transform.position.y;

        motion *= Time.deltaTime;
        characterController.Move(motion);

        //character rotation
        Vector3 rotation = new Vector3();
        rotation.y += Input.GetAxis("Horizontal") * rotationSpeed;

        rotation *= Time.deltaTime;
        ship.Rotate(rotation);
    }
}
