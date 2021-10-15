using UnityEngine;

public class AtkPiercing : MonoBehaviour
{
	PiercingStats _piercing;
	PlayerAttack _attack;
	int pierced;

    void Awake()
    {
		//Get the attack component
		_attack = GetComponent<PlayerAttack>();
		//Get the piercing component from attack
        _piercing = _attack.piercing;
		//Pierce when ever the attack hit something
		_attack.onHit.AddListener(Pierce);
    }

	//Reset the pierce amount
	void OnEnable() {pierced = _piercing.amount;}

	void Update()
	{
		//Deacitve the attack when has no pierce and detach trail from parent
		if(pierced <= 0) {gameObject.SetActive(false); _attack.trail.transform.parent = null;}
	}

	void Pierce(GameObject enemy, Vector3 point)
	{
		//Deacitve the attack when has no pierce and detach trail from parent
		pierced--; if(pierced <= 0) {gameObject.SetActive(false);}
	}
}
