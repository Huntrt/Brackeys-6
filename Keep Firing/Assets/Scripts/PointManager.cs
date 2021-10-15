using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointManager : MonoBehaviour
{
	//Make the PointManager into singleton
	public static PointManager ins; void Awake() {ins = this;}
	[SerializeField] TextMeshProUGUI pointCounter, multiCounter, overPoint;
	[SerializeField] Image multiProgress;
	[SerializeField] float point, multiplier, multiRequired, curMulti, multiDecease;
	float deceaseCounter;

    void Update()
    {
		//Update the game over final point
		overPoint.text = ((int)point).ToString();
		//If has current has reach required
		if(curMulti >= multiRequired)
		{
			//Reset the current by using leftover fron curMUlti
			curMulti = curMulti - multiRequired;
			//Multiplier get 10%
			multiplier += 10;
		}
		//If current has go below zero
		if(curMulti < 0)
		{
			//Reset the current multi to the previous max
			curMulti = multiRequired-1;
			//Multiplier loss 10%
			multiplier -= 10;
		}
		//If multiplier begin
		if(multiplier > 1) 
		{
			//Get decease speed (e.g: 120(multiplier)/10 = 12 per sec)
			multiDecease = multiplier / 10;
			//Get how fast will lose each point per sec
        	float deceaseRate = 1/multiDecease;
			//If counter are lower than decease rate
			if(deceaseCounter < deceaseRate)
			{
				//Begin counting
				deceaseCounter += Time.deltaTime;
			}
			//If counter has reached rate
			else
			{
				//Current multipler progress loss 1
				curMulti--;
				//Reset loading
				deceaseCounter -= deceaseCounter ;
			}
		}
		//Clamp point to 0
		point = Mathf.Clamp(point, 0, Mathf.Infinity);
		//Clamp multiplier to 0
		multiplier = Mathf.Clamp(multiplier, 0, Mathf.Infinity);
		//Display point and multiplier in Text UI
		pointCounter.text = (int)point + "";multiCounter.text = "x"+multiplier+"%";
		//Filling the progress image base on mulitple cur and req
		multiProgress.fillAmount = curMulti/multiRequired;
    }

	//Get point when called with custom amount
	public void Pointing(float recive, float multiplie)
	{
		//Increase the curMulti progress
		curMulti += multiplie;
		//Get point recive that multiplie by the multiplier percent
		float reciveModify = ((multiplier/100) * recive) + recive;
		//Increase point with the recive modified
		point += reciveModify;
		//Send the recive modified to upgrade
		UpgradeManager.ins.GetPoint(reciveModify);
	}
}