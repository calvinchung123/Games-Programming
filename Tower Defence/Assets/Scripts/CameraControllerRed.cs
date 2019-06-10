using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerRed : MonoBehaviour
{

    private bool activeMove = true;

    public float panSpeed = 40f;

    public float scrollSpeed = 5f;
    public float minZoom = 80f;
    public float maxZoom = 200f;
    public GameObject cursor;

    public GameObject nodeSelected;
    public GameObject lastNodeHit = null;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameEnded)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            activeMove = !activeMove;

        if (!activeMove)
            return;

        //move up
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        //move down
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        //move right
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        //move left
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

       

        //restrict moving space
        transform.position = new Vector3(transform.position.x, transform.position.y, (Mathf.Clamp(transform.position.z, 142, 400)));
        transform.position = new Vector3((Mathf.Clamp(transform.position.x, 0, 120)), transform.position.y, transform.position.z);

        //zooming
        Vector3 pos = transform.position;
        if (Input.GetKey("o") )
        {
            pos.y += 30 * scrollSpeed * Time.deltaTime;
        }

        if (Input.GetKey("p"))
        {
            pos.y -= 30 * scrollSpeed * Time.deltaTime;
        }

        //restrict zooming
        pos.y = Mathf.Clamp(pos.y, minZoom, maxZoom);
        transform.position = pos;


        
    }

    void UpdateTarget()
    {
        shootRay();
    }

    void shootRay()
    {
        RaycastHit nodeHit;

        if (Physics.Raycast(cursor.transform.position, cursor.transform.forward, out nodeHit))
        {

            if (nodeHit.collider.gameObject.tag == "RedNode")
            {
                nodeSelected = nodeHit.collider.gameObject;

                if (lastNodeHit == null)
                {
                    lastNodeHit = nodeSelected;
                }
                else if (nodeSelected != lastNodeHit)
                {
                    lastNodeHit.GetComponent<Red_Node>().onRelease();
                    lastNodeHit = nodeSelected;
                }

                nodeSelected.GetComponent<Red_Node>().onSelect();
                if (Input.GetKey("n"))
                {
                    lastNodeHit.GetComponent<Red_Node>().onClick();
                }
                if (Input.GetKey("m"))
                {
                    if (lastNodeHit.GetComponent<Red_Node>().turret != null)
                    {
                        lastNodeHit.GetComponent<Red_Node>().SellTurret();
                    }
                }
            }
        }
    }
}
