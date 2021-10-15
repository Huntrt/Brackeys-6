using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
	[SerializeField] GameObject pauseMenu;
	[SerializeField] TextMeshProUGUI musicText, soundText, autoFireText;
	[SerializeField] GameManager game;
	[SerializeField] UnityEngine.UI.Image acceptProgress, exitProgress;
	[SerializeField] float acceptDuration, curAccept; bool accepting;
	public float defaultTime;
	public static PlayManager ins;

	void Awake()
	{
		//Get the game manger
		game = GameManager.ins;
		//Get the timescale
		defaultTime = Time.timeScale;
		//Turn player manager to singelton
		ins = this;
	}

	void Update()
	{
		//Begin counting when accepting and reset when not
		if(accepting) {curAccept += Time.unscaledDeltaTime;} else {curAccept -= curAccept;}
		//If the game has paused
		if(Time.timeScale == 0)
		{
			//Show accept progress loading
			acceptProgress.fillAmount = curAccept/acceptDuration;
			//Show ext progress loading
			exitProgress.fillAmount = curAccept/acceptDuration;
		}
		//Toggle Pause menu when pressed esc key
		if(Input.GetKeyDown(KeyCode.Escape)) {if(!pauseMenu.activeInHierarchy){Pause();}else{Continue();}}
		//If current accept has reach duration
		if(curAccept >= acceptDuration)
		{
			//Reset the timescale
			Time.timeScale = defaultTime;
			//Reset current accept and go back to menu
			curAccept -= curAccept; SceneManager.LoadScene("Menu", LoadSceneMode.Single);
		}
		//Display the auto fire text
		autoFireText.text = "Auto Fire " + game.autoFire.ToString();
		//Display the music info
		musicText.text = "Music "+(int)(game.musicVolume*100)+"%";
		//Display the sound info
		soundText.text = "Sound "+(int)(game.soundVolume*100)+"%";
	}

	public void Pause()
	{
		//Only pause if the game are running
		if(Time.timeScale != 0)
		//Active the pause menu and pause the game
		pauseMenu.SetActive(true); Time.timeScale = 0;
	}
	public void Continue()
	{
		//Deactive the pause menu and continue the game
		pauseMenu.SetActive(false); Time.timeScale = defaultTime;
	}
	
	//Music volume increase and will reset when reach 100
	public void musicVoluming() {game.musicVolume+=0.1f;if(game.musicVolume > 1.1f){game.musicVolume = 0;}}
	//Sound volume increase and will reset when reach 100
	public void soundVoluming() {game.soundVolume+=0.1f;if(game.soundVolume > 1.1f){game.soundVolume = 0;}}
	//Toggeling auto fire
	public void autoFire() {game.autoFire = !game.autoFire;}
	//Reset the accept base on event call
	public void StartAccept(){accepting = true;}public void CancelAccept(){accepting = false;}
}