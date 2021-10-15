using UnityEngine;

public class AtkHoming : MonoBehaviour
{
	HomingStats _homing;
	Transform target;
	bool seeked;

    void Start()
    {
		//Get the homing component from attack
        _homing = GetComponent<PlayerAttack>().homing;
    }

	//Has not seek when created
	void OnEnable() {seeked = false;}

    // Update is called once per frame
    void Update()
    {
		///If no target -> search -> active? ┬Y-> get it location -> look at it
		///									 └N-> search another one -> get it location -> look at it
		///-> stop looking if get to release range
		///This process will reset when target got deactive
		//If allow to homing and haven't seek any enemy
		if(_homing.use && !seeked) {Homing();}
    }


	void Homing()
	{
		//Save the enemy manager
		EnemyManager manager = EnemyManager.ins;
		//If there is enemy nearby
		if(manager.SearchNearest(transform) != null)
		{
			//If there is no target than mark the nearest enemy as target
			if(target == null) {target = manager.SearchNearest(transform);}
			//If there is target but it has been deative than mark the nearest enemy as target
			else{if(!target.gameObject.activeInHierarchy) {target = manager.SearchNearest(transform);}}
			//Get the direction from bullet to target
        	Vector2 direction = target.position - transform.position;
			//Get the distance from the attack to the target
			float distanceToTarget = Vector2.Distance(transform.position, target.position);
			//If the distace between attack and target are not in release range
			if(distanceToTarget > _homing.release)
			{
				//Look toward the target with the rotate speed of accuracy
				transform.right = Vector3.Slerp(transform.right, direction, _homing.accuracy * Time.deltaTime);
			}
			//Stop homing if has in range to release
			if(distanceToTarget < _homing.release)
			{
				//Look at the target final time before stop homing to prevent homing disable upon create
				transform.right = direction;
				//Has seek an enemy
				seeked = true;
			}
		}
	}
}
