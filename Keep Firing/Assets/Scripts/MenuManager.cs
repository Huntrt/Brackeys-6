using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI musicText, soundText, autoFireText;
	[SerializeField] GameManager game;

	void Awake()
	{
		//Get the game manger
		game = GameManager.ins;
	}

	void Update()
	{
		//Display the auto fire text
		autoFireText.text = "Auto Fire " + game.autoFire.ToString();
		//Display the music info
		musicText.text = "Music "+(int)(game.musicVolume*100)+"%";
		//Display the sound info
		soundText.text = "Sound "+(int)(game.soundVolume*100)+"%";
	}
	
	//Music volume increase and will reset when reach 100
	public void musicVoluming() {game.musicVolume+=0.1f;if(game.musicVolume > 1.1f){game.musicVolume = 0;}}
	//Sound volume increase and will reset when reach 100
	public void soundVoluming() {game.soundVolume+=0.1f;if(game.soundVolume > 1.1f){game.soundVolume = 0;}}
	//Toggeling auto fire
	public void autoFire() {game.autoFire = !game.autoFire;}
	public void ToGame() {SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);}
}