using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsGun : MonoBehaviour {

    public GameObject [] bullet;
    private ArrowsGun arrowsgun;
    private EnemyHealthManager04 enemy;

    float time;
    Rigidbody rb;
    void Start()
    {

        enemy=GameObject.Find("stoneTower").GetComponent<EnemyHealthManager04>();
        
    }


    void Update()
    {
        
    }

    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        time+=Time.deltaTime;
        
        if (other.gameObject.tag == "Player")
        {
            
            
           if(enemy.hitpoint<=0){
                //stop shooting
               StopCoroutine("Shooting");
           }else{

                //start shooting
               StartCoroutine("Shooting");
           }
           
        }
        
        
    }
    void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            time=0;
            StopCoroutine("Shooting");
        }
    }

    IEnumerator Shooting()
    {
  
        while (true)
        {
            if(time<=0.5f){
                Instantiate(bullet[0], transform.position, transform.rotation);
                rb=bullet[0].GetComponent<Rigidbody>();
            }else if(time>=0.5f){
                Instantiate(bullet[1], transform.position, transform.rotation);
                rb=bullet[1].GetComponent<Rigidbody>();
                time=0;
            }
            
            rb.velocity=transform.forward*10;
            yield return new WaitForSeconds(1);
            
             
            
        }
        
        

    }
}
