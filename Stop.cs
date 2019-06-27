using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : MonoBehaviour
{
    Enemycontrol enemy;
    Door door;
    
    // Start is called before the first frame update
    void Start()
    {
        door=GameObject.Find("doors01_model").GetComponent<Door>();
        enemy=GetComponent<Enemycontrol>();
        enemy.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(door.open==true){
            enemy.enabled=true;
        }
    }
}
