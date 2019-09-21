using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mount : MonoBehaviour {
    public Target target;

    //can be unimplemented
    public abstract void Fire(Target asteroid);
}