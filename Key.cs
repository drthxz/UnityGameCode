using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(30 * Time.deltaTime, 0, 0));
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
        StartCoroutine(Get());
        }
    }

    public IEnumerator Get()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        //image.enabled = true;
    }
}
