using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherTargettingsystem : Turrettargettingsystem {

    float lastFired;
    public float fireDelay = 2f;
    

    // Use this for initialization
    override public void Start()
    {
        lastFired = Time.deltaTime;
        base.Start();
    }

}
