using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodamage : MonoBehaviour {

    float amount;
    float heal;

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "EnemyNOskill")
        {
            amount = 0.5f;
            other.GetComponent<EnemyNoskill>().TakeDamage(amount);

            //Destroy(gameObject);
        }
        if (other.gameObject.tag == "enemy")
        {
            amount = Mathf.Round(Random.Range(1f, 5f));
            other.GetComponent<Creature>().Hurt(amount);

            //Destroy(gameObject);
        }
    }

}
