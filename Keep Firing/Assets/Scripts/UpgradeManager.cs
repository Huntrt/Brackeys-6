using UnityEngine;
using System.Collections.Generic;
using System;

public class UpgradeManager : MonoBehaviour
{
	//Make UpgradeManager into singleton
	public static UpgradeManager ins; void Awake() {ins = this;}
	Player player;
	[SerializeField] AudioClip lvUpSound;
	//The level the exp required to the next level and the current exp
	[SerializeField] int level; [SerializeField] float expRequired, currentExp, displayDuration;
	[SerializeField] TMPro.TextMeshProUGUI expDisplay, upgradeDisplay;
	public enum UpgradeType ///Enable power when called function
	{
		IncreaseHeath,
		IncreaseMoveSpeed, IncreaseAttackSpeed,
		IncreaseBarrageAmount,
		IncreaseDamage, IncreaseBulletVelocity, IncreaseBulletRange,
		IncreasePiercing,
		IncreaseExplosionDamageScale, IncreaseExplosionSize,
		IncreaseHomingAccuracy,
		IncreaseRicochetRange
	}
	[SerializeField] float upgradeChance;
	public List<UpgradeOption> upgrades = new List<UpgradeOption>();
	[System.Serializable] public class UpgradeOption
	{
		public UpgradeType upgrade;
		public float chance;
	}

	void Start()
	{
		//Reset the current exp upon start
		currentExp = expRequired;
		//Getting the player
		player = Player.ins;
	}

    void Update()
    {
		//Display the current exp
		expDisplay.text = "Next Level " + (int)currentExp;
    }
	
    public void GetPoint(float point)
    {
		//Deceease the current exp with point has get
        currentExp -= point;
		//If the current exp has reached 0
        if(currentExp <= 0)
		{
			//Increase exp required when level up
			expRequired *= 1.35f;
			//Leveling up
			level++; LevelUp();
			//Reset the current exp upon levelup
			currentExp = expRequired;
		}
    }

	void LevelUp()
	{
		//The total chance of getting all upgrade
		float totalUpgradeChance = 0;
		//Calculated the total chance of all upgrades
		for (int d = upgrades.Count - 1; d >= 0 ; d--) {totalUpgradeChance += upgrades[d].chance;}
		//Get the random between 0 to total upgrades chance
		upgradeChance = UnityEngine.Random.Range(0,totalUpgradeChance);
		//Save the random value
		float random = upgradeChance;
		//Go throught all the upgrades in list
		for (int x = upgrades.Count - 1; x >= 0 ; x--)
		{
			//If random decrease with chance are lower than 0
			if((random - upgrades[x].chance) <= 0) 
			{
				//Run the upgrade base on upgrade option
				Invoke(upgrades[x].upgrade.ToString(),0);
				//Break the loop
				break;
			}
			//Decrease the chance has use if not lower than 0
			else {random -= upgrades[x].chance;}
		}
		//Increase the difficult
		Spawner.ins.IncreaseDifficulty(level);
		//Play the level up sound
		GameManager.ins.soundSource.PlayOneShot(lvUpSound);
	}

	void IncreaseHeath()
	{
		player.heath.Heal(3);
		//Set the display text to custom
		upgradeDisplay.text = "Level " + level + "\n" + "Heal 3";
		//Begin reseting display text
		StopCoroutine("ResetUpgradeDisplay"); StartCoroutine("ResetUpgradeDisplay");
	}

	void IncreaseMoveSpeed()
	{
		player.entity.speed += (0.05f * player.entity.speed);
		//Set the display text to custom
		upgradeDisplay.text = "Level " + level + "\n" + "Increase Moving Speed";
		//Begin reseting display text
		StopCoroutine("ResetUpgradeDisplay"); StartCoroutine("ResetUpgradeDisplay");
	}

	void IncreaseAttackSpeed()
	{
		player.entity.attackSpeed += 0.6f;
		//Set the display text to custom
		upgradeDisplay.text = "Level " + level + "\n" + "Increase Attacking Speed";
		//Begin reseting display text
		StopCoroutine("ResetUpgradeDisplay"); StartCoroutine("ResetUpgradeDisplay");
	}

	void IncreaseBarrageAmount()
	{
		player.barrage.amount++;
		//Set the display text to custom
		upgradeDisplay.text = "Level " + level + "\n" + "+1 Projectile";
		//Begin reseting display text
		StopCoroutine("ResetUpgradeDisplay"); StartCoroutine("ResetUpgradeDisplay");
	}

	void IncreaseDamage()
	{
		player.combat.damage += 1f;
		//Set the display text to custom
		upgradeDisplay.text = "Level " + level + "\n" + "Increase Damage";
		//Begin reseting display text
		StopCoroutine("ResetUpgradeDisplay"); StartCoroutine("ResetUpgradeDisplay");
	}

	void IncreaseBulletVelocity()
	{
		player.combat.velocity += (0.3f * player.combat.velocity);
		//Set the display text to custom
		upgradeDisplay.text = "Level " + level + "\n" + "Increase Projectile Velocity";
		//Begin reseting display text
		StopCoroutine("ResetUpgradeDisplay"); StartCoroutine("ResetUpgradeDisplay");
	}

	void IncreaseBulletRange()
	{
		player.combat.range++;
		//Set the display text to custom
		upgradeDisplay.text = "Level " + level + "\n" + "Increase Projectile Range";
		//Begin reseting display text
		StopCoroutine("ResetUpgradeDisplay"); StartCoroutine("ResetUpgradeDisplay");
	}

	void IncreasePiercing()
	{
		player.piercing.amount++;
		//Set the display text to custom
		upgradeDisplay.text = "Level " + level + "\n" + "+1 Piercing";
		//Begin reseting display text
		StopCoroutine("ResetUpgradeDisplay"); StartCoroutine("ResetUpgradeDisplay");
	}

	void IncreaseExplosionDamageScale()
	{
		player.explosion.use = true;
		player.explosion.scaleDamage += 25f;
		//Set the display text to custom
		upgradeDisplay.text = "Level " + level + "\n" + "Increase Explosion Damage";
		//Begin reseting display text
		StopCoroutine("ResetUpgradeDisplay"); StartCoroutine("ResetUpgradeDisplay");
	}

	void IncreaseExplosionSize()
	{
		player.explosion.use = true;
		player.explosion.size += 1f;
		//Set the display text to custom
		upgradeDisplay.text = "Level " + level + "\n" + "Increase Explosion Size";
		//Begin reseting display text
		StopCoroutine("ResetUpgradeDisplay"); StartCoroutine("ResetUpgradeDisplay");
	}

	void IncreaseHomingAccuracy()
	{
		player.homing.use = true;
		player.homing.accuracy += 0.3f;
		//Set the display text to custom
		upgradeDisplay.text = "Level " + level + "\n" + "Increase Homing Accuracy";
		//Begin reseting display text
		StopCoroutine("ResetUpgradeDisplay"); StartCoroutine("ResetUpgradeDisplay");
	}

	System.Collections.IEnumerator ResetUpgradeDisplay()
	{
		//Wait for an set duration
		yield return new WaitForSeconds(displayDuration);
		//Clear the display
		upgradeDisplay.text = "";
	}
}
