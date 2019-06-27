using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Villagercontrol : Creature {

	public State state;
	float delay;
	public enum State{
		think,
		move,
		hited,
		die,
	}

	float moveTarget;
	int spped=4;
	public GameObject Hp;
	float time;
	
	// Use this for initialization
	protected override void Start () {
		base.Start();
		state=State.think;
		hitpoint = 10;
		
		StartCoroutine(delaytime());
		Hp=transform.GetChild(0).gameObject;
		currentHealth=Hp.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponentInChildren<Image>();
		currentText=Hp.transform.GetChild(1).GetComponentInChildren<Text>();
		Hp.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		switch (state)
        {
            case State.think:
                Think();
                break;
            case State.move:
                Move();
                break;
			case State.hited:
				Hited();
				break;
            case State.die:
                anim.SetBool("Die",true);
				Destroy(gameObject,5f);
                break;

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
				state=State.think;
			}else{
				state=State.move;
			}
			return;
		}else{
			delay-=Time.deltaTime;
		}
		
	}

	public override void Hurt(float damage){
		Hp.SetActive (true);
		base.Hurt(damage);
		if(hitpoint>0){
			anim.SetTrigger("Hit");
			state=State.hited;
			//print("vil hp: " + hitpoint);
		}
		else{
			state=State.die;
			
		}
		UpdateHealthbar();
	}

	public override void UpdateHealthbar(){
		base.UpdateHealthbar();
	}

	void Move(){
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
			}else{
				moveTarget=0;
				state=State.think;
			}
	}

	void Hited(){
		time+=Time.deltaTime;
		anim.SetTrigger("Hited");
		if(hitpoint<0){
			rbody.isKinematic=true;
        	rbody.useGravity=false;
        	col.isTrigger=true;
			state=State.die;
		}

		if(time>3){
			time=0;
			state=State.think;
		}
		
		
	}
}
