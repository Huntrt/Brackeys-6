using UnityEngine;

public class Supporter : MonoBehaviour
{
	[Tooltip("How far will the enemy gonna dodge player")]
	public float repel;
	public Transform allies;
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
		//Get the player position
		Vector2 playerPos = Player.ins.transform.position;
		//Vector for movement
		Vector2 movement = Vector2.zero;
		//Vector for repeling
		Vector2 repeling = Vector2.zero;
		//Calculated modify range by get 85% of range stats
		float modifyRanged = 0.7f * combat.range;
		//If has allies that active in hierarchy
		if(allies != null && allies.gameObject.activeInHierarchy)
		{
			//Get the direction to allies
			Vector3 direction = (allies.position - transform.position).normalized;
			//If the supporter got too close to player 
			if(Vector2.Distance(transform.position,playerPos) < repel)
			{
				//Repel away from all the player if it get too close
				repeling += (rb.position - playerPos).normalized;
			}
			//Get the distance between allies position and support
			float alliesDistance = Vector2.Distance(allies.position, transform.position);
			//Approach if the allies haven't got in range
			if(alliesDistance > modifyRanged)
			{
				//Moving toward allies into range using direction
				movement = transform.position + direction * Time.fixedDeltaTime * entity.speed;
			}
			//Stop when in range
			else {movement = rb.position;}
			//Update movement with repeling force
			movement += repeling * Time.deltaTime * entity.speed;
			//Moving rigidbody position
			rb.MovePosition(movement);
		}
    }
}
