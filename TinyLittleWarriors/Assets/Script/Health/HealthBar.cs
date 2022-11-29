using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public float maxHealth;

    public void setMaxHealth(float max)
    {
        maxHealth = max;
    }
    public void SetHealth(float health)
    {

        bar.fillAmount = health/maxHealth;
    }
}
