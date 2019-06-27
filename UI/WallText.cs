using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallText : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Canvas/WallText").GetComponent<Text>();
        text.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            text.enabled = true;
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