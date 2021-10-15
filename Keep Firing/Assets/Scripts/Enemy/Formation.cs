using UnityEngine;

public class Formation : MonoBehaviour
{
    public int pointCreate;

    void Update()
    {
		//If has create all the point
        if(pointCreate == transform.childCount) 
		{
			//Reseting point counter
			pointCreate -= pointCreate;
			//Deactive formation
			gameObject.SetActive(false);
		}
    }
}
