using UnityEngine;
using UnityEngine.EventSystems;

public class Red_Node : MonoBehaviour {

    public Color hColor;
    public Color notEnoughMoneyColor;
    public Vector3 positonOffset;

    [Header("Optional")]
    public GameObject turret;
    public TurretBlueprint turretBlueprint;


    private Renderer rend;
    private Color startColor;

    Red_BuildManager buildManger;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManger = Red_BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positonOffset;
    }

    /*
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (buildManger.GetTurretToBuild() == null)
            return;

        if (turret != null)
        {
            Debug.Log("Can't build there! ");
            return;
        }

        GameObject turretToBuild = Red_BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positonOffset, transform.rotation);
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (buildManger.GetTurretToBuild() != null)
        {
            rend.material.color = hColor;
        }
        return;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    */

    public void SellTurret()
    {
        RedPlayerStats.Money += turretBlueprint.GetSellAmount();
        Destroy(turret);
        buildManger.DeselectNode();
    }

    public void onClick()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (turret != null)
        {
            buildManger.SelectNode(this);
            return;
        }

        if (!buildManger.CanBuild)
            return;
        BuildTurret(buildManger.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (RedPlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build turret!!");
            return;
        }

        RedPlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        Debug.Log("Money Left:" + RedPlayerStats.Money);

    }


    public void onSelect()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (buildManger.CanBuild && buildManger.HasMoney)
        {
            rend.material.color = hColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
        return;
    }

    public void onRelease()
    {
        rend.material.color = startColor;
    }
}
