using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Boss : Creature
{
    public State state;
    public enum State
    {
        think,
        jump,
        move,
        find,
        die,
    }

    public float attackRange;
    public Transform player;
    public Creature playerHp;
    float moveTarget;

    float attackDistance = 5;
    float lookangle = 120;
    float attackangle = 60;
    int speed = 4;

    bool isAttack;
    bool isFind;
    float delay;
    
    public GameObject Hp;
    public bool isDie;
    float distance;

    public Transform jumpTarget;

    GameObject dustStorm;
    protected override void Start()
    {
        base.Start();
        //player = GameObject.Find("Player").transform;
        //playerHp = GameObject.Find("Player").GetComponent<Creature>();
        dustStorm = GameObject.Find("DustStorm");
        dustStorm.SetActive(false);
        
        state = State.jump;
        Hp.SetActive(false);
        Hp = transform.GetChild(0).gameObject;
        currentHealth = Hp.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponentInChildren<Image>();
        currentText = Hp.transform.GetChild(1).GetComponentInChildren<Text>();
    }
    private void Init()
    {
        anim.SetBool("Die", false);
        
        hitpoint = 10;

    }

    void Update()
    {
            switch (state)
            {
                case State.jump:
                    Jump();
                    break;
                case State.move:
                    Move();
                    break;
                case State.find:
                    Find();
                    break;
                case State.die:
                    Die();

                    break;

            }
        
        UpdateHealthbar();
    }

    private IEnumerator makenew()
    {

        yield return new WaitForSeconds(3f);
        Hp.SetActive(false);
    }

    void Find()
    {
        if (isDie == false)
        {
            if (isAttack == false)
            {
                if (FindedPlayer())
                {
                    return;
                }
            }
        }
    }

    bool FindedPlayer()
    {
        
        distance = Vector3.Distance(player.position, transform.position);
        Vector3 enemyvec = transform.rotation * Vector3.forward;
        Vector3 vec = player.position - transform.position;
        Vector3 pos = transform.position + vec;
        float angle = Mathf.Acos(Vector3.Dot(enemyvec.normalized, vec.normalized)) * Mathf.Rad2Deg;


        if (playerHp.hitpoint <= -1)
        {
            state=State.think;
        }

        if (distance < 30)
        {

            if (angle <= lookangle * 0.8f)
            {
                isFind = true;
            }
        }

        if (isFind == true)
        {
            anim.SetTrigger("Walk");
            
            if (hitpoint>50f && distance < 5f)
            {

                Attack();

                return true;
            }else if(hitpoint<50f){
                
                float temp=Mathf.Round(Random.Range(1,3));

                if(temp==1){
                    if(distance < 5f){
                        Attack();
                    }
                }else if(distance < 10f){
                    JumpAttack();
                }
            }
            if (distance > 1f)
            {
                transform.LookAt(player.transform);
            }
            
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            return true;
        }
        else
        {
            return false;
        }
    }


    bool Attack()
    {
        dustStorm.SetActive(false);
        Hp.SetActive(true);
        anim.SetTrigger("Attack");
        isAttack = true;
        player.GetComponent<Creature>().Hurt(5);
        StartCoroutine(Delay(3));
        return false;
    }

    bool JumpAttack()
    {
        anim.SetTrigger("JumpAttack");
        Hp.SetActive(true);
        isAttack = true;
        //player.GetComponent<Creature>().Hurt(10);
        
        StartCoroutine(Delay(3));
        
        return false;
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(2f);
        dustStorm.SetActive(true);
        yield return new WaitForSeconds(delay);
        isAttack = false;
        dustStorm.SetActive(false);
    }

    public override void Hurt(float damage)
    {
        StartCoroutine(makenew());
        isFind = true;
        base.Hurt(damage);
        if (hitpoint > 0)
        {
//            print("Enemy hp: " + hitpoint);
        }
        else if (hitpoint <= 0)
        {
            isDie = true;
            state = State.die;


        }
        UpdateHealthbar();
    }
    void Die()
    {
        anim.SetBool("Die", true);
        rbody.isKinematic=true;
        rbody.useGravity=false;
        col.isTrigger=true;
        transform.Translate(new Vector3(0, 0, 0));
        Destroy(gameObject, 5f);
    }
    void Jump()
    {
        delay += Time.deltaTime;
        if (delay >1f && delay < 2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, jumpTarget.position, 10f * Time.deltaTime);
        }else if (delay > 15f)
        {
            state = State.find;
        }
        
    }

    void Think(){

        if(playerHp.hitpoint>0){
            state=State.move;
        }

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

    void Move(){

        if(playerHp.hitpoint>0){
            state=State.move;
        }

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

    }

    public override void UpdateHealthbar()
    {
        base.UpdateHealthbar();
    }
}
