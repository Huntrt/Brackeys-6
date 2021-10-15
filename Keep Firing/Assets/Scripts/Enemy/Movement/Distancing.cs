using UnityEngine;

public class Distancing : MonoBehaviour
{
	[Tooltip("The closer to 0% the more closer to player \n Enemy<-distance%-->Player")] 
	[Range(0,100)] [SerializeField] float distance;
	Rigidbody2D rb;
	public EntityStats entity;
	public CombatStats combat;

	void Start()
	{
		//Get the rigidbody component
		rb = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
	{
		//Get the distance between player position and distancer
		float playerDistance = Vector2.Distance(Player.ins.transform.position, transform.position);
		//Save the player position
		Vector2 playerPos = Player.ins.transform.position;
		//Vector for direction to player
		Vector3 direction = (Player.ins.transform.position - transform.position).normalized;
		//Vector for movement
		Vector2 movement = Vector2.zero;
		//Calculated modify range by get 85% of range stats
		float modifyRanged = (85f/100f) * combat.range;
		//How close until distancer began to ran away
		float runningDistance = (distance/100f) * modifyRanged;
		//If the player got too close to distancer 
		if(playerDistance < runningDistance)
		{
			//Keep distance away from player as opposite direction
			movement = transform.position - direction * Time.fixedDeltaTime * entity.speed;
		}
		//Approach if the distancer haven't got in range while not running
		else if(playerDistance > modifyRanged)
		{
			//Moving toward player into range using direction
			movement = transform.position + direction * Time.fixedDeltaTime * entity.speed;
		}
		//Stop moving if has reach safe spot
		else {movement = rb.position;}
		//Moving rigidbody position
		rb.MovePosition(movement);
	}
}
