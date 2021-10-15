using UnityEngine; using System;

public class Ranger : MonoBehaviour
{
	[SerializeField] AudioClip attackSound;
	[SerializeField] Transform weapon, atkPoint;
	float load;
	public GameObject attack;

    void Start()
    {
		//Setting up the distancing movement
		DistancingSetting();
    }

	void Update()
	{
		//Get the distance between player position and enemy
		float playerDistance = Vector2.Distance(Player.ins.transform.position, transform.position);
		//If player are now in range
		if(playerDistance <= combat.range)
		{
			//If load are lower than attack speed
			if(load < entity.attackSpeed)
			{
				//Begin loading
				load += Time.deltaTime;
			}
			//If load has reach attack speed
			else
			{
				//Play the attack sound
				GameManager.ins.soundSource.PlayOneShot(attackSound);
				//Create attack
				GameObject atk = Pool.get.Object(attack, atkPoint.position, atkPoint.rotation, true);
				//Get the stats from attack just create
				EnemyBullet bullet = atk.GetComponent<EnemyBullet>();
				///Set the core stats
				bullet._combat.damage = combat.damage;
				bullet._combat.velocity = combat.velocity;
				bullet._combat.range = combat.range;
				//Reset loading
				load -= load;
			}
		}
	}


	[SerializeField] Distancing distancing;
	public EntityStats entity = new EntityStats();
	public CombatStats combat = new CombatStats();

	void DistancingSetting()
	{
		//Send the entity stats to distancing movement
		distancing.entity = entity;
		//Send the combat stats to distancing movement
		distancing.combat = combat;
	}
}
