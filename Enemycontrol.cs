using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemycontrol : Creature {

  	public State state;
	public enum State{
		think,
        move,
        die,
	}

    public float attackRange;
    public Transform player;
    public Creature playerHp;
    float moveTarget;

    float attackDistance=5;
    float lookangle=120;
    float attackangle=60;
    int speed=4;

    bool isAttack;
    bool isFind;
    float delay;
    //public Transform born;
    public GameObject Hp;
    public bool isDie;
    float distance;
 protected override void Start () 
  {
        base.Start();
        player=GameObject.FindWithTag("Player").transform;
        playerHp = GameObject.FindWithTag("Player").GetComponent<Creature>();
        StartCoroutine(delaytime());
        //born=GameObject.FindWithTag("born").transform;
        state =State.think;
        hitpoint = 50;
        Hp.SetActive(false);
        Hp=transform.GetChild(0).gameObject;
		currentHealth=Hp.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponentInChildren<Image>();
		currentText=Hp.transform.GetChild(1).GetComponentInChildren<Text>();
  }
  	   private void Init()
    {
        anim.SetBool("Die", false);
       // state = State.idle;
        hitpoint = 10;
        //transform.position = new Vector3(0, 6, 0);
    }
    
      void Update () 
  {
      if(isDie==false){
      if(isAttack==false){
      if(FindedPlayer()){
          return;
      }
      }
      
      switch (state)
        {
            case State.think:
                Think();
                break;
            case State.move:
                Move();
                break;
            case State.die:
                Die();
                
                break;

        }
      }
      UpdateHealthbar();
  }


    private IEnumerator delaytime ()
	{
		
		yield return new WaitForSeconds (3f);
		rbody.constraints =RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
	}
    private IEnumerator makenew ()
    {
        
        yield return new WaitForSeconds (3f);
        Hp.SetActive(false);
    }
    void Think(){

        if(delay<=0){
            anim.SetTrigger("Idle");
            delay=Random.Range(15,25)/10f;
            int temp=Random.Range(0,5);
            if(temp<3){
                
                state = State.think;
            }else{
                state = State.move;   
            }
            return;
            }else{
                delay-=Time.deltaTime;
            }
    }
    bool FindedPlayer(){

        distance=Vector3.Distance(player.position,transform.position);
        Vector3 enemyvec=transform.rotation*Vector3.forward;
        Vector3 vec=player.position-transform.position;

        Vector3 pos=transform.position+vec;
        float angle=Mathf.Acos(Vector3.Dot(enemyvec.normalized,vec.normalized))*Mathf.Rad2Deg;

        //print(distance);

        if (playerHp.hitpoint <= -1)
        {
            return false;
        }

        if(distance<10){

            if(angle<=lookangle*0.5f){
            //Debug.DrawLine(transform.position, pos, Color.red);
                isFind=true;
            }
        }

        if(isFind==true){
            anim.SetTrigger("Walk");
            
            if(distance<1f){
                
                    Attack();
                
            return true;
            }
            if(distance>1){
                transform.LookAt(player.transform);      
		    }
            // else if(distance>10){
            //     isFind=false;
            //     transform.LookAt(born.transform);
            // }
        transform.Translate(Vector3.forward*Time.deltaTime*speed);
        return true;
        }


        // if(transform.position==born.position){
        //     //isFind=false;
        //     state=State.think;
        //     return true;
        // }
        else{
            return false;
        }
    }   
    

    bool Attack(){
        
        StartCoroutine(Dodamage(2));
        return false;
    }

    IEnumerator Dodamage(float delay){
        Hp.SetActive (true);
        anim.SetTrigger("Attack");
        isAttack=true;
        player.GetComponent<Creature>().Hurt(damage);
        yield return new WaitForSeconds(delay);
        isAttack=false;
    }

    public override void Hurt(float damage)
    {
        StartCoroutine (makenew()); 
        isFind=true;
        base.Hurt(damage);
        if (hitpoint > 0)
        {
            anim.SetTrigger("Hit");
            print("Enemy hp: " + hitpoint);
            state = State.think;
        }
        else if(hitpoint <= 0)
        {
            state = State.die;
            
            
        }
        UpdateHealthbar();
    }
    void Die(){
        anim.SetBool("Die", true);
        isDie=true;
        rbody.isKinematic=true;
        rbody.useGravity=false;
        col.isTrigger=true;
        transform.Translate(new Vector3(0,0,0));
        Destroy(gameObject, 5f);
    }
    void Move(){

        //float BornDistance=Vector3.Distance(born.position,transform.position);
        //if(BornDistance<3){
        if(delay>0){
            if(moveTarget==0){
                moveTarget=Random.Range(1,5);
            }
            Quaternion mRotation=Quaternion.Euler(0,moveTarget*90,0);
            transform.rotation=Quaternion.Slerp(transform.rotation,mRotation,Time.deltaTime*10);
            anim.SetTrigger("Walk");
            transform.Translate(Vector3.forward*Time.deltaTime);
            delay-=Time.deltaTime;
            return;
        }
        else{
            moveTarget=0;
            state=State.think;
        }
        //}
        // else{
        //     anim.SetTrigger("walk");
        //     transform.LookAt(born);
        //     transform.Translate(Vector3.forward*Time.deltaTime);
            
        // }
    }

    public override void UpdateHealthbar(){
		base.UpdateHealthbar();
	}

}