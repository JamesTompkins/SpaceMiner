using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float minCameraDistance, maxCameraDistance, cameraDistance, cameraHeight, scrollSpeed;

    private Vector3 offset;
    private Transform ship;
    // Start is called before the first frame update
    void Start() {
        ship = FindObjectOfType<PlayerController>().transform;
        offset = transform.position - ship.position;
    }

    // Update is called once per frame
    void FixedUpdate() {
        cameraDistance -= Input.mouseScrollDelta.y * Time.deltaTime * scrollSpeed;
        cameraDistance = Mathf.Clamp(cameraDistance, minCameraDistance, maxCameraDistance);
        Vector3 offset = ship.position  + new Vector3(0, cameraHeight);
        transform.position = offset - (ship.forward * cameraDistance);
        transform.LookAt(ship);
    }
}
