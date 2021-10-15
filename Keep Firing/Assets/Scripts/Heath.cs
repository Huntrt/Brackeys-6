using UnityEngine; using System.Collections;

public class Heath : MonoBehaviour
{
	public float maxHeath, curHeath;
	[SerializeField] float damagePoint, killPoint, killMulti;
	[SerializeField] float flashDuration; SpriteRenderer sprite;
	[SerializeField] Color defaultColor, hurtColor, healColor;
	[SerializeField] GameObject trail, hurtEffect, healEffect, dieEffect;
	[SerializeField] AudioClip healSound, hurtSound, dieSound;
	GameManager manager;

	void Awake()
	{
		//Get the sprite renderer
		sprite = GetComponent<SpriteRenderer>();
		//Save the sprite default color
		defaultColor = sprite.color;
		//Get the game manager
		manager = GameManager.ins;
	}

	//reset current Heath when active and sprite color
	void OnEnable() {curHeath = maxHeath; sprite.color = defaultColor;}

	void Update()
	{
		//Deactive object if it out of heath
		if(curHeath <= 0)
		{
			//Play the die sound in sound source
			manager.soundSource.PlayOneShot(dieSound);
			//Deactive an trial if has one
			if(trail!=null){trail.transform.parent = null;}
			//Getting kill point and multiplier
			PointManager.ins.Pointing(killPoint, killMulti);
			//Create the die effect
			Pool.get.Object(dieEffect, transform.position, Quaternion.identity);
			//Show and pause the game if the object are player
			if(gameObject.CompareTag("Player")) {Player.ins.overMenu.SetActive(true);Time.timeScale = 0;}
			//Destroy the object
			gameObject.SetActive(false);
		}
	}

	IEnumerator ResetEffect()
	{
		//Wait for an delay
		yield return new WaitForSeconds(flashDuration);
		//Reset back top default color
		sprite.color = defaultColor;
	}

	public void Hurt(float value) 
	{
		//Decrease current heath with value has take
		curHeath -= value;
		//Set the color to hurt
		sprite.color = hurtColor;
		//Play the heal sound in sound source
		manager.soundSource.PlayOneShot(hurtSound);
		//Getting damage point and no multiplier
		PointManager.ins.Pointing(damagePoint, 0);
		//Create the hurt effect
		Pool.get.Object(hurtEffect, transform.position, Quaternion.identity);
		//Reseting hurting flash
		StopCoroutine("ResetEffect"); StartCoroutine("ResetEffect");
	}

	public void Heal(float value) 
	{
		//Increase current heath with value has take
		curHeath += value;
		//Set the color to heal
		sprite.color = healColor;
		//Play the heal sound in sound source
		manager.soundSource.PlayOneShot(healSound);
		//Create the heal effect
		Pool.get.Object(healEffect, transform.position, Quaternion.identity);
		//Reseting healing flash
		StopCoroutine("ResetEffect"); StartCoroutine("ResetEffect");
	}
}