using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;
    public CharacterStats stats;

    public Camera mainCamera;

    private void Update()
    {
        bar.transform.LookAt(bar.transform.position + mainCamera.transform.rotation * Vector3.back, mainCamera.transform.rotation * Vector3.down);
    }
    private void Start()
    {
        bar.fillAmount = stats.getMaxLife();
    }
    public void SetHealth(float health)
    {

        bar.fillAmount = health/stats.getMaxLife();
    }
}
