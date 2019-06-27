using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Playercontrol : Creature {

    

	public State state;
	public enum State{
		idle,
		walk,
		jump,
		attack,
		die,
	}

    const float walkSpeed=4f;
	const float runSpeed=6f;
	const float rotSpeed=180f;
	const float dirRatio=90f;
	public bool isAttack;
	//public List<Transform> targets;
	//public Transform selectedTarget;
	//public List<Transform> tower;
	//public Transform selectedTower;
	float distance;
	float attackDistance=1;
	float attackangle=120;
	Vector3 pos;
	float angle;
	float jumpForce=10f;
	int jumpNum;
	float time=0;
	public Transform cameraGet;

	public GameObject katana;

    GameObject map;
	GameObject control;

	bool setCamera;

	Transform enemy;
	GameObject slashAttcak;
	GameObject shieldAttack;
	bool temp;
	

	// Use this for initialization
	protected override void Start () {

        base.Start();
		state=State.idle;
		katana=GameObject.Find("mixamorig:RightHand/katana");
		katana.SetActive(false);

        map= GameObject.Find("Canvas/Map");
		control= GameObject.Find("Canvas/Control");
        map.SetActive(false);
		StartCoroutine(delay(5f));
		
		slashAttcak=GameObject.Find("Slash_1");
		shieldAttack=GameObject.Find("Slash_2");
		slashAttcak.SetActive(false);
		shieldAttack.SetActive(false);


    }
	

	void Update () {
		
		if(Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Joystick1Button3)){
			Score.attackMode=!Score.attackMode;
			katana.SetActive(Score.attackMode);
		}

		if(state!=State.jump){

			if(isAttack== false){
				if((Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Joystick1Button2)) && Score.attackMode==true){
				katana.SetActive(true);
				Attack();
				}

				if((Input.GetMouseButton(1) || Input.GetKeyDown(KeyCode.Joystick1Button1)) && Score.attackMode==true){
					katana.SetActive(true);
					Attack();
				}

			}
			
		}
		

        if (Input.GetKeyUp(KeyCode.M) || Input.GetKeyUp(KeyCode.Joystick1Button5) ){
            map.SetActive(false);
			control.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Joystick1Button5) )
        {
            map.SetActive(true);
			control.SetActive(true);
        }

		if(isAttack==false)
		move();
		UpdateHealthbar();

    }


	void move(){
		if(hitpoint <= 0){
			return;
		}


        float v = Input.GetAxis("Vertical");
 
		float h = Input.GetAxis("Horizontal");


        Vector3 vec=Vector3.ClampMagnitude(new Vector3(h,0,v),1f);
	
		Quaternion q=Quaternion.Euler(0,Camera.main.transform.rotation.eulerAngles.y,0);
		vec=q*vec;
		
		//anim.applyRootMotion=false;
		if(vec.magnitude>0f){
			float nowRot=transform.rotation.eulerAngles.y;
			float targetRot=Mathf.Atan2(vec.x,vec.z)*Mathf.Rad2Deg;
			float targetRotOrg=targetRot;
			float deltaRot=Mathf.Abs(Mathf.DeltaAngle(nowRot,targetRot));
			float rspeed=rotSpeed*Time.deltaTime;
			
			if(deltaRot>rspeed){
				targetRot=Mathf.LerpAngle(nowRot,targetRot,rspeed/deltaRot);
			}

			transform.rotation=Quaternion.Euler(0,targetRotOrg,0);
			float dir=Mathf.DeltaAngle(nowRot,targetRotOrg)/dirRatio;
			

		}
	
		float speed=vec.magnitude;
		Vector3 move=transform.forward*speed;
		
		if(Mathf.Abs(speed)>=0f){
		if(time>=2){
			anim.SetFloat("speed",speed*2);
			move*=runSpeed;
			
		}else {
			anim.SetFloat("speed",speed);
			move*=walkSpeed;
			time+=Time.deltaTime;
		}
		
		}
		
		if(Mathf.Abs(speed)<=0.05f){
			speed=0;
			time=0;
		}
		move.y=rbody.velocity.y;
		rbody.velocity=move;

		if(state==State.idle){
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
			{	
				if(isAttack==false){
					jumpNum++;
				}
				
			}
		}
		jump();

	}

	void jump(){
				if(jumpNum==1){
				state=State.jump;
				rbody.AddForce(transform.up * jumpForce * 1f, ForceMode.Impulse);
				anim.applyRootMotion=true;
				anim.SetTrigger("Jump");
				jumpNum++;
				}else{
					Ray ray=new Ray(transform.position,-transform.up);
					RaycastHit hit;
					if(Physics.Raycast(ray, out hit,0.1f)){
						if(hit.transform.tag=="Ground"){
							jumpNum=0;
							state = State.idle;
						}
					}
				}

	
	}

	   public override void Hurt(float damage)
    {
        base.Hurt(damage);
        if (hitpoint >= 0)
        {
            //anim.SetTrigger("Hit");
			
			anim.SetBool("Die", false);
            //state = State.idle;
            //print("Player hp: " + hitpoint);

        }
        else if(hitpoint <= 0)
        {
			anim.SetBool("Die", true);
            state = State.die;
            //Destroy(gameObject, 5f);
            
        }
		UpdateHealthbar();
    }

	IEnumerator DoDamage(float delay)
    {
		state=State.attack;
		isAttack=true;
		//player.GetComponent<Creature>().Hurt(damage);
		yield return new WaitForSeconds(delay);
		isAttack=false;
		slashAttcak.SetActive(false);
		shieldAttack.SetActive(false);
    }

	IEnumerator delay(float delay)
    {
		
		yield return new WaitForSeconds(delay);
		control.SetActive(false);
    }

    


	void Attack(){
		
		

		if(Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Joystick1Button2)){
			slashAttcak.SetActive(true);
			anim.SetTrigger("Attack");
		}
		if(Input.GetMouseButton(1) || Input.GetKeyDown(KeyCode.Joystick1Button1)){
			shieldAttack.SetActive(true);
			anim.SetTrigger("Shield Attack");
		}

		StartCoroutine(DoDamage(1.5f));

	}
	
	public override void UpdateHealthbar(){
		base.UpdateHealthbar();
		if (hitpoint >= 0)
        {
			
			anim.SetBool("Die", false);
            //state = State.idle;

        }
        else if(hitpoint <= 0)
        {
			anim.SetBool("Die", true);
            state = State.die;
            
        }
	}




}