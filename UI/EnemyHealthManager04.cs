using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager04 : EnemyNoskill {

    public GameObject [] explosion;//爆炸特效

    private Vector3 pos;

    float time;

    Transform bomb;
    Transform smoke;

    Transform town;
    GameObject arrow;
    override public  void Start()
    {   
        //血條消失
        base.Start();
  
        pos = transform.position;
        town=gameObject.transform.Find("stoneTower01_poly");
        arrow=transform.Find("Bow").gameObject;
    }
    
    override public void Update()
    {
        
        base.Update();
        
        Bomb();



        //StartCoroutine (Explosion());
    }

    override public void DoDamage()
    {//扣10滴血
        base.DoDamage();
        
    }
    

     private IEnumerator makenew ()
{
	
	yield return new WaitForSeconds (3f);
    HP1.SetActive(false);
    HP12.SetActive (false);
  }
    
    //受到攻擊後扣血
    override public void  TakeDamage(float amount){
        //if(!alive){
        //    return;
        //}

        //if(currentHealth<=0){
       //     currentHealth=0;
       //     alive=false;
       // }

        
        base.TakeDamage(amount);
        HP1.SetActive (true);
        StartCoroutine (makenew());
      
    // if(currentHealth<=0||hitpoint<=0)
    //     {
    //         isdie=true;
    //         if(hitpoint>-8){
    //         CC.isTrigger = true;
    //         RB.useGravity=true;
    //         RB.isKinematic=false;
            
            
    //         }

    //     }



    }

    void Bomb(){

        if(currentHealth<=0||hitpoint<=0){
            time+=Time.deltaTime;
            town.position = new Vector3(Mathf.Sin(Time.time) * 1.0f + pos.x, pos.y-Time.time*2f , pos.z);
            
            Destroy (gameObject, 8f);
        
            if(time<0.05f){
                Instantiate (explosion[0], new Vector3(pos.x,pos.y+2f,pos.z+3f), transform.rotation,transform);
            }
            if(time<1f){
                Instantiate (explosion[1], new Vector3(pos.x,pos.y+2f,pos.z+3f), transform.rotation,transform);
            }
            
            arrow.SetActive(false);
        }
    }
    

    // private IEnumerator Explosion ()
    // {
        
    //     yield return new WaitForSeconds (5f);
    //     //碰撞後產生爆炸
    //         Plus(1);           
    //         Debug.Log(Score.score);
    //         Instantiate (explosion, transform.position, transform.rotation);
    //         if(Score.score==5){
    //             GetComponent<Null>().enabled=true;
                
    //             }
    // }
 
 
    
    override public void SetHealthBar(){
        
        base.SetHealthBar();
         healthSlider.value=currentHealth/maxHealth;       
    }


}
