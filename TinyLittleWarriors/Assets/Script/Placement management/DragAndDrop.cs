using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private GameObject selectedObj;
    int armySize = 8;
    bool armyIsHere;
    [SerializeField] GameObject soldier;
    List<GameObject> orphans = new List<GameObject>();

    float norm = 2f;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(selectedObj == null)
            {
                RaycastHit hit = CastRay();

                if(hit.collider != null)
                {
                    if(!hit.collider.CompareTag("Drag"))
                    {
                        return;
                    }

                    selectedObj = hit.collider.gameObject;
                    selectedObj.GetComponent<Rigidbody>().useGravity = false;
                    foreach (GameObject orphan in orphans)
                    {
                        orphan.GetComponent<Rigidbody>().useGravity = false;
                    }
                    Cursor.visible = false;
                }
            } else {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObj.transform.position).z);
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(position);
                selectedObj.transform.position = new Vector3(worldPos.x, 2f, worldPos.z);

                selectedObj.GetComponent<Rigidbody>().useGravity = true;
                if (!armyIsHere)
                {
                    CreateArmy(armySize);
                    armyIsHere = true;
                }
                foreach (GameObject orphan in orphans)
                {
                    orphan.GetComponent<Rigidbody>().useGravity = true;
                }

                selectedObj = null;
                Cursor.visible = true;
            }
        }
        
        if(selectedObj != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObj.transform.position).z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(position);
            selectedObj.transform.position = new Vector3(worldPos.x, 2f, worldPos.z);
        }
    }

    private RaycastHit CastRay() {

        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);

        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }

    public void CreateArmy(int armysize)
    {
        
        for (int i = 0; i < armysize; i++)
        {
            float angle = ((2 * Mathf.PI) / armysize) * i;
            float normX = norm * Mathf.Cos(angle);
            float normZ = norm * Mathf.Sin(angle);
            Vector3 dupPos = new Vector3(soldier.transform.position.x + normX, soldier.transform.position.y, soldier.transform.position.z + normZ);

            GameObject duplicate = Instantiate(soldier, dupPos, Quaternion.identity);
            duplicate.gameObject.tag = "Untagged";
            orphans.Add(duplicate); 
            duplicate.name = "Soldier" + (i);

            duplicate.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);

            Debug.Log(duplicate.name + " : " + soldier.name);
        }
        foreach (GameObject orphan in orphans)
        {
            orphan.transform.SetParent(soldier.transform);
        }
    }
}