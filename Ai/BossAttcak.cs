using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttcak : MonoBehaviour
{
    float amount;
	float heal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider other){


		if(other.gameObject.tag=="Player"){
			amount=Mathf.Round(Random.Range(10f,15f));
			other.GetComponent<Creature>().Hurt(amount);

			//Destroy(gameObject);
		}
	}
}
