using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGun : MonoBehaviour {

    [SerializeField]
    private UIElement uiElement;
    [SerializeField]
    GameObject Laser;
    private bool reload = false;
    private float timer = 2f;
    private float currentTime = 0f;
    private float fatigue = 0;
    [SerializeField]
    private Animator animator;
    void FixedUpdate()
    {
        if (fatigue > 0) {
            fatigue -= 0.1f;
            uiElement.reloadGun(fatigue / 100);
        } else if (fatigue < 0) { fatigue = 0; }
    }

   public void attack()
    {
       if (fatigue <= 70 && isAnim("Running"))
       {
          // Vector3 pos = transform.position /*+ transform.forward * 5*/;
          // Instantiate(Laser, pos, transform.rotation);
           fatigue += 30;
           animator.SetTrigger("jump");
           //animator.SetTrigger("attack");
       }
    }

    public bool isJump()
    {
        
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Jump");
    }

    public bool isAnim(string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

}
