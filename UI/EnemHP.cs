using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemHP : MonoBehaviour {
 	public Transform healthBar;
    public Slider healthSlider;

    public float currentHealth;
    public float maxHealth;

    public Image currenthealth;
    public Text ratioText;
    public float hitpoint=100f;
    public float maxhealth = 100f;
    public bool alive=true;

    public Slider skillSlider;
    public float currentSkill;
    public float maxSkill;
    public Image currentskill;
    public Text skillText;
    public float skillpoint;
    public float maxskill;

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
        
        SetSkillBar();
        Updateskill();
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

    public virtual void HealSkill(float heal){
        currentSkill+=heal;
        SetSkillBar();
        skillpoint+=heal;
        Updateskill();
        if(currentSkill==100||skillpoint==100){
            StartCoroutine (Skillnew());
            
        }


    }
    public virtual void SetSkillBar(){
        currentSkill=Mathf.Clamp(currentSkill,0,maxSkill);
        
       
    }
    private void Updateskill()
    {
        float ratioskill=Mathf.Clamp((skillpoint/maxskill),0,1);
        currentskill.rectTransform.localScale=new Vector3(ratioskill,1,1);
        skillText.text=(ratioskill*100).ToString("0")+'%';
    }

    //Skill條變成0
     private IEnumerator Skillnew ()
        {
            
        	yield return new WaitForSeconds (0.5f);
           currentSkill=0;
            skillpoint=0;
         }
    public virtual void SetHealthBar(){
        
        currentHealth=Mathf.Clamp(currentHealth,0,maxHealth);

        
    }

 
 
}
