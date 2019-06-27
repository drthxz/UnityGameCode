using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    public GameObject shellPrefab;
    float shotSpeed = 1000;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shot();

        }



    }
    public void Shot()
    {
        GameObject shell = (GameObject)Instantiate(shellPrefab, transform.position, Quaternion.identity);
        Rigidbody shellRigibody = shell.GetComponent<Rigidbody>();
       
        Random speed=new Random();
        shotSpeed=Mathf.Round(Random.Range(1000f,2500f)); 
        shellRigibody.AddForce(transform.forward * shotSpeed);
        
}
}
