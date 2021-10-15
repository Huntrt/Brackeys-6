using UnityEngine;
using System.Collections.Generic;

public enum dif {easy, normal, hard, extreme, insane}

public class Spawner : MonoBehaviour
{
	//AN static class of this class
	public static Spawner ins; 
	[Tooltip("How long between each spawn")]
	public float spawnRate, curSpawnrate;
	[Tooltip("How many formation when spawn")]
	public float spawnFrequent;
	
	[Header("Formation Manager")]
	[SerializeField] float formatorChance;
	public float totalFormatorRate;
	[Header("――――――――――――――――――――――――――――――――――――――――――――")]
	public List<FormationInfo> formators = new List<FormationInfo>();
	[System.Serializable] public class FormationInfo
	{
		[Tooltip("The formation object")]
		public GameObject formation;
		[Tooltip("The formation diffucult")]
		public dif difficult;
		[Tooltip("The formation chance")]
		public float rate;
	}
	[Header("Difficult Manager")]
	[Header("――――――――――――――――――――――――――――――――――――――――――――")]
	public dif difficult;
	[SerializeField] float difficultChance;
	public float totalDifficultsRate;
	public List<DifficultInfo> difficults = new List<DifficultInfo>();
	[System.Serializable] public class DifficultInfo
	{
		[Tooltip("The difficult type")]
		public dif type;
		[Tooltip("The difficult rate")]
		public float rate;
	}
	public List<Transform> spawnPoint = new List<Transform>();

	//Make the spawner to singleton
	void Awake() {ins = this;}

    void Start() ///Replace to send an event to re-caculated
    {	
		//Clear the total rate of difficult
		totalDifficultsRate -= totalDifficultsRate;
		//Calculated the total rate of all difficult
		for (int d = difficults.Count - 1; d >= 0 ; d--) {totalDifficultsRate += difficults[d].rate;}
    }

	void Update()
	{
        //If current are lower than spawnrate
		if(curSpawnrate < (1/spawnRate))
		{
			//Begin loading
			curSpawnrate += Time.deltaTime;
		}
		//If current has reach spawnrate
		else
		{
			//Reset spawn rate
			curSpawnrate -= curSpawnrate;
			//Begin calculted difficult and so on...
			Difficuliting();
		}
	}

	void SpawnRating(float amount){spawnRate += amount;} //Increase spawn rate

	public void IncreaseDifficulty(int level)
	{
		//Increase spawn rate when reach level the can be divide by 5
		if(level % 5 == 0) {spawnRate += 0.1f;}
		//If level are smaller than 40
		if(level < 40)
		{
			//Get the random between difficult to modify them
			float diffModify = Random.Range(0, level);
			//Go trough all the difficult in list
			for (int d = 0; d < difficults.Count; d++)
			{
				//Save the difficults rate and it type 
				float rate = difficults[d].rate; dif type = difficults[d].type;
				//Decrease the difficults if it is easy and clamming it to an certain value
				if(type == dif.easy){rate -= diffModify; difficults[d].rate = Mathf.Clamp(rate, 0, 50);}
				//Increase the difficults if it is normal
				if(type == dif.normal){difficults[d].rate += diffModify*1.5f;}
				//Increase the difficults if it is hard
				if(type == dif.hard) {difficults[d].rate += diffModify*1.25f;}
				//Increase the difficults if it is extreme
				if(type == dif.extreme) {difficults[d].rate += diffModify*1.05f;}
			}
		}
		//Sprting difficult
		difficults.Sort(DiffSort);
	}

	//Begin sorting difficulty
	static int DiffSort(DifficultInfo dif1, DifficultInfo dif2)
	{
		//Compare the difficult 1 to 2
		return dif1.rate.CompareTo(dif2.rate);
	}

	void Difficuliting()
	{
		//Get the random chance between 0 to total difficult rate
		difficultChance = Random.Range(0,totalDifficultsRate);
		//Save the chance
		float chance = difficultChance;
		//Go throught all the difficult in list
		for (int x = difficults.Count - 1; x >= 0 ; x--)
		{
			//If it chance decrease with rate are lower than 0
			if((chance - difficults[x].rate) <= 0) 
			{
				//Set the difficult
				difficult = difficults[x].type; 
				//Begin to decdie which formation to use
				SpawnDecide();
				//Break the loop
				break;
			}
			//Decrease the rate has use if not lower than 0
			else {chance -= difficults[x].rate;}
		}
	}

	void SpawnDecide()
	{
		//Save the chance
		float chance = formatorChance;
		//Reset the formation rate
		totalFormatorRate -= totalFormatorRate;
		//Go throught all the formation in list
		for (int f = formators.Count - 1; f >= 0 ; f--)
		//Get the rate of all formation in current difficult
		{if(formators[f].difficult == difficult) {totalFormatorRate += formators[f].rate;}}
		//Get the random chance between 0 to total formation rate
		formatorChance = Random.Range(0,totalFormatorRate);
		//Go throught all the formation in list
		for (int x = 0; x < formators.Count; x++)
		{
			//If the formation are in correct difficult
			if(formators[x].difficult == difficult)
			{
				//Create formatoion if it chance decrease with rate are lower than 0 than break the loop
				if((chance - formators[x].rate) <= 0) {Spawning(formators[x].formation); break;}
				//Decrease the rate has use if not lower than 0
				else {chance -= formators[x].rate;}
			}
		}
	}

	void Spawning(GameObject spawn)
	{
		//Randomly chose between the all spawn point
		int chosePoint = Random.Range(0, spawnPoint.Count);
		//Get the order of chose point in the spawn point list
		Transform point = spawnPoint[chosePoint];
		//Set the spawn position
		Vector2 spawnPosition = new Vector2
		//Randomly chose X position using the spawnerpoint scale and it current position
		(point.position.x + Random.Range(-point.localScale.x/2, point.localScale.x/2),
		//Randomly chose Y position using the spawnerpoint scale and it current position
		point.position.y + Random.Range(-point.localScale.y/2, point.localScale.y/2));
		//Create formation with an random position with set rotation and auto active
		Pool.get.Object(spawn, spawnPosition, Quaternion.identity, true);
	}
}