using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountPoint : MonoBehaviour {
    public GameObject turret;

    public GameObject setMount(GameObject mount) {
        turret = Instantiate(mount, transform);
        return turret;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Vector3 end = transform.position + (transform.up);
        Gizmos.DrawWireCube(transform.position, new Vector3(0.1f, 0.1f, 0.1f));
        Gizmos.DrawLine(transform.position, end);
    }
}
