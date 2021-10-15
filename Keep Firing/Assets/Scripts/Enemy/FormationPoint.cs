using UnityEngine;

public class FormationPoint : MonoBehaviour
{
	[SerializeField] GameObject enemy; Formation formation;

	//Get the formation component from formation
	void Awake() {formation = transform.parent.GetComponent<Formation>();}
	
	//Begin spawn indicator for 2 second
    void OnEnable(){Invoke("Spawn", 2);}

	void Spawn()
	{
		//Create the enemy that has set point in formation using object pooler
		Pool.get.Object(enemy, transform.position, transform.rotation, true);
		//Has create an point
		formation.pointCreate++;
	}
}