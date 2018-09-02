using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forEventAnim : MonoBehaviour {
    private bool jumpFlag = false;
    
	// Use this for initialization
	public bool isJump()
    {
        return jumpFlag;
    }

    public void jump(int i)
    {
        
        jumpFlag = i == 1;
        Debug.Log(jumpFlag);
    }
}
