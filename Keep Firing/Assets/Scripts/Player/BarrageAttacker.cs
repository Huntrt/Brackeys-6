using UnityEngine; using System.Collections.Generic;

public class BarrageAttacker : MonoBehaviour
{
	[SerializeField] BarrageStats barrage;
	[SerializeField] Transform anchor, muzzle, firepoint;
 	[SerializeField] Reloader reloader;

    void Start()
    {
		//Get the barrage stats from player
		barrage = Player.ins.barrage;
		//Create projectile when attacker are called
		reloader.attacking.AddListener(Creating);
    }

	void Creating()
	{
		//If there is only 1 attack create 1 and set it rotation as anchor
		if(barrage.amount <= 1) {CreateAttack(anchor.rotation);}
		//If there is multiple attack
		else
		{
			//Get the spread by using it with amount
			float spread = barrage.amount*2;
			//Range are the focus stats got divide by 2 since it affect 2 direction
			float range = spread / 2;
			//Get the 180 to -180 rotation of the anchor
			float rot = anchor.localEulerAngles.z; float center = (rot > 180) ? rot-360 : rot;
			//Get the start and end rotation by decrease and increase the center with range
			float start = center - range; float end = center + range;
			//Get the length between each step on the spread stat
			//-1 amount cuase has 1 extra than step, e.g: A = attack | s = step | A <-s-> A <-s-> A
			float step = spread / (barrage.amount-1);
			//Begin the frist angle at start
			float angle = start;
			//For each of the attack need to create
			for (int i = barrage.amount - 1; i >= 0 ; i--)
			{
				//Create attack with the rotation has get
				CreateAttack(Quaternion.Euler(0,0,angle));
				//Proceed to the next step
				angle += step;
			}
		}
	}

    void CreateAttack(Quaternion rotation)
    {
		//Get object from pool with attack and set it position custom rotation and auto active
    	Pool.get.Object(reloader.attack, firepoint.position, rotation, true);
		//Create the muzzle flash at firepoint position and get it and do not active it
		GameObject m = Pool.get.Object(muzzle.gameObject, firepoint.position, rotation);
		//Set the muzzle as the attacker children look at the direction and active it
		m.transform.right = anchor.right; m.transform.parent = transform;
    }
}
