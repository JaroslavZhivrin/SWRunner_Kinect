using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CamMove : MonoBehaviour {
    [SerializeField]
    private Vector3 camTarget = new Vector3(0, 3.64f, -1);
    [SerializeField]
    private Vector3 camStart = new Vector3(0, 30.64f, -1);

    [SerializeField]
    private Vector3 camRotTarget = new Vector3(16.88f, 0, 0);
    [SerializeField]
    private Vector3 camRotStart = new Vector3(90f, 0, 0);
    [SerializeField]
    private GameObject startText;

    [SerializeField]
    private float step = 0.1f;
    private float stepRot = 0.1f;
    public bool start = false;
    // Use this for initialization
    void Start () {
        transform.localPosition = camStart;
        transform.localRotation = Quaternion.Euler(camRotStart);
        stepRot = (float)Mathf.Abs(camRotStart.x - camRotTarget.x) / (float)(Mathf.Abs(camStart.y - camTarget.y) / step);
        Debug.Log(stepRot);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (start)
        {
            if (camStart.y >= camTarget.y)
            {
                camStart.y -= step;
                camRotStart.x -= stepRot;
                transform.localPosition = camStart;
                transform.localRotation = Quaternion.Euler(camRotStart);
            } else if (!startText.activeSelf)
            {
                startText.SetActive(true);
            } else if (startText.activeSelf)
            {
                startText.transform.position = new Vector3(startText.transform.position.x, startText.transform.position.y+2, startText.transform.position.z);
            }
        }

    }
}
