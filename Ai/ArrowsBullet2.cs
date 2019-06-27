using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsBullet2 : Arrow {

	// Use this for initialization
	override public void Start()
        {
			i = Random.Range(12.5f, 14.0f);
            
			transform.Rotate(new Vector3(50, 0, 0));
            base.Start();
          
        }
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(1 * Time.deltaTime, 0, 0));
	}
}
