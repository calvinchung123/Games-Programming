using UnityEngine;
using UnityEngine.UI;

public class Red_NodeUI : MonoBehaviour
{

    public GameObject ui;
    public Text sellAmount;
    private Red_Node target;

    public void SetTarget(Red_Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();
        ui.SetActive(true);

        sellAmount.text = "Sell\n" + "$" + target.turretBlueprint.GetSellAmount();

    }

    public void Hide()
    {
        ui.SetActive(false);

    }

    public void Sell()
    {
        target.SellTurret();
    }
}
