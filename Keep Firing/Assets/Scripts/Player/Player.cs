using UnityEngine;
using UnityEngine.UI;

public  class Player : MonoBehaviour
{
	public GameObject overMenu;
	//Make the player singleton 
	public static Player ins;
	public EntityStats entity = new EntityStats();
	[HideInInspector] public Heath heath;
	[SerializeField] TMPro.TextMeshProUGUI heathCounter;
	public BarrageStats barrage = new BarrageStats();
	public CombatStats combat = new CombatStats();
	public PiercingStats piercing = new PiercingStats();
	public ExplosionStats explosion = new ExplosionStats();
	public HomingStats homing = new HomingStats();
	public RicochetStats ricochet = new RicochetStats();

    void Awake()
    {
		//Singleton are this component
		ins = this;
		//Get the heath component on player
        heath = GetComponent<Heath>();
    }

	void OnEnable()
	{
		//If over menu are not null than deactive
		if(overMenu != null) {overMenu.SetActive(false);}
		//If the play manager are not null than reset time scale
		if(PlayManager.ins != null) {Time.timeScale = PlayManager.ins.defaultTime;}
	}

	void Update()
	{
		//Display current heath and max heath
		heathCounter.text = "HP " + heath.curHeath;
	}

	void OnDisable()
	{
		//Show 0 hp when die
		heathCounter.text = "HP 0";
	}
}
