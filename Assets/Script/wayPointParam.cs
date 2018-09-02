using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayPointParam : MonoBehaviour {
    public Vector3 posWPoint;
    private Vector3 rotatePoint;
    private bool direction = true;
    public GameObject Giz;
    public int index = 0;

    public void calcRotatetPoint(Transform wPoint, float angle, float radiusWP, bool dir)
    {
        direction = dir;

        angle *= Mathf.Rad2Deg;
        angle = Mathf.Abs(angle / 2);
        angle *= Mathf.Deg2Rad;
        float gipLen = radiusWP / Mathf.Sin(angle);
        float catLen = gipLen * Mathf.Cos(angle);
        rotatePoint = wPoint.right.normalized * catLen;

        Debug.Log("b " + dir);
        rotatePoint = !dir ? rotatePoint : rotatePoint*-1;
        rotatePoint = new Vector3(wPoint.position.x + rotatePoint.x, 0, wPoint.position.z + rotatePoint.z);
        //Instantiate(Giz, rotatePoint, Quaternion.identity);

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Ship")
        {
            ShipMove shipMove = collider.gameObject.GetComponent<ShipMove>();
            if (gameObject.tag == "Inner" && shipMove.indexWP == index-1)
            {
                shipMove.startRotate(rotatePoint, direction);
            } else if(gameObject.tag == "Outer" && shipMove.indexWP == index - 1)
            {
                shipMove.setRotNorm(true);
                shipMove.indexWP++;
                shipMove.stopRotate(transform.forward);
            } else if(gameObject.tag == "Finish")
            {
                shipMove.finish = true;
                GameObject.Find("SceneController").GetComponent<SceneControllet_RunnerScene>().finish();
            }
            else if (gameObject.tag == "Warning")
            {
                shipMove.setRotNorm(false);
            }
        }
    }
}
