using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;
public class ScareEnemy : Creature {

    public float attackRange;
    public Transform player;
    public Creature playerHp;
    float moveTarget;

    float attackDistance=5;
    float lookangle=60;
    float attackangle=60;
    int speed=4;

    bool isAttack;
    bool isFind;
    bool isDie;
    float delay;
    public GameObject Hp;

    protected override void Start () 
  {
        base.Start();
        player=GameObject.FindWithTag("Player").transform;
        playerHp = GameObject.FindWithTag("Player").GetComponent<Creature>();
        StartCoroutine(delaytime());
        hitpoint =50f;
        Hp.SetActive(false);
        Hp=transform.GetChild(0).gameObject;
		currentHealth=Hp.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponentInChildren<Image>();
		currentText=Hp.transform.GetChild(1).GetComponentInChildren<Text>();

        anim.applyRootMotion = false;
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
                    anim.applyRootMotion = true;
                    return;
                }
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

    bool FindedPlayer(){

        float distance=Vector3.Distance(player.position,transform.position);
        Vector3 enemyvec=transform.rotation*Vector3.forward;
        Vector3 vec=player.position-transform.position;

        Vector3 pos=transform.position+vec;
        float angle=Mathf.Acos(Vector3.Dot(enemyvec.normalized,vec.normalized))*Mathf.Rad2Deg;

        if (playerHp.hitpoint <= -1)
        {
            return false;
        }

        //print(angle);
        if (angle<=lookangle*0.5f){
            //Debug.DrawLine(transform.position, pos, Color.red);
            
            if(distance<10){
                isFind=true;
            }
        }

        if(isFind==true){
            anim.SetTrigger("Walk");
			transform.LookAt(player.transform); 
			transform.Translate(Vector3.forward*Time.deltaTime*speed);
            if(distance<1f){
			    Attack();
            }
			return true;
        }
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
        }
        else if(hitpoint <= 0)
        {
            anim.SetBool("Die", true);
            rbody.isKinematic=true;
            rbody.useGravity=false;
            col.isTrigger=true;
            Destroy(gameObject, 5f);
            isDie=true;
        }
        UpdateHealthbar();
    }


    public override void UpdateHealthbar(){
		base.UpdateHealthbar();
	}

}