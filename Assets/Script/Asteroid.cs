using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    private int lives = 3; 

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Ship") {
            if (!collision.gameObject.GetComponentInChildren<forEventAnim>().isJump())
            {
                collision.gameObject.GetComponent<ShipMove>().speedCrash();
                Destroy(gameObject);
            }
        } 
    }

    public void attackAst()
    {
        Destroy(gameObject);
    }
}
