using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour {
    private UIElement uiElement;
    private SceneControllet_RunnerScene runnerSceneCtrl;
    private ShipController shipController;

    private bool start = false;
    //private bool start = true;
    // Use this for initialization
    void Awake()
    {
        //hands[0] = KinectWrapper.NuiSkeletonPositionIndex.HandLeft;
        // hands[1] = KinectWrapper.NuiSkeletonPositionIndex.HandRight;
        shipController = GetComponent<ShipController>();
        runnerSceneCtrl = GetComponent<SceneControllet_RunnerScene>();
        uiElement = GetComponent<UIElement>();

    }

	
	// Update is called once per frame
	void Update () {
        if (!start)
        {
            reactivateRunnerCursor(true);
            startRunnerCursor();
        }
        else
        {
            calcDelta();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                shipController.setRotNor_hand(true);
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                shipController.attack();
            }
        }
    }

    private void reactivateRunnerCursor(bool flag)
    {
        uiElement.reactivateRunnerCursor(flag, 0);
        if (!flag)
        {
            uiElement.setPosRunnerCursor(new Vector2(0, -1000), 0);
        }
    }

    private void startRunnerCursor()
    {
        //Debug.Log(posHandRight + " PHR");
        Vector2 posHand = Input.mousePosition;
        posHand.x -= Screen.width / 2;
        posHand.y -= Screen.height / 2;
        //Debug.Log(posHand + " PH");
        uiElement.setPosRunnerCursor(posHand, 0);
        //float dir = -hand.Direction.x*Mathf.Rad2Deg;
        //uiElement.setRotRunnerCursor(dir, 0);
        if (Mathf.Abs(posHand.x) < 50/* && Mathf.Abs(posHand.y) < 50*/)
        {
            runnerSceneCtrl.timerStart_Counter();
        }
        else
        {
            runnerSceneCtrl.timerStart_Restart();
        }
    }

    //Триггер переключения управления
    public void startRunner()
    {
        start = true;
    }

    //РАссчет смещения корабля
    private void calcDelta()
    {
            float deltaX = Input.GetAxis("Horizontal");
            deltaX = deltaX != 0 ? deltaX / Mathf.Abs(deltaX) : 0;
            //float deltaY = curPos.y - oldPos.y;
            shipController.setDeltaPos(deltaX, 0);

    }
}
