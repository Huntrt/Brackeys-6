using UnityEngine;

public class Seeking : MonoBehaviour
{
	[SerializeField] float repel;
	public EntityStats entity;
	Rigidbody2D rb;
	Transform player;
	
    void Start()
    {
		//Get the player transfrom
        player = Player.ins.transform;
		//Get the player rigidbody
		rb = GetComponent<Rigidbody2D>();
    }

	void FixedUpdate()
	{
		//Rotate toward player
		transform.up = Player.ins.transform.position - transform.position;
		//Moving toward the player base on direction
		Vector2 movement = transform.position + transform.up * Time.fixedDeltaTime * entity.speed;
		//The repeling modify
		Vector2 repeling = Vector2.zero;
		//Go throught all the enemy has spawn
		for (int i = EnemyManager.ins.enemies.Count - 1; i >= 0 ; i--)
		{
			//Get the enemy in list
			Transform enemy = EnemyManager.ins.enemies[i];
			//If the enemy are active in hierarchy and not null
			if(enemy.gameObject.activeInHierarchy || enemy != null)
			{
				//Check distance of all enemy to see if it shorter than repel range
				if(Vector2.Distance(enemy.position, transform.position) <= repel)
				{
					//Repel away from all the player in range
					repeling += rb.position - (Vector2)enemy.position;
				}
			}
		}
		//Update movement with repeling force
		movement += repeling * Time.fixedDeltaTime;
		//Moving with rigidbody
		rb.MovePosition(movement);
	}
}
