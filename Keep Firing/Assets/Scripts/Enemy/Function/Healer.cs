using UnityEngine;

public class Healer : MonoBehaviour
{
	[SerializeField] AudioClip auraSound;
	[SerializeField] GameObject effect, ignore;
	float load; 
	bool healing = false;
	EnemyManager manager;

    void Start()
    {
		//Setting up the support movement
		SupporterSetting();
		//Save the enemy manager
		manager = EnemyManager.ins;
    }

	void Update()
	{
		///Seek nearest allies -> when in range -> healing
		//If currently not healing or allies are no longer active or null
		if(!healing || !supporter.allies.gameObject.activeInHierarchy || supporter.allies != null)
		{
			//Get the nearest allies
			supporter.allies = EnemyManager.ins.SearchNearest(transform, ignore);
		}
		//If there is an allies
		if(supporter.allies != null)
		{
			//Begin healing
			healing = true;
			//If allies distance are now in range to heal
			if(Vector2.Distance(supporter.allies.position, transform.position) <= combat.range)
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
					//Play the aura sound
					GameManager.ins.soundSource.PlayOneShot(auraSound);
					//Rotate particle 90 in the X asic
					Quaternion particleRotation = Quaternion.Euler(90,0,0);
					//Create the healing aura effect at healer postion and rotation without active
					GameObject part = Pool.get.Object(effect,transform.position,Quaternion.identity,false);
					//Set the particle effect of aura scale as range and active it
					part.transform.localScale = new Vector2(combat.range,combat.range); part.SetActive(true);
					//Create an overlap circle that act as autra hitbox using range
					Collider2D[] aura = Physics2D.OverlapCircleAll(transform.position, combat.range);
					//Go through all the allies in range
					for (int a = aura.Length - 1; a >= 0 ; a--)
					{
						//If the object in aura has enemy tag
						if(aura[a].transform.CompareTag("Enemy"))
						{
							//Get transfrom of all the allies in aura
							Transform inAura = aura[a].transform;
							//If the allies are not this object and are active
							if(inAura != transform && inAura.gameObject.activeInHierarchy)
							{
								//Heal all the allies in range
								inAura.GetComponent<Heath>().Heal(combat.damage);
							}
						}
					}
					//Reset loading
					load -= load;
				}
			}
			//No longer healing when go out of range
			else {healing = false;}
		}
	}

	[SerializeField] Supporter supporter;
	public EntityStats entity = new EntityStats();
	public CombatStats combat = new CombatStats();

	void SupporterSetting()
	{
		//Send the entity stats to hunting movement
		supporter.entity = entity;
		//Send the combat stats to distancing movement
		supporter.combat = combat;
	}
}
		///If has speare time - more complex and better
			//1. Search nearest allies
			//2. Active range when reach it
			//3. Get all the enemy in range using overlay circle
			//4. Loop enemy heath and filter allies in range
			//5. Get the lowest heath
			//6. Healing the allies until it full heath or die
			//7. If the allies healing either full heath or die
			//	  a. Repeat step 3 if there still allies available in range
			//    b. Repeat step 1 if no available allies in range
		///Quick method - quick and acceptable
			//1. Find the nearest allies
			//2. Go to it and healing it 
			//3. Keep healing until it full or die
			//4. Search lowset hearth allies and repeat to step 3