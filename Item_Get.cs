using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Get : MonoBehaviour
{
    bool isBoom;

    public Text text;

    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Canvas/BombText").GetComponent<Text>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null)
        {
            if(Score.attackMode==false){
                
                if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Joystick1Button2))
                {
                    if (isBoom == true)
                    {
                        Score.boom += 1;
                        text.text = "";
                        Destroy(gameObject);
                    }
                }
            }
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
         {
            
            text.enabled = true;
            isBoom = true;
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
