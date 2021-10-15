using UnityEngine;

public class Tanker : MonoBehaviour
{
	[SerializeField] AudioClip stompSound;
	[SerializeField] GameObject stompEffect;
	[SerializeField] Transform stompTF;
	float load;

    void Start()
    {
		//Setting up the frontliner movement
		FrontlinerSetting();
    }

	void Update()
	{
		//If the tanker has got in range of the player
		if(Vector2.Distance(Player.ins.transform.position,transform.position) <= combat.range)
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
				//Play the stomping sound
				GameManager.ins.soundSource.PlayOneShot(stompSound);
				//Create an overlap circle that drawn at stomp point and scale with combat range
				Collider2D attack = Physics2D.OverlapCircle(stompTF.position,combat.range);
				//Create the stomp effect from pool at the attack point with none rotation
				GameObject effect = Pool.get.Object(stompEffect,stompTF.position,Quaternion.identity);
				//Set the effect scale base on combat range
				effect.transform.localScale = new Vector2(combat.range, combat.range);
				//Hurt the player if the overlap circle hit it
				if(attack.CompareTag("Player")) {Player.ins.heath.Hurt(combat.damage);}
				//Reset loading
				load -= load;
			}
		}
	}

	[SerializeField] Frontliner frontliner;
	public EntityStats entity = new EntityStats();
	public CombatStats combat = new CombatStats();

	void FrontlinerSetting()
	{
		//Send the entity stats to frontliner movement
		frontliner.entity = entity;
	}
}
