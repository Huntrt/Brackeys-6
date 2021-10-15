using System;
using UnityEngine;
using UnityEngine.Events; 
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
	///1. Player attack get the "Player Stat" of player once
	///2. Update "PlayerStat" stats to attack when ever got create (OnEnable)
	///3. Power will get "Player Stat" from this attack once and it will update 
	///4. Power will then use the power stats that got from this attack "Player Stat"
	///Thank to pooling we dont need to get stat from player each attack since
	///the "Player Stat" are component it will alway update and attack will not update
	///"Player Stat" from player when create since it only get once when create

	public Rigidbody2D rb;
	public Player player;
	public CombatStats combat = new CombatStats();
	public PiercingStats piercing = new PiercingStats();
	public ExplosionStats explosion = new ExplosionStats();
	public HomingStats homing = new HomingStats();
	public RicochetStats ricochet = new RicochetStats();
	public TrailRenderer trail;
	//Create an event that send enemy got hit and it position
	[Serializable] public class Hitting : UnityEvent<GameObject, Vector3>{} public Hitting onHit;

    void Awake()
    {
		//Get the player ins
		player = Player.ins;
    }

	void OnEnable()
	{
		//Make the chaser to be trial parent
		trail.transform.parent = transform;
		//Reset the trail position
		trail.transform.localPosition = Vector2.zero;
		//Clear trail
		trail.Clear();
		//Clear the object got hit
		hitted.Clear(); hitted = new List<GameObject>();
		//Update stats when create
		///Set the combat stats
		combat.damage = player.combat.damage;
		combat.velocity = player.combat.velocity;
		combat.range = player.combat.range;
		///Set the component feature base on use bool or make the component deactive itself
		///Set the explosion stats
		explosion.use = player.explosion.use;
		explosion.scaleDamage = player.explosion.scaleDamage;
		explosion.size = player.explosion.size;
		///Set the homing stats
		homing.use = player.homing.use;
		homing.accuracy = player.homing.accuracy;
		homing.release = player.homing.release;
		///Set the ricochet stats
		ricochet.use = player.ricochet.use;
		ricochet.range = player.ricochet.range;
		///Set the piercing stats
		piercing.amount = player.piercing.amount;
	}

    // Update is called once per frame
    void Update()
    {
		//Begin casthitbox
		CastHitBox();
		//Update the bullet velocity as the speed stats at transfrom right
		rb.velocity = transform.right * combat.velocity;
    }

	//Deactive when out of range
	void FixedUpdate() {if(combat.range <= 0) {gameObject.SetActive(false);}}

	//An list of bullet has been hit and begin casting hitbox
	[SerializeField] List<GameObject> hitted = new List<GameObject>(); void CastHitBox()
	{
		//Predict the future postion using rigidbody velocity
		Vector2 futurePos = (Vector2)transform.position + rb.velocity * Time.deltaTime;
		//Get the distance gonna travel next frame
		float distance = Vector2.Distance(transform.position, futurePos);
		//Decrease the range with travelled distance
		combat.range -= distance;
		//Casting an hitbox at this projectile position and hitbox radius as the projectile height
		RaycastHit2D[] hitbox = Physics2D.CircleCastAll(transform.position, (transform.localScale.x/5)
		//Facing hit box forward from projectile and get the hitbox length base on predicted position
		, transform.up, Mathf.Clamp(distance, distance, combat.range));
		//If hitbox hit something
		if(hitbox.Length > 0)
		{
			//For each of the object got hit by hitbox
			for (int obj = hitbox.Length-1; obj >= 0; obj--)
			{
				//Get the object got hit
				GameObject hit = hitbox[obj].transform.gameObject;
                //If hit object are an new one
				if(!hitted.Contains(hit))
				{
					//If the hit object tag are enemy
					if(hit.CompareTag("Enemy"))
					{
						//Call damaging function with hit object, position and stats
						onHit.Invoke(hit,hitbox[obj].point);
						//Hurting the enemy got hit
						hit.gameObject.GetComponent<Heath>().Hurt(combat.damage);
						//Has hit the object
						hitted.Add(hit);
					}
				}
			}
			//Deactive when out of range
			if(combat.range <= 0) {gameObject.SetActive(false);}
		}
	}

	private void OnTriggerStay2D(Collider2D other) 
	{
		//If the object got trigger tag are enemy and it haven't been hit
		if(other.CompareTag("Enemy") && !hitted.Contains(other.gameObject))
		{
			//Get the object got trigger
			GameObject hit = other.gameObject;
			//Call on damage function with hit object, position and stats
			onHit.Invoke(hit,other.bounds.ClosestPoint(transform.position));
			//Hurting the enemy got hit
			hit.gameObject.GetComponent<Heath>().Hurt(combat.damage);
			//Has hit the object
			hitted.Add(hit);
		}
	}
}
