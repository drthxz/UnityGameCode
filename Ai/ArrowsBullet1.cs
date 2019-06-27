using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsBullet1 : Arrow {
      
    override public void Start()
        {
            i = Mathf.Round(Random.Range(14.0f, 14.5f));
            
            transform.Rotate(new Vector3(50, 0, 0));
            base.Start();
        }
        void Update()
        {
            transform.Rotate(new Vector3(1 * Time.deltaTime, 0, 0));
        }
     
        
    }
