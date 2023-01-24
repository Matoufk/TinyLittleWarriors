using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public CharacterStats stats;

    private void Start()
    {
        bar.fillAmount = stats.getMaxLife();
    }
    public void SetHealth(float health)
    {

        bar.fillAmount = health/stats.getMaxLife();
    }
}
