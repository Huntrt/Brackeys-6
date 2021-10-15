using UnityEngine;

public class AtkRicochet : MonoBehaviour
{
	RicochetStats _ricochet;
	PlayerAttack _attack;

    void Start()
    {
		//Get the attack component
		_attack = GetComponent<PlayerAttack>();
		//Get the ricochet component from attack
        _ricochet = _attack.ricochet;
		//Begin recochet when attack hit
		_attack.onHit.AddListener(Ricochet);
    }

    void Ricochet(GameObject hit, Vector3 point)
    {
		//If able to ricochet and there are more than enemy on screen
		if(_ricochet.use && EnemyManager.ins.enemies.Count >= 1)
		{
			//Get the enemy manager
			EnemyManager manager = EnemyManager.ins;
			//If there an an active nearest enemy
			if(manager.SearchNearest(transform, hit).gameObject.activeInHierarchy)
			{
				//Mark the it as target
				Transform target = manager.SearchNearest(transform, hit);
				//Looking toward target
				transform.up = target.position - transform.position;
			}
		}
    }
}
