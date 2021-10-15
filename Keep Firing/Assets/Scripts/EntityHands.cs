using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHands : MonoBehaviour
{
	enum TargetMode {player, mouse, custom} 
	[SerializeField] TargetMode mode;
	[Tooltip("Use this object as target if using custom mode")]
	[SerializeField] Vector2 targetPos;
	[SerializeField] Transform target = null;
	[SerializeField] Transform anchor, firepoint;
	[SerializeField] float offSet;
	[SerializeField] SpriteRenderer sprite;

    void Start()
    {
		//Get the player transfrom if using player mode
        if(mode == TargetMode.player) {target = Player.ins.transform;}
    }

    void Update()
    {
		//Get the mouse position if needed
		MousePos();
		//Get the target psotion if not using mouse mode
		if(mode != TargetMode.mouse) {targetPos = target.position;}
		//Make the anchor's green axis look at target position
     	anchor.right = targetPos - (Vector2)anchor.position;
		//If the target are behind the weapon a -0
		if(targetPos.x < sprite.transform.position.x)
		{
			//Set the firepoint local X axis using opposite of offset
			firepoint.localPosition = new Vector2(firepoint.localPosition.x,offSet);
			//Flipping the weapon
			sprite.flipY = true;
		}
		//If the target are infront the weapon 0- a
		else
		{
			//Set the firepoint local X axis using offset
			firepoint.localPosition = new Vector2(firepoint.localPosition.x,-offSet);
			//Unflip the weapon
			sprite.flipY = false;
		}
    }

	void MousePos() //Get the mouse position if using mouse mode
	{if(mode == TargetMode.mouse) {targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);}}
}