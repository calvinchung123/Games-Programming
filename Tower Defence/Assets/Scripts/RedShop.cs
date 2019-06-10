using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedShop : MonoBehaviour {

    public TurretBlueprint RedstandardTurret;
    public TurretBlueprint RedmissileTurret;
    public TurretBlueprint RedlaserTurret;

    Red_BuildManager buildmanger;

    void Start()
    {
        buildmanger = Red_BuildManager.instance;
    }

    void Update()
    {
        if(Input.GetKeyDown("9"))
        {
            SelectStandardTurret();
        }
        if (Input.GetKeyDown("0"))
        {
            SelectMissileTurret();
        }
        if (Input.GetKeyDown("-"))
        {
            SelectLaserTurret();
        }
    }

	public void SelectStandardTurret ()
    {
        Debug.Log("Standard turret selected");
        buildmanger.SelectRedTurretToBuild(RedstandardTurret);
    }

    public void SelectMissileTurret()
    {
        Debug.Log("Missile turret select");
        buildmanger.SelectRedTurretToBuild(RedmissileTurret);
    }

    public void SelectLaserTurret()
    {
        Debug.Log("Laser turret select");
        buildmanger.SelectRedTurretToBuild(RedlaserTurret);
    }
}
