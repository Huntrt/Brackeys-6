using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public AudioSource musicSource, soundSource;
	public float musicVolume, soundVolume;
	public bool autoFire;
	public static GameManager ins;

	void Awake()
	{
		//Create singelton and don't destroy on load when neede
		if(GameManager.ins == null) {ins = this;DontDestroyOnLoad(this);}
		//If in the mananger create scene
		if(SceneManager.GetActiveScene().name == "ManagerCreate")
		//Go to the menu scene
		{SceneManager.LoadScene("Menu", LoadSceneMode.Single);}
	}

	void Update()
	{
		//Set the music volume 
		musicSource.volume = musicVolume;
		//Set the sound volume 
		soundSource.volume = soundVolume;
	}
}
