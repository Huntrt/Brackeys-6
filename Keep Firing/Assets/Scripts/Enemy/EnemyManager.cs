using UnityEngine; using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
	//Turn enemy manager into singelton
	public static EnemyManager ins; void Awake() {ins = this;}
	//The enemy has spawned
	public List<Transform> enemies;
	public List<Heath> enemiesHeath;

	//Search the nearest enemy has spawn at searxcher location and does need to ignore any object
	public Transform SearchNearest(Transform seacher, GameObject ignore = null)
	{
		//Create nearest distance
		float nearest = 10000;
		//The nearest distance's transfrom
		Transform result = null;
		//Go through all the allies transform has been spawn
		for (int n = enemies.Count - 1; n >= 0 ; n--)
		{
			//No enemy has fround
			Transform found = null;
			//If there is objet to ignore than found the enemy if it name don't has ignore object
			if(ignore != null) {if(enemies[n] != ignore) {found = enemies[n];}}
			//Found the enemy if it don't need to ignore
			else {found = enemies[n];}
			//If has found enemy, found enemy are not searcher itself and are active
			if(found != null && found!=seacher && found.gameObject.activeInHierarchy)
			{
				//Get the distance between the healer and the found position
				float distance = Vector2.Distance(transform.position, found.position);
				//If that distance are shorter than the neartest distance
				if(distance < nearest) 
				{
					//The distance are now nearest
					nearest = distance;
					//Mark this found as result
					result = found;
				}
			}
		}
		//Send the found result that are nearest
		return result;
	}
}
