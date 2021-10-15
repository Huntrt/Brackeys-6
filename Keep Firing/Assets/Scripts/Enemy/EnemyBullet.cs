using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBullet : MonoBehaviour
{
	public CombatStats _combat = new CombatStats();
	public Rigidbody2D rb;
	float travelled; Vector2 oldPos;
	//Create an event that send enemy got hit and it position
	[Serializable] public class Hitting : UnityEvent<GameObject, Vector3>{} public Hitting onHit;

    void Awake()
    {
		//Get the rigidbody component
		rb = GetComponent<Rigidbody2D>();
    }

	void OnEnable()
	{
		//Reset the old position and travelled distance
		oldPos = transform.position; travelled -= travelled;
	}

    void Update()
    {
		//Update the bullet velocity as the speed stats
		rb.velocity = transform.right * _combat.velocity;
		//Get how many unit this frame has travel
		float frameTravel = Vector2.Distance(transform.position, oldPos);
		//Adding the time has travel per frame
		travelled += frameTravel;
		//If has travel all the range
		if(travelled >= _combat.range )
		//Deactive the gameobject
		{gameObject.SetActive(false);}
		//Update the old position
		oldPos = transform.position;
    }

	private void OnCollisionEnter2D(Collision2D other) 
	{
		//If the object got collide are player
		if(other.transform.CompareTag("Player"))
		{
			//Hurting the player
			Player.ins.heath.Hurt(_combat.damage);
			//Deactive the attack
			gameObject.SetActive(false);
		}
		//Deactive bullet when collide with brder
		if(other.transform.CompareTag("Border")) {gameObject.SetActive(false);}
	}
}