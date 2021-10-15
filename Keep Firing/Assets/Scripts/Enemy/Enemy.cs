using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Start()
    {
		//Add the transfrom of the enemy has been spawn
        EnemyManager.ins.enemies.Add(transform);
		//Add the heath of the enemy has been spawn
        EnemyManager.ins.enemiesHeath.Add(GetComponent<Heath>());
    }
}