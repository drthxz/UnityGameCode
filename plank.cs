using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plank : MonoBehaviour {

	float amount;
	float heal;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate(Vector3.up*Time.deltaTime*speed);
		//Destroy(gameObject,5);
	}

	void OnTriggerEnter(Collider other){
		//扣血範圍
		if(other.gameObject.tag=="Enemy"){
		amount=Mathf.Round(Random.Range(5f,10f));
		other.GetComponent<EnemHP>().TakeDamage(amount);
		heal=Mathf.Round(Random.Range(5f,10f));
		other.GetComponent<EnemHP>().HealSkill(heal);
		//Destroy(gameObject);
		}
		if(other.gameObject.tag=="EnemyNOskill"){
			amount=1f;
			other.GetComponent<EnemyNoskill>().TakeDamage(amount);

			//Destroy(gameObject);
		}
		if(other.gameObject.tag=="enemy"){
			amount=Mathf.Round(Random.Range(15f,20f));
			other.GetComponent<Creature>().Hurt(amount);

			//Destroy(gameObject);
		}

	}
		


}
//other.gameObject.tag == "Player"
