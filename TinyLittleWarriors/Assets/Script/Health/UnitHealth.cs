using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    //public Unit unite;
    private int maxHealth;
    public int currentHealth;
    public GameObject unit;

    public HealthBar healthBar;

    //Test CollisionUnit2
    //public CollisionUnit collisionUnit;
    //public CollisionUnit2 collisionUnit;

    //public Animator animator;

    private bool isDead = false;

    /*
    * This Method will apply damage on the unit it collided to
    */
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
        //For testing purpose
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (currentHealth > 0) TakeDamage(20);
        }


        //Determine if the unit is dead or not and delete it if it is
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            
            Debug.Log("The unit : " + this.tag + " is dead");
            death();
 
        }


    }

    //Just the method to make the current unit taking an certain amout of damage 
    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void death()
    {
        Destroy(unit);
    }



}
