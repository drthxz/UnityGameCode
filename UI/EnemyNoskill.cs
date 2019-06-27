using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyNoskill : MonoBehaviour {

 	public Transform healthBar;
    public Slider healthSlider;

    public float currentHealth;
    public float maxHealth;

    public Image currenthealth;
    public Text ratioText;
    public float hitpoint=100f;
    public float maxhealth = 100f;
    public bool alive=true;


    public GameObject HP1;
    public GameObject HP12;
    public GameObject obj;

    public Rigidbody RB;
    public CapsuleCollider CC;
    public BoxCollider BC;

	// Use this for initialization
	 public virtual void Start()
    {   
        //血條消失
        HP1.SetActive(false);
        HP12.SetActive(false);
        UpdateHealthbar();
        alive=true;
        CC=GetComponent<CapsuleCollider>();
        BC=GetComponent<BoxCollider>(); 
        RB=GetComponent<Rigidbody>();
        //1秒後每5秒扣血
       // InvokeRepeating("DoDamage",1f,5f);
    }

public virtual void UpdateHealthbar()
    {
        
        float ratio=Mathf.Clamp((hitpoint/maxhealth),0,1);
        currenthealth.rectTransform.localScale=new Vector3(ratio,1,1);
        ratioText.text=(ratio*100).ToString("0")+'%';
    }
    public virtual void DoDamage()
    {//扣10滴血
        TakeDamage(10f);
        
    }
    public virtual void Update()
    {
        
       Vector3 currentPos=transform.position;
        
        SetHealthBar();
        UpdateHealthbar();
        healthBar.LookAt(Camera.main.transform);

    }

    public virtual void Plus(int i){
        Score.score+=i;
        
    }

    //受到攻擊後扣血
   public virtual void TakeDamage(float amount){
        //if(!alive){
        //    return;
        //}

        //if(currentHealth<=0){
       //     currentHealth=0;
       //     alive=false;
       // }
        hitpoint-=amount;
        UpdateHealthbar();
        currentHealth-=amount;
        SetHealthBar();
        HP12.SetActive (true);
       
       

    }


    public virtual void SetHealthBar(){
        
        currentHealth=Mathf.Clamp(currentHealth,0,maxHealth);

        
    }

 
 
}
