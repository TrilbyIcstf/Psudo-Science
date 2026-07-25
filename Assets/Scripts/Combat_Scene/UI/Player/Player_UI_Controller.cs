using UnityEngine;

public class Player_UI_Controller : MonoBehaviour
{
    [SerializeField]
    private PC player;
    public PC Player { get => player; }

    public GameObject PlayerObject { get => gameObject; }

    [SerializeField]
    private GameObject healthBar;
    public GameObject HealthBar { get => healthBar; }
    private Health_UI healthScript;
    public Health_UI HealthScript { get => healthScript; }

    [SerializeField]
    private GameObject energyBar;
    public GameObject EnergyBar { get => energyBar; }
    private Energy_UI energyScript;
    public Energy_UI EnergyScript { get => energyScript; }

    [SerializeField]
    private GameObject deathOverlay;
    public GameObject DeathOverlay { get => deathOverlay; }

    [SerializeField]
    private Player_Animation_Controller anim;
    public Player_Animation_Controller Anim { get => anim; }

    private Combat_Move_Button_Controller buttons;
    public Combat_Move_Button_Controller Buttons { get => buttons; }

    private void Awake()
    {
        healthScript = healthBar.GetComponent<Health_UI>();
        energyScript = EnergyBar.GetComponent<Energy_UI>();
        buttons = GetComponent<Combat_Move_Button_Controller>();
    }
}
