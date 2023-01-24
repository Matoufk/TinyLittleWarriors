using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    /// <summary>
    /// This script is to apply on every character in the scene. You can easily give them starting stats on the Unity right panel
    /// </summary>
    /// 

    public int attack;
    public int life;
    public int maxLife;
    public float speed;
    public int defense;
    public float attackSpeed;
    public int range;
    public bool isAlive = true;


    // Start is called before the first frame update
    void Start()
    {
        maxLife = life;
    }


    // Getters and Setters for the Attack
    public int getAttack()
    {
        return attack;
    }
    public void setAttack(int val)
    {
        attack = val;
    }

    // Getters and Setters for the Life
    public int getLife()
    {
        return life;
    }
    public void setLife(int val)
    {
        life = val;
    }

    // Getters for the MaxLife
    public int getMaxLife()
    {
        return maxLife;
    }

    // Getters and Setters for the Speed
    public float getSpeed()
    {
        return speed;
    }
    public void setSpeed(float val)
    {
        speed = val;
    }

    // Getters and Setters for the Defense
    public int getDefense()
    {
        return defense;
    }
    public void setDefense(int val)
    {
        defense = val;
    }

    // Getters and Setters for the attackSpeed
    public float getAttackSpeed()
    {
        return attackSpeed;
    }
    public void setAttackSpeed(float val)
    {
        attackSpeed = val;
    }

    // Getters and Setters for the Range
    public int getRange()
    {
        return range;
    }
    public void setRange(int val)
    {
        range = val;
    }

    // Boolean if alive
    public bool getAlive()
    {
        return isAlive;
    }
    public void setAlive(bool val)
    {
        isAlive = val;
    }
}
