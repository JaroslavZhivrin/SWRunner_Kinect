using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward/1.5f;
    }

    void OnTriggerEnter(Collider collision)
    {
        //if (collision.gameObject.tag == "Ship")
        //{
        //  collision.gameObject.GetComponent<ShipMove>().speedCrash();
        // Destroy(gameObject);

        //} else
        if (collision.gameObject.tag == "Ast")
        {
            collision.gameObject.GetComponent<Asteroid>().attackAst();
            Destroy(gameObject);
        }
    }
}
