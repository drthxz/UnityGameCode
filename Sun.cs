using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
	float temp;
    public Transform moon;
    EnemyHealthManager04 town;
    GameObject boss;

    GameObject [] fire;
    GameObject[] light;
	// Use this for initialization
	void Start () {
        moon = GameObject.Find("moon").transform;

        town = GameObject.Find("stoneTower").GetComponent<EnemyHealthManager04>();
        boss = GameObject.Find("Boss");
        boss.SetActive(false);

        fire = GameObject.FindGameObjectsWithTag("BrazierFire");

        light = GameObject.FindGameObjectsWithTag("BrazierLight");
        for(int i=0; i < light.Length; i++)
        {
            light[i].GetComponent<Light>().range = 0;
            fire[i].SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
		if(town==null){
            temp += Time.deltaTime;
			if(temp <= 5){
				transform.RotateAround(Vector3.zero,Vector3.right,30f*Time.deltaTime);
				transform.LookAt(Vector3.zero);

                moon.RotateAround(Vector3.zero, Vector3.right, 30f * Time.deltaTime);
                moon.LookAt(Vector3.zero);
                boss.SetActive(true);
			}

            if (temp >= 2.5f)
            {
                for (int i = 0; i < light.Length; i++)
                {
                    light[i].GetComponent<Light>().range = Mathf.Lerp(0f, 30f, temp/ 10f);
                    fire[i].SetActive(true);
                }
            }
		}
	}
}
