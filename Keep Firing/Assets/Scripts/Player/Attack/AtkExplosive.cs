using UnityEngine;

public class AtkExplosive : MonoBehaviour
{
	[SerializeField] AudioClip explosiveSound;
	ExplosionStats _explosion;
	PlayerAttack _attack;
	[SerializeField] GameObject effect;

    void Start()
    {
		//Get the player attac component of this attack
		_attack = GetComponent<PlayerAttack>();
		//Get the explosion component from player attack
		_explosion = _attack.explosion;
		//Run explode when attack hit something
		_attack.onHit.AddListener(Explode);
    }

	void Explode(GameObject hit, Vector3 point)
	{
		//If allow to explosive
		if(_explosion.use)
		{
			//Play the explosive sound
			GameManager.ins.soundSource.PlayOneShot(explosiveSound);
			//Get the explosion effecytransfrom by create it at hit point, custom rotate and not auto active
			GameObject exp = Pool.get.Object(effect.gameObject, point, Quaternion.Euler(90,0,0), false);
			//Set the particle effect of explosion scale as range and active it
			exp.transform.localScale = new Vector2(_explosion.size,_explosion.size); exp.SetActive(true);
			//Get the scaled damage using percent of the attack damage
			float scaledDamage = (_explosion.scaleDamage/100) * _attack.combat.damage;
			//Create an over lap circle all at hit point scale with size stats that get all object hit
			Collider2D[] Explosion = Physics2D.OverlapCircleAll(point, _explosion.size);
			//Go through all the enemy in list of enemies heath in manager
			for (int e = EnemyManager.ins.enemiesHeath.Count - 1; e >= 0 ; e--)
			{
				//Get the enemy heath from list
				Heath InList = EnemyManager.ins.enemiesHeath[e];
				//Go through all the object that got hit by explosion
				for (int r = Explosion.Length - 1; r >= 0 ; r--)
				{
					//If the object are active and it has enemy tag
					if(Explosion[r].gameObject.activeInHierarchy && Explosion[r].CompareTag("Enemy"))
					{
						//Deal scaled damage to it if it inside the heath list
						if(Explosion[r].transform == InList.transform){InList.Hurt(scaledDamage);}
					}
				}
			}
		}
	}
		
}
