using UnityEngine;
using System.Collections;
using System;
//using Windows.Kinect;


public class JointOverlayer : MonoBehaviour 
{
//	[Tooltip("GUI-texture used to display the color camera feed on the scene background.")]
//	public GUITexture backgroundImage;

	[Tooltip("Camera that will be used to overlay the 3D-objects over the background.")]
	public Camera foregroundCamera;
	
	[Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
	public int playerIndex = 0;
	
	[Tooltip("Kinect joint that is going to be overlayed.")]
	public KinectInterop.JointType trackedJoint = KinectInterop.JointType.HandRight;

	[Tooltip("Game object used to overlay the joint.")]
	public Transform overlayObject;

	[Tooltip("Smoothing factor used for joint rotation.")]
	public float smoothFactor = 10f;

    private KinectInterop.JointType spineMidJoint = KinectInterop.JointType.SpineBase;
    public KinectInterop.JointType handRightJoint = KinectInterop.JointType.HandRight;
    public KinectInterop.JointType handLeftJoint = KinectInterop.JointType.HandLeft;

    //public UnityEngine.UI.Text debugText;

    [NonSerialized]
	public Quaternion initialRotation = Quaternion.identity;
	private bool objFlipped = false;

    private UIElement uiElement;
    private SceneControllet_RunnerScene runnerSceneCtrl;
    private ShipController shipController;

    private Vector3 smPosJointOld = new Vector3(0, 0, 0);
    private Vector3 hrPosJointOld = new Vector3(0, 0, 0);
    private Vector3 hlPosJointOld = new Vector3(0, 0, 0);

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


    public void Start()
	{
		if (!foregroundCamera) 
		{
			// by default - the main camera
			foregroundCamera = Camera.main;
		}

		if(overlayObject)
		{
			// always mirrored
			initialRotation = overlayObject.rotation; // Quaternion.Euler(new Vector3(0f, 180f, 0f));

			Vector3 vForward = foregroundCamera ? foregroundCamera.transform.forward : Vector3.forward;
			objFlipped = (Vector3.Dot(overlayObject.forward, vForward) < 0);

			overlayObject.rotation = Quaternion.identity;
		}
	}
	
	void Update () 
	{
		KinectManager manager = KinectManager.Instance;
		
		if(manager && manager.IsInitialized() && foregroundCamera)
		{
//			//backgroundImage.renderer.material.mainTexture = manager.GetUsersClrTex();
//			if(backgroundImage && (backgroundImage.texture == null))
//			{
//				backgroundImage.texture = manager.GetUsersClrTex();
//			}
			
			// get the background rectangle (use the portrait background, if available)
			Rect backgroundRect = foregroundCamera.pixelRect;
			PortraitBackground portraitBack = PortraitBackground.Instance;
			
			if(portraitBack && portraitBack.enabled)
			{
				backgroundRect = portraitBack.GetBackgroundRect();
			}

			// overlay the joint
			long userId = manager.GetUserIdByIndex(playerIndex);
			
			int smJointIndex = (int)spineMidJoint;
            int hrJointIndex = (int)handRightJoint;
            int hlJointIndex = (int)handLeftJoint;
            if (manager.IsJointTracked (userId, smJointIndex)) 
			{
				Vector3 smPosJoint = manager.GetJointPosColorOverlay(userId, smJointIndex, foregroundCamera, backgroundRect);
                Vector3 hrPosJoint = manager.GetJointPosColorOverlay(userId, hrJointIndex, foregroundCamera, backgroundRect);
                Vector3 hlPosJoint = manager.GetJointPosColorOverlay(userId, hlJointIndex, foregroundCamera, backgroundRect);

                if (smPosJoint != Vector3.zero && hrPosJoint != Vector3.zero) 
				{
                    if (!start)
                    {
                        reactivateRunnerCursor(true);
                        startRunnerCursor(smPosJoint);
                    }
                    else {

                        calcDelta(smPosJoint, smPosJointOld);
                        if(calcDeltaHand(hrPosJoint, hrPosJointOld))
                        {
                            shipController.setRotNor_hand(true);
                        }
                        if (calcDeltaHand(hlPosJoint, hlPosJointOld))
                        {
                            shipController.attack();
                        }
                        smPosJointOld = smPosJoint;
                        hrPosJointOld = hrPosJoint;
                        hlPosJointOld = hlPosJoint;
                    }


//					if(debugText)
//					{
//						debugText.text = string.Format("{0} - {1}", trackedJoint, posJoint);
//					}
                    /*
					if (overlayObject) 
					{
						overlayObject.position = posJoint;

						Quaternion rotJoint = manager.GetJointOrientation (userId, iJointIndex, !objFlipped);
						rotJoint = initialRotation * rotJoint;

						overlayObject.rotation = Quaternion.Slerp (overlayObject.rotation, rotJoint, smoothFactor * Time.deltaTime);
					}
                    */
				}
			} 
			else 
			{
				// make the overlay object invisible
				if (overlayObject && overlayObject.position.z > 0f) 
				{
					Vector3 posJoint = overlayObject.position;
					posJoint.z = -10f;
					overlayObject.position = posJoint;
				}
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


    private void startRunnerCursor(Vector3 posHandRight)
    {
        //Debug.Log(posHandRight + " PHR");
        Vector2 posHand = new Vector2(posHandRight.x * 500, 0);
        //Debug.Log(posHand + " PH");
        uiElement.setPosRunnerCursor(posHand, 0);
        //float dir = -hand.Direction.x*Mathf.Rad2Deg;
        //uiElement.setRotRunnerCursor(dir, 0);
        if (Mathf.Abs(posHand.x) < 50 /* && Mathf.Abs(posHand.y) < 50*/)
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
    private void calcDelta(Vector3 curPos, Vector3 oldPos)
    {

        float deltaX = oldPos.x - curPos.x;

        //float deltaY = curPos.y - oldPos.y;
        shipController.setDeltaPos(deltaX * 0.4f, 0);

    }

    private bool calcDeltaHand(Vector3 curPos, Vector3 oldPos)
    {

        float deltaHand =  Mathf.Abs(curPos.y - oldPos.y);
        if (deltaHand > 8.0f)
        {
            return true;
        }
        return false;
    }

}
