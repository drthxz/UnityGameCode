using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowup : MonoBehaviour {
	public GameObject enemy;

    public GameObject [] arrow;
    public Material [] red;
    // Use this for initialization
    void Start () {
        arrow = GameObject.FindGameObjectsWithTag("Arrow");

        for(int i=0;i<6;i++){
            red[i] = arrow[i].GetComponent<Renderer>().material;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		if(enemy==null){
            for(int i=0;i<6;i++){
                red[i].color = Color.Lerp(new Color32(255, 50, 50, 255), Color.red, Mathf.PingPong(Time.time, 1));
            }
        }
    }
}
