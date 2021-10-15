using UnityEngine;

public class Chaser : MonoBehaviour
{
	[SerializeField] AudioClip explodeSound;
	[SerializeField] GameObject explodeEffect;
	[SerializeField] TrailRenderer trail;

	void Start()
	{
		//Setting up the hunting movement
		HuntingMovement();
	}

	void OnEnable() 
	{
		//Start self explode count down
		StartCoroutine("SelfExplode");
		//Make the chaser to be trial parent
		trail.transform.parent = transform;
		//Reset the trail position and off set it
		trail.transform.localPosition = new Vector2(0,-0.623f);
		//Clear trail
		trail.Clear();
	}

	System.Collections.IEnumerator SelfExplode()
	{
		//Wait for the velocity counter to end
		yield return new WaitForSeconds(combat.velocity);
		//Explode cause of out time
		Explode();
	}

	private void OnCollisionEnter2D(Collision2D other) 
	{
		//When collide with player
		if(other.gameObject.CompareTag("Player")) 
		{
			//Explode cause hit player
			Explode();
		}
	}

	void Explode()
	{
		//Play the level up explode sound
		GameManager.ins.soundSource.PlayOneShot(explodeSound);
		//Create and get the explode effect from pooler
		Transform exp = Pool.get.Object(explodeEffect, transform.position, Quaternion.identity).transform;
		//Set the explode effect scale using the range
		explodeEffect.transform.localScale = new Vector2(combat.range,combat.range);
		//Create an circle cast all at the chaser with size of range
		Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, combat.range);
		//Deal x2 damage to the player if collider hit object with player tag
		foreach (Collider2D hit in col){if(hit.CompareTag("Player")) {Player.ins.heath.Hurt(combat.damage);}}
		//And die after explode
		gameObject.SetActive(false);
		//Detech children of chaser
		trail.transform.parent = null;
	}
	
	Seeking seeking;
	public EntityStats entity = new EntityStats();
	public CombatStats combat = new CombatStats();

	void HuntingMovement()
	{
		//Get the component hunting movement
		seeking = GetComponent<Seeking>();
		//Send the enity stats to hunting movement
		seeking.entity = entity;
	}
}