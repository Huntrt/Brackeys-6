using UnityEngine; using UnityEngine.Events;

public class Reloader : MonoBehaviour
{
	[SerializeField] AudioClip attackingSound;
	float load; 
	public GameObject attack;
	[SerializeField] bool ready;
	public UnityEvent attacking;

    void Update()
    {
		//If ready to attack while press mouse
		if(ready)
		{
			//If there are gamemanger
			if(GameManager.ins != null)
			{
				//Keep firing if auto fire are on 
				if(GameManager.ins.autoFire) {Firing();}
				//Click mouse to fire if auto fire are of
				else {if(Input.GetMouseButton(0)) {Firing();}}
			}
		}
        //If load are lower than how many attack perform in 1 second
		if(load < (1/Player.ins.entity.attackSpeed))
		{
			//Begin loading
			load += Time.deltaTime;
			//Attack no longer ready
			ready = false;
		}
		//If load has reach attack speed
		else
		{
			//Attack are ready
			ready = true;
		}
    }

	void Firing()
	{
		//Begin attacking
		attacking.Invoke();
		//Reseting load
		load -= load;
		//Play the attacking sound
		GameManager.ins.soundSource.PlayOneShot(attackingSound);
	}
}
