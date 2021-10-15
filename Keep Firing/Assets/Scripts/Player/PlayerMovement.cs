using UnityEngine; using UnityEngine.Experimental.U2D.IK; using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
	public Vector3 inputDirection;
	[SerializeField] bool cameraFollow;
	public Rigidbody2D Rigidbody;
	[SerializeField] Vector2 targetPos;
	[SerializeField] Transform anchor, firepoint;
	[Tooltip("-20 is recommend")] [SerializeField] float offSet;
	[SerializeField] SpriteRenderer sprite;
	
    void Update()
    {
		//Running function
		MoveInput();
	}

	Vector2 velocity; void MoveInput()
	{
		//Set the input horizontal and vertical direction
		inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0);
        //Make diagonal movement no longer faster than vertical, horizontal
        velocity = inputDirection.normalized;
        //Add the player speed to velocity
        velocity *= Player.ins.entity.speed;
	}

	void FixedUpdate()
	{
		//Moving the player
		Rigidbody.MovePosition(Rigidbody.position + velocity * Time.fixedDeltaTime);
	}

	void LateUpdate()
	{
		//If camera follow enable
		if(cameraFollow)
		{
			//Camera following player
			Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y,-10);
			//Get the mouse position if needed
			targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
	}
}