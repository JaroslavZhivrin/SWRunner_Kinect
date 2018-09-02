using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMove : MonoBehaviour {
    public bool start = false;
    [SerializeField]
    private bool easy = false;
    private bool moveFlagForEasyX = false;
    private bool moveFlagForEasyY = false;
    private float targetPosX = 0;
    private float currentPosX = 0;
    private float targetPosY = 0;
    private float currentPosY = 0;
    [SerializeField]
    public float speed = 1f;
    [SerializeField]
    public GameObject warning;
   // [SerializeField]
    private float speedTarget = 5f;
    [SerializeField]
    private float speedMax = 100f;
    [SerializeField]
    private float speedMin = 1f;
    [SerializeField]
    private float accelerate = 0.2f;
    [SerializeField]
    private float speedXY = 10f;
    [SerializeField]
    private float widthTrack = 4;
    [SerializeField]
    private float speedRot = 0.5f;
    [SerializeField]
    private float speedRotMin = 0.075f;
    private bool flagRotate = false;
    private int dirRotate = 1;
    public int indexWP = 0;
    private Vector3 axisRotate = new Vector3(0, 0, 0);
    private CharacterController charConroller;
    public bool finish = false;
    public bool normalRotate = true;
    private float curDeltaY = 0;
    private float curDeltaX = 0;
    private float koefSizeHand = 1;
    private float shiftSizeHand = 1;
    [SerializeField]
    private Animator animator;
    private void Awake()
    {
        easy = RunnerInfo.easy;
        koefSizeHand = (speedMax-speedMin) / (170-90);
        shiftSizeHand = speedMax - 170 * koefSizeHand;
    }

    // Use this for initialization
    void Start()
    {
        if (easy)
        {
            widthTrack = widthTrack / 3;
        } else
        {
            widthTrack = (widthTrack / 2)*0.9f;
        }
        charConroller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!finish && start)
        {
            //editSpeedTarget();
            speedAccelerate();

            if (!flagRotate)
            {
                
                float deltaZ = speed * 0.02f;
                Vector3 movement = new Vector3(0, 0, deltaZ);
                movement = transform.TransformDirection(movement);
                charConroller.Move(movement);
            }
            else
            {
                if (normalRotate)
                    transform.RotateAround(axisRotate, Vector3.up, speedRot * dirRotate);
                else
                {
                    transform.RotateAround(axisRotate, Vector3.up, speedRotMin * dirRotate);
                    //speedCrash();
                }
            }
        }

    }

    public void startRotate(Vector3 axis, bool dir)
    {

        dirRotate = !dir? 1 : -1;
        flagRotate = true;
        axisRotate = axis;
    }

    public void stopRotate(Vector3 edit)
    {
        transform.LookAt(new Vector3(transform.position.x + edit.x, transform.position.y, transform.position.z + edit.z));
        flagRotate = false;

        animator.SetBool("fast", false);

        speedTarget = 5;
    }

    private float notEasyMove(float delta, ref float target)
    {
        float tg = target;
        target += delta;
        if(target < -widthTrack)
        {
            delta = -(widthTrack + tg);
            target = -widthTrack;
        } else if (target > widthTrack)
        {
            delta = widthTrack+tg*-1;
            target = widthTrack;
        }
        return delta;
    }

    private float easyMove(ref float target, ref float current, ref bool flag, float axis, ref float curDelta)
    {

       
        float delta = 0;
        if (!flag && axis != 0)
        {
            if (target > -widthTrack && axis < 0)
            {
                animator.SetBool("left", true);
                target -= widthTrack;
                flag = true;

            }
            else if (target < widthTrack && axis > 0)
            {
                animator.SetBool("right", true);
                target += widthTrack;
                flag = true;
            }
        }
        else if (flag)
        {
            if (Mathf.Abs(target - current) < speedXY * 0.005f)
            {
                delta = target - current;
                current = target;
                curDelta = 0;
                flag = false;
                animator.SetBool("left", false);
                animator.SetBool("right", false);
            }
            else
            {
                delta = speedXY * 0.005f * ((target - current) / Mathf.Abs(target - current));
                current += delta;
            }
        }
    return delta;
    }

    private void speedAccelerate()
    {
        
        if ((speedTarget-speed)*accelerate < 0)
        {
            speed = speedTarget;
        }
        else
        {
            speed += accelerate;
        }
    }

    public void editSpeedTarget(float size)
    {
        speedTarget = size * koefSizeHand + shiftSizeHand;
        //if (Input.GetKey(KeyCode.Q)) speedTarget -= 0.2f;
        //if (Input.GetKey(KeyCode.E)) speedTarget += 0.2f;
        speedTarget = Mathf.Clamp(speedTarget, speedMin, speedMax);
        if(speedTarget != speed)
            accelerate = Mathf.Abs(accelerate) * (speedTarget - speed) / Mathf.Abs(speedTarget - speed);
    }

    public void speedCrash()
    {
        animator.SetTrigger("crash");
        speed = speedMin;
    }

    public void setRotNorm(bool flag)
    {
       if(!normalRotate && flag)
        {
            animator.SetBool("fast", true);
            speedTarget = 10 ;
        }
        normalRotate = flag;
        warning.SetActive(!flag);
        
    }

    public void setRotNor_hand(bool flag)
    {
        
        if (!flagRotate)
        {
            setRotNorm(flag);
        }
    }

    public void startRunner()
    {
        start = true;
    }

    public void setDelta(float deltaXInner, float deltaYInner)
    {
        if (!flagRotate)
        {
            float deltaX = 0;
            float deltaY = 0;
            if (!easy)
            {
                deltaX = notEasyMove(deltaXInner, ref targetPosX);
                deltaY = notEasyMove(deltaYInner, ref targetPosY);
            }
            else
            {
                curDeltaY += deltaYInner;
                curDeltaX += deltaXInner;
                deltaX = Mathf.Abs(curDeltaX) > 2f ? 1 * (curDeltaX / Mathf.Abs(curDeltaX)) : 0;
                deltaY = Mathf.Abs(curDeltaY) > 2f ? 1 * (curDeltaY / Mathf.Abs(curDeltaY)) : 0;
                deltaX = easyMove(ref targetPosX, ref currentPosX, ref moveFlagForEasyX, deltaX, ref curDeltaX);
                deltaY = easyMove(ref targetPosY, ref currentPosY, ref moveFlagForEasyY, deltaY, ref curDeltaY);

                //deltaY = easyMove(deltaY, ref targetPosY, ref currentPosY, ref moveFlagForEasyY, "Vertical");
            }
            Vector3 movement = new Vector3(deltaX, deltaY, 0);
            movement = transform.TransformDirection(movement);
            charConroller.Move(movement);
        }
    }

}
