using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamera : MonoBehaviour
{
    enum State{
        openRock,
        isBoss,
    }

    State state;
    float maxRotation = 30f;
    float time;

    GameObject stone;
    GameObject boss;
    GameObject stoneTower;
    bool isopen;

    GameObject player;

    Camera setCamera;
    // Start is called before the first frame update
    void Start()
    {
        stone = GameObject.Find("Rock");
        stoneTower = GameObject.Find("stoneTower");
        boss=GameObject.Find("Boss");

        setCamera=GameObject.Find("BossCamera").GetComponent<Camera>();
        player=GameObject.Find("Player");
        setCamera.enabled=false;

        state=State.openRock;
    }

    // Update is called once per frame
    void Update()
    {
    
        switch (state){
            case State.openRock:
                Rock();
                break;
            case State.isBoss:
                Boss();
                break;
        }
        
        

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isopen = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isopen = false;
        }
    }

    void Rock(){

        if(stone==null && isopen == true)
        {
            setCamera.enabled=true;
            time += Time.deltaTime;

            if (time < 8f)
            {
                player.SetActive(false);
                transform.rotation = Quaternion.Euler(maxRotation * Mathf.Sin(Time.time * 0.5f), 180f, 0f);
            }else{
                player.SetActive(true);
                setCamera.enabled=false;
                time=0;
                state=State.isBoss;
            }
        }
    }

    void Boss(){

        transform.position=new Vector3(451f,28f,435f);
        transform.rotation = Quaternion.Euler(-5f,220f,0f);

        if(stoneTower==null && time<12){
            setCamera.enabled=true;
            time += Time.deltaTime;
            player.SetActive(false);

            
            //transform.eulerAngles = new Vector3(-5f,220f,0f);
        }
        else if(time>12f){
                player.SetActive(true);
                setCamera.enabled=false;
            }
    }
}
