using UnityEngine;

public class Red_BuildManager : MonoBehaviour {

    public static Red_BuildManager instance;

    void Awake()
    { 
        instance = this;
    }

    public GameObject redTurretPrefab;
    public GameObject redMissilePrefab;
    public GameObject redLaserPrefab;

    private TurretBlueprint turretToBuild;
    private Red_Node selectedNode;

    public Red_NodeUI nodeUI;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return RedPlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn (Red_Node node)
    {
        if(RedPlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build turret!!");
            return;
        }

        RedPlayerStats.Money -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Money Left:" + RedPlayerStats.Money);
    }

    public void SelectNode(Red_Node node)
    {

        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;

        nodeUI.SetTarget(node);

    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectRedTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
