using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetSymbol : MonoBehaviour {
    public Transform image;

    GameObject target;
    float rotationSpeed = 40f, scaleFactor = 0.7f;

    public void setTarget(GameObject gameObject, float size) {
        target = gameObject;
        transform.position = gameObject.transform.position;
        transform.localScale = new Vector3(size, size, size) * scaleFactor;
    }

    // Update is called once per frame
    void Update() {
        Move();
        checkObject();
    }

    void Move() {
        transform.LookAt(Camera.main.transform.position);
        image.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    void checkObject() {
        if (!target) {
            gameObject.SetActive(false);
        }
    }
}
