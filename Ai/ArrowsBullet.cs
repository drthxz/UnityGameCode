using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsBullet : Arrow {

    
    
        override public void Start()
        {   
            //attack distance 
            i = Mathf.Round(Random.Range(20f, 25f));
            base.Start();

        }
        void Update()
        {
            
        }

         
}

