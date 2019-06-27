using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	public Rigidbody rb;
	public float BulletSpeed;
    public float dis;
    Transform Player;
    float damage=5f;
    public float i ;
    public bool iseare;
	// Use this for initialization
	public virtual void Start () {
		
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
        dis=Vector3.Distance(Player.position,transform.position);
        
        // if(dis<16f){
        //     iseare=true;
        //     i = Random.Range(16.0f, 16.5f);
        // }else if(dis>16f && dis<17.5f){
        //     i = Random.Range(15f, 15.5f);
        // }

        rb = GetComponent<Rigidbody>();
		BulletSpeed=dis-i;
        print(dis);
        rb.AddRelativeForce(0, 0, BulletSpeed, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
        
		transform.rotation=Quaternion.LookRotation(rb.velocity);
	}

	void OnCollisionEnter(Collision other)
        {
            if(other.transform.tag=="Ground"){
                rb.isKinematic=true;
                Destroy(gameObject,5f);
            }
            else if(other.transform.tag=="Player" && rb.isKinematic==false){
                //attack
                damage=Mathf.Round(Random.Range(1f,10f));
                other.gameObject.GetComponent<Creature>().Hurt(damage);
                Destroy(gameObject);
           
            }else{
                Destroy(gameObject,3f);
            }

            
            
        // Update is called once per frame
        
    }

}
