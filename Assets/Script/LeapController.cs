using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Leap;

public class LeapController : MonoBehaviour {
  // private Controller controller;
  // [SerializeField]
  // private float koefScaleVector = 0.1f;
  //
  // private UIElement uiElement;
  // private SceneControllet_RunnerScene runnerSceneCtrl;
  // private ShipController shipController;
  //
  // private float koefSize = 1;
  //
  // private bool start = false;
  //
  // // Use this for initialization
  // void Start () {
  //
  //     runnerSceneCtrl = GetComponent<SceneControllet_RunnerScene>();
  //     shipController = GetComponent<ShipController>();
  //     uiElement = GetComponent<UIElement>();
  //     controller = new Controller();
  // }
  //
  // // Update is called once per frame
  // void Update()
  // {
  //     if (controller.IsConnected)
  //     {
  //         Frame frame = controller.Frame();
  //         if (frame.Hands.Count > 0)
  //         {
  //             Hand hand = frame.Hands[0];
  //             if (hand.Fingers.Count >= 3)
  //             {
  //                 if (!start)
  //                 {
  //                     calcKoefSize(hand);
  //                     reactivateRunnerCursor(true);
  //                     startRunnerCursor(hand);
  //                 }
  //                 else
  //                 {
  //
  //                     if (detectClick(hand.PalmPosition, hand.Fingers[1].TipPosition, 75))
  //                     {
  //                         shipController.attack();
  //                     }
  //                     if (detectClick(hand.PalmPosition, hand.Fingers[2].TipPosition, 75))
  //                     {
  //                         shipController.setRotNor_hand(true);
  //                     }
  //                     shipController.editSpeedTarget(getDistance(hand.Fingers[0].TipPosition, hand.Fingers[hand.Fingers.Count-1].TipPosition));
  //
  //                      calcDelta(hand);
  //                 }
  //             }
  //         } else
  //         {
  //             reactivateRunnerCursor(false);
  //             runnerSceneCtrl.timerStart_Restart();
  //         }
  //     }
  // }
  //
  // private void startRunnerCursor(Hand hand)
  // {
  //     Vector2 posHand = new Vector2(hand.PalmPosition.x* koefScaleVector, -hand.PalmPosition.z* koefScaleVector);
  //     uiElement.setPosRunnerCursor(posHand, 0);
  //     float dir = -hand.Direction.x*Mathf.Rad2Deg;
  //     uiElement.setRotRunnerCursor(dir, 0);
  //     if(Mathf.Abs(dir) < 10 && Mathf.Abs(posHand.x) < 15 && Mathf.Abs(posHand.y) < 15)
  //     {
  //         runnerSceneCtrl.timerStart_Counter();
  //     } else
  //     {
  //         runnerSceneCtrl.timerStart_Restart();
  //     }
  // }
  //
  // private void reactivateRunnerCursor(bool flag)
  // {
  //     uiElement.reactivateRunnerCursor(flag, 0);
  // }
  //
  // private void calcKoefSize(Hand hand)
  // {
  //     Finger finger = hand.Fingers[1];
  //     Vector3 handPos = new Vector3(hand.PalmPosition.x, hand.PalmPosition.y, hand.PalmPosition.z);
  //     Vector3 fingerPos = new Vector3(finger.TipPosition.x, finger.TipPosition.y, finger.TipPosition.z);
  //     koefSize = 100 / Vector3.Distance(handPos, fingerPos);
  //
  // }
  //
  // public void startRunner()
  // {
  //     start = true;
  // }
  //
  // private void OnDestroy()
  // {
  //     //controller.StopConnection();
  // }
  //
  // private void calcDelta(Hand hand)
  // {
  //     Frame frame = controller.Frame(1);
  //     if (frame.Hands.Count > 0)
  //     {
  //         Hand hand2 = frame.Hands[0];
  //         float deltaX = hand.PalmPosition.x - hand2.PalmPosition.x;
  //         float deltaY = hand.PalmPosition.y - hand2.PalmPosition.y;
  //         shipController.setDeltaPos(deltaX/10, deltaY/10);
  //         
  //     }
  // }
  //
  // private bool detectClick(Vector hand, Vector finger, float threshold)
  // {
  //     return (getDistance(hand, finger) < threshold);
  // }
  //
  // private float getDistance(Vector hand, Vector finger)
  // {
  //     Vector3 handPos = new Vector3(hand.x, hand.y, hand.z);
  //     Vector3 fingerPos = new Vector3(finger.x, finger.y, finger.z);
  //     Debug.Log(Vector3.Distance(handPos, fingerPos) * koefSize);
  //     return Vector3.Distance(handPos, fingerPos) * koefSize;
  // }
}
