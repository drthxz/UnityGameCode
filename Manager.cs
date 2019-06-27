using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Manager : MonoBehaviour
{
    GameObject isboom;
    Text boomText;

    public GameObject villager;
    public GameObject enemy;
    public GameObject enemy_2;
    public GameObject scare;

    public GameObject rock;
    // Start is called before the first frame update
    void Start()
    {
        isboom = GameObject.Find("Image");
        boomText = GameObject.Find("Image_Text").GetComponent<Text>();
        isboom.SetActive(false);

        rock = GameObject.Find("Rock");

        enemy = GameObject.Find("EnemyGroup");
        enemy_2 = GameObject.Find("EnemyGroup_2");
        scare = GameObject.Find("ScareGroup");
        villager = GameObject.Find("VillagerGroup");

        //transform enemyCount =enemy.transform.Find("EnemyGroup");
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Score.boom != 0)
        {
            isboom.SetActive(true);
            boomText.text = "X" + Score.boom;
        }else{
            isboom.SetActive(false);
        }
        if(villager!=null && enemy!= null && enemy_2 != null && scare!=null){
            
            if(villager.transform.childCount==0 && enemy.transform.childCount==0 && enemy_2.transform.childCount == 0 && scare.transform.childCount == 0)
            {
            
                Destroy(rock);
                Destroy(villager);
                Destroy(enemy);
                Destroy(enemy_2);
                Destroy(scare);
            }
            
        }
    }
}
