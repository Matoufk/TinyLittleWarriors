using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectInteract : MonoBehaviour, IPointerClickHandler
{

    public bool clicked = false;
    private bool appliedAmelio = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        clicked = !clicked;


        if (!clicked)
        {
            RemoveAmelio();
        }
        else {
            ApplyAmelio();
        }
    }

    public void ApplyAmelio()
    {
        
            if (this.name == "sword")
            {
                Amelioration.applyAttackBoostGlobal();
            }
            if (this.name == "apple")
            {
                Amelioration.applySpeedBoostGlobal();
            }
            if (this.name == "armor")
            {
                Amelioration.applyDefenseBoostGlobal();
            }
            appliedAmelio = true;
        
    }

    public void RemoveAmelio()
    {
        
            if (this.name == "sword")
            {
                Amelioration.applyAttackBoostGlobal(0.5f);
            }
            if (this.name == "apple")
            {
                Amelioration.applySpeedBoostGlobal(0.5f);
            }
            if (this.name == "armor")
            {
                Amelioration.applyDefenseBoostGlobal(0.5f);
            }
            appliedAmelio = true;
        
    }
}
