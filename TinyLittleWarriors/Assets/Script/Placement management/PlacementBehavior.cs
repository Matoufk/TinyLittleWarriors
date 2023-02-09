using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementBehavior : MonoBehaviour
{
    /// <summary>
    /// Drag And Drop
    /// </summary>
    private GameObject selectedObj;
    int armySize = 8;

    GameObject soldier;
    List<GameObject> orphans = new List<GameObject>();

    float norm = 4f;

    /// <summary>
    /// Placement Grid
    /// </summary>
    // The size of the grid cells
    static public int numberOfCellInX = 40;
    static public int numberOfCellInZ = 15;

    public LayerMask layerMask;
    public GameObject board;

    public bool battle = false;
    public List<GameObject> army = new List<GameObject>();

    // Tiles
    (float, float, bool)[,] tabTile = new (float, float, bool)[numberOfCellInX, numberOfCellInZ];

    // Important coordinates
    [System.NonSerialized] public float originX;
    [System.NonSerialized] public float originZ;

    [System.NonSerialized] public float sizeTileX;
    [System.NonSerialized] public float posY;
    [System.NonSerialized] public float sizeTileZ;

    Color color = new Color(1, 0, 0, 1);

    // Start is called before the first frame update
    void Start()
    {
        originX = board.transform.position.x - (board.GetComponent<Collider>().bounds.size.x / 2);
        originZ = board.transform.position.z - (board.GetComponent<Collider>().bounds.size.z / 2);

        sizeTileX = board.GetComponent<Collider>().bounds.size.x / numberOfCellInX;
        sizeTileZ = board.GetComponent<Collider>().bounds.size.z / numberOfCellInZ;

        posY = board.transform.position.y + board.GetComponent<Collider>().bounds.size.y + 2f;

        createBoard();
    }

    // Update is called once per frame
    void Update()
    {
        //Army creation
        if (battle == true)
        {
            foreach (GameObject e in army)
            {
                CreateArmy(armySize, e);
                e.tag = "Ally";
                e.GetComponent<AgentBehavior>().setState(AgentBehavior.AgentFSM.Seek);
                
            }

            foreach(GameObject i in orphans)
            {
                i.GetComponent<AgentBehavior>().setState(AgentBehavior.AgentFSM.Seek);
            }
            Destroy(this);
        }
        //battle = false;

        RaycastHit hitGround = CastRayGrid();
        (int, int) cellCoord = getCellCoord(hitGround);
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObj == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Drag"))
                    {
                        return;
                    }
                    
                    tabTile[cellCoord.Item1,cellCoord.Item2].Item3 = true;
                    selectedObj = hit.collider.gameObject;
                    selectedObj.GetComponent<Rigidbody>().useGravity = false;
                    selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    selectedObj.GetComponent<Collider>().enabled = false;

                    if (army.Contains(selectedObj) == false)
                    {
                        army.Add(selectedObj);
                    }

                    for (int i = 2; i < selectedObj.transform.childCount; i++)
                    {
                        selectedObj.transform.GetChild(i).GetComponent<Rigidbody>().useGravity = false;
                        selectedObj.transform.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        selectedObj.transform.GetChild(i).GetComponent<Collider>().enabled = false;
                    }
                    Cursor.visible = false;
                }
            }
            else if (tabTile[cellCoord.Item1, cellCoord.Item2].Item3 == true)
            {
                
                // Calculate the position of the center of the grid cell
                Vector3 cellCenter = new Vector3(tabTile[cellCoord.Item1, cellCoord.Item2].Item1 + sizeTileX / 2, posY + 1f, tabTile[cellCoord.Item1, cellCoord.Item2].Item2 + sizeTileZ / 2);

                //Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObj.transform.position).z);
                //Vector3 worldPos = Camera.main.ScreenToWorldPoint(position);
                //selectedObj.transform.position = new Vector3(worldPos.x, 2f, worldPos.z);
                selectedObj.transform.position = cellCenter;
                selectedObj.GetComponent<Rigidbody>().useGravity = true;
                selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                selectedObj.GetComponent<Collider>().enabled = true;
                tabTile[cellCoord.Item1, cellCoord.Item2].Item3 = false;

                
                for(int i = 2; i < selectedObj.transform.childCount; i++)
                {
                    selectedObj.transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
                    selectedObj.transform.GetChild(i).GetComponent<Collider>().enabled = true;
                    selectedObj.transform.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                }

                selectedObj = null;
                Cursor.visible = true;
            }
        }

        if (selectedObj != null)
        {
            DrawOnFloor(hitGround);
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObj.transform.position).z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(position); 
            selectedObj.transform.position = new Vector3(worldPos.x, posY + 1f, worldPos.z);
        }
    }

    /// <summary>
    /// Draw on the reference tile
    /// </summary>
    private void DrawOnFloor(RaycastHit hit)
    {
        (int, int) cellCoord = getCellCoord(hit);
        (float, float, bool) cell = tabTile[cellCoord.Item1, cellCoord.Item2];
        Debug.DrawLine(new Vector3(cell.Item1 + 0.1f, posY, cell.Item2 + 0.1f)
            , new Vector3(cell.Item1 + sizeTileX - 0.1f, posY, cell.Item2 + sizeTileZ - 0.1f), color);

        Debug.DrawLine(new Vector3(cell.Item1 + sizeTileX - 0.1f, posY, cell.Item2 + 0.1f)
            , new Vector3(cell.Item1 + 0.1f, posY, cell.Item2 + sizeTileZ - 0.1f), color);
    }

    /// <summary>
    /// Create the placement Grid
    /// </summary>
    private void createBoard()
    {
        for (int i = 0; i < numberOfCellInX; i++)
        {
            for (int j = 0; j < numberOfCellInZ; j++)
            {
                tabTile[i, j] = (originX + sizeTileX * i, originZ + sizeTileZ * j, true);
            }
        }
    }

    /// <summary>
    /// get the cell hit by the ray
    /// </summary>
    /// <param name="hit"> the RaycastHit</param>
    /// <returns></returns>
    private (int, int) getCellCoord(RaycastHit hit)
    {
        (int, int) coord = (0, 0);

        for (int i = 0; i < numberOfCellInX; i++)
        {
            for (int j = 0; j < numberOfCellInZ; j++)
            {
                if (hit.point.x >= tabTile[i, j].Item1 &&
                    hit.point.z >= tabTile[i, j].Item2)
                {
                    coord = (i, j);
                }
                if (hit.point.x <= tabTile[i, j].Item1 &&
                    hit.point.z <= tabTile[i, j].Item2)
                {
                    break;
                }
            }
        }
        return coord;
    }

    /// <summary>
    /// Cast the ray
    /// </summary>
    /// <returns>
    /// The hit
    /// </returns>
    private RaycastHit CastRay()
    {

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

    private RaycastHit CastRayGrid()
    {

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
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit, layerMask);

        return hit;
    }

    /// <summary>
    /// Duplicate the selected object armysize time around him
    /// </summary>
    /// <param name="armysize"> the size of the army</param>
    public void CreateArmy(int armysize,GameObject selectedObj_)
    {
        
            for (int i = 0; i < armysize; i++)
            {
                float angle = ((2 * Mathf.PI) / armysize) * i;
                float normX = norm * Mathf.Cos(angle);
                float normZ = norm * Mathf.Sin(angle);
                Vector3 dupPos = new Vector3(selectedObj_.transform.position.x + normX, selectedObj_.transform.position.y, selectedObj_.transform.position.z + normZ);

                GameObject duplicate = Instantiate(selectedObj_, dupPos, Quaternion.identity);
                duplicate.gameObject.tag = "Ally";
                duplicate.GetComponent<CharacterStats>().setLife(Mathf.RoundToInt(selectedObj_.GetComponent<CharacterStats>().getMaxLife() * 0.7f));
                duplicate.GetComponent<CharacterStats>().setAttack(Mathf.RoundToInt(selectedObj_.GetComponent<CharacterStats>().getAttack() * 0.5f));
                orphans.Add(duplicate);
                duplicate.name = "Soldier" + (i);

                duplicate.transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f);
            }
        
    }
}
