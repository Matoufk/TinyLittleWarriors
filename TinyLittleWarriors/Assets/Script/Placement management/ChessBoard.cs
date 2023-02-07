using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    // The size of the grid cells
    static public int numberOfCellInX = 8;
    static public int numberOfCellInZ = 8;

    // The layer mask to use for the raycast
    public LayerMask layerMask;

    // The prefab to instantiate on the grid
    public GameObject board;

    // The prefab to instantiate on the grid
    public GameObject item;

    // Tiles
    (float, float)[,] tabTile = new (float, float)[numberOfCellInX,numberOfCellInZ];

    // Important coordinates
    [System.NonSerialized] public float originX;
    [System.NonSerialized] public float originZ;

    [System.NonSerialized] public float sizeTileX;
    [System.NonSerialized] public float posY;
    [System.NonSerialized] public float sizeTileZ;

    private void Start()
    {
        originX = board.transform.position.x - (board.GetComponent<Collider>().bounds.size.x / 2);
        originZ = board.transform.position.z - (board.GetComponent<Collider>().bounds.size.z / 2);

        sizeTileX = board.GetComponent<Collider>().bounds.size.x / numberOfCellInX;
        posY = board.transform.position.y + board.GetComponent<Collider>().bounds.size.y;
        sizeTileZ = board.GetComponent<Collider>().bounds.size.z / numberOfCellInZ;

        createBoard();
    }
    


    /// <summary>
    /// Try and separate the two // Make a visual chessboard + drag and drop on center of tile
    /// </summary>
    private void createBoard()
    {
        for (int i = 0; i < numberOfCellInX; i++)
        {
            for(int j = 0; j < numberOfCellInZ;j++)
            {
                tabTile[i,j] = (originX + sizeTileX * i, originZ + sizeTileZ * j);
            }
        }
    }

    private (float,float) getCell(RaycastHit hit)
    {
        (float, float) cell = (originX, originZ);

        for (int i = 0; i < numberOfCellInX; i++)
        {
            for (int j = 0; j < numberOfCellInZ; j++)
            {
                if (hit.point.x >= tabTile[i,j].Item1 &&
                    hit.point.z >= tabTile[i,j].Item2)
                {
                    cell = tabTile[i,j];
                }
                if (hit.point.x <= tabTile[i,j].Item1 &&
                    hit.point.z <= tabTile[i,j].Item2)
                {
                    break;
                }
            }
        }
        return cell;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the mouse button was pressed
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))        
        {

            (float,float) cell = getCell(hit);

            // Calculate the position of the center of the grid cell
            Vector3 cellCenter = new Vector3(cell.Item1 + sizeTileX/2, posY, cell.Item2 + sizeTileZ/2);

            if (Input.GetMouseButtonDown(0))
            {          
                //Drop on left click and drop on left click again

                // Instantiate the prefab at the center of the grid cell
                Instantiate(item, cellCenter, Quaternion.identity);
            }

        }
    }
}
