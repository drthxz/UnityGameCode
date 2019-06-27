using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    GameObject door;
    private Vector3 pos;
    public bool open;
    // Start is called before the first frame update
    void Start()
    {
        door=GameObject.Find("doors02_poly");
        pos = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(door!=null && open==true){
            door.transform.localPosition = new Vector3(Mathf.Sin(Time.time*2) * 0.005f +pos.x, pos.y-Time.time*0.015f , pos.z);
            Destroy (door);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            open=true;
        }
    }
}
