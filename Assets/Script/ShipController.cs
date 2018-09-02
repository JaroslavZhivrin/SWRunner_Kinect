using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
    [SerializeField]
    private GameObject ship;
    private ShipMove shipMove;
    private ShipGun shipGun;

    private void Awake()
    {
        shipMove = ship.GetComponent<ShipMove>();
        shipGun = ship.GetComponent<ShipGun>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startRunner()
    {
        shipMove.startRunner();
    }

    public void setDeltaPos(float deltaX, float deltaY)
    {
        shipMove.setDelta(deltaX, deltaY);
    }

    public void attack()
    {
        shipGun.attack();
    }

    public void setRotNor_hand(bool flag)
    {
        shipMove.setRotNor_hand(flag);
    }

    public void editSpeedTarget(float size)
    {
        shipMove.editSpeedTarget(size);
    }
}
