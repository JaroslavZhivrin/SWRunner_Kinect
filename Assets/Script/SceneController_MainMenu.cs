using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController_MainMenu : MonoBehaviour {

    private UIElement uiElement;

    void Awake()
    {
        uiElement = GetComponent<UIElement>();
    }

    // Use this for initialization
    void Start () {
        uiElement.hsInit();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
