using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager_count : MonoBehaviour {
	public float count;
	public float Total;
	public Transform Boss;
	Transform Player;
	public GameObject VillagerGroup;
	public Playercontrol playercontrol;
	public GameObject key;
	// Use this for initialization
	void Start () {
		Total=transform.childCount;
		Player=GameObject.Find("Player").transform;
		playercontrol=GameObject.Find("Player").GetComponent<Playercontrol>();
		key.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		count=transform.childCount;
		Create();
	}

	void Create(){
		if(count==0){
			key.SetActive(true);
			return;
		}
	}

}
