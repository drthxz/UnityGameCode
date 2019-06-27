using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creature : MonoBehaviour {
	public Image currentHealth;
    public Text currentText;
    public float hitpoint=100f;
    public float maxHealth = 100f;
	public Animator anim;
	protected Rigidbody rbody;
	protected Collider col;
	public float damage;
	
	// Use this for initialization
	protected virtual void Start () {
		anim=GetComponent<Animator>();
		rbody=GetComponent<Rigidbody>();
		col=GetComponent<CapsuleCollider>();
		
	}

    // Update is called once per frame


    //Hurt
    public virtual void Hurt(float damage)
    {
        hitpoint -= damage;
    }

    //UpdateHealthbar
    public virtual void UpdateHealthbar()
        {
            float ratio=Mathf.Clamp(hitpoint/maxHealth,0,1);
            currentHealth.rectTransform.localScale=new Vector3(ratio,1,1);
            currentText.text=(ratio*100).ToString("0")+'%';
        }
}
