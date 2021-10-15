using UnityEngine;

public class Frontliner : MonoBehaviour
{
    [Tooltip("The lower the value the closer frontliner go near the player")] 
	[SerializeField] float distance;
	Rigidbody2D rb;
	public EntityStats entity;

	void Start()
	{
		//Get the rigidbody component
		rb = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
	{
		//Get the distance between player position and frontliner
		float playerDistance = Vector2.Distance(Player.ins.transform.position, transform.position);
		//Vector for movement and direction of enemy to player
		Vector2 movement = Vector2.zero;
		//Vector for direction to player
		Vector3 direction = (Player.ins.transform.position - transform.position).normalized;
		//Approach if the frontliner haven't got in range
		if(playerDistance > distance)
		{
			//Moving toward player into range using direction
			movement = transform.position + direction * (entity.speed * Time.fixedDeltaTime);
		}
		//Stop moving if has reach safe spot
		else {movement = rb.position;}
		//Moving rigidbody position
		rb.MovePosition(movement);
	}
}
