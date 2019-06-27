using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Use : MonoBehaviour
{
    // Start is called before the first frame update
    bool setItem;
    public Text text;

    public GameObject bomb;

    public GameObject tower;

    bool isbomb;
    
    void Start()
    {
        tower = GameObject.Find("stoneTower01_poly");

        text = GameObject.Find("Canvas/BombText").GetComponent<Text>();
        text.enabled = false;
        bomb.transform.localScale = new Vector3(3f, 3f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (tower == null)
        {
            text.text = "";
        }

        if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            if(setItem==true){
                Score.boom-= 1;
                Instantiate(bomb, transform.position,Quaternion.Euler(-90f,0f,0f));
                Destroy(gameObject);
                isbomb =true;
                setItem = false;
                text.text = ""; 
            }
                
        }
       
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
         {
            text.text = "通常モードでXボタンを押すと爆弾を設置できる。"; 
            if (Score.boom!=0){
                text.enabled = true;
                setItem =true;
            }
               
        }

        if(other.transform.tag=="fire" && isbomb ==true){
            Destroy(gameObject,3f);
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            text.enabled = false;

        }
    }


}
