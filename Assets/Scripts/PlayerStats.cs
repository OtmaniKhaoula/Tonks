using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float Health;
    public float healthOverTime;

    public float Hunger;
    public float hungerOverTime;

    public float Stamina;
    public float staminaOverTime;


    public Slider HealthBar;
    public Slider HungerBar;
    public Slider StaminaBar;

    public float minAmount = 5f;

    public float speed;
    public int strength;
    public float attackRange;

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.maxValue = Health;
        StaminaBar.maxValue = Stamina;
        HungerBar.maxValue = Hunger;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateValues();
    }

    private void CalculateValues()
    {
        Hunger -= hungerOverTime * Time.deltaTime;

        if(Hunger <= minAmount)
        {
            Health -= healthOverTime * Time.deltaTime;
            Stamina -= staminaOverTime * Time.deltaTime;
        }

        if(Health <= 0)
        {
            print("Player has died");
        }

        //Less stamina when attacking || Stamina = mana 
        updateUI();
    }

    private void updateUI()
    {
        Health = Mathf.Clamp(Health, 0, 100f);
        Hunger = Mathf.Clamp(Hunger, 0, 100f);
        Stamina = Mathf.Clamp(Stamina, 0, 100f);

        HealthBar.value = Health;
        StaminaBar.value = Stamina;
        HungerBar.value = Hunger;
    }
}
