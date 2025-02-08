using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Rendering;
using static UnityEngine.UI.Image;
using System.Drawing;
using TMPro;
using Unity.Mathematics;

public class HexTilemapGenerator : MonoBehaviour {
    [SerializeField]
    public HexTileScript tileScript;
    [SerializeField]
    public GameObject tileCursor;
    [SerializeField]
    GameObject tilePrefab;

    [SerializeField]
    Button[] buttons;

    [SerializeField]
    uint Seed;

    public static HexTilemapGenerator instance;



    // Start is called before the first frame update
    void Start() {

        if (instance == null) {
            instance = this;
        }

        buttons[0].onClick.AddListener(() => {
            if (tileScript.GetNeighbour(NeighboursDirection.Right_top) == null) {
                tileScript.spawnTile(tileScript, NeighboursDirection.Right_top);

                tileScript = tileScript.GetNeighbour(NeighboursDirection.Right_top);
            }
            else {
                tileScript = tileScript.GetNeighbour(NeighboursDirection.Right_top);

            }

            tileScript.SpawnNeighbours(tileScript);
        });

        buttons[1].onClick.AddListener(() => {
            if (tileScript.GetNeighbour(NeighboursDirection.Right) == null) {
                tileScript.spawnTile(tileScript, NeighboursDirection.Right);
                tileScript = tileScript.GetNeighbour(NeighboursDirection.Right);

            }
            else {
                tileScript = tileScript.GetNeighbour(NeighboursDirection.Right);

            }

            tileScript.SpawnNeighbours(tileScript);
        });

        buttons[2].onClick.AddListener(() => {
            if (tileScript.GetNeighbour(NeighboursDirection.Right_bottom) == null) {
                tileScript.spawnTile(tileScript, NeighboursDirection.Right_bottom);
                tileScript = tileScript.GetNeighbour(NeighboursDirection.Right_bottom);

            }
            else {
                tileScript = tileScript.GetNeighbour(NeighboursDirection.Right_bottom);

            }

            tileScript.SpawnNeighbours(tileScript);
        });

        buttons[3].onClick.AddListener(() => {
            if (tileScript.GetNeighbour(NeighboursDirection.Left_bottom) == null) {
                tileScript.spawnTile(tileScript, NeighboursDirection.Left_bottom);
                tileScript = tileScript.GetNeighbour(NeighboursDirection.Left_bottom);

            }
            else {
                tileScript = tileScript.GetNeighbour(NeighboursDirection.Left_bottom);

            }

            tileScript.SpawnNeighbours(tileScript);
        });

        buttons[4].onClick.AddListener(() => {
            if (tileScript.GetNeighbour(NeighboursDirection.Left) == null) {
                tileScript.spawnTile(tileScript, NeighboursDirection.Left);
                tileScript = tileScript.GetNeighbour(NeighboursDirection.Left);

            }
            else {
                tileScript = tileScript.GetNeighbour(NeighboursDirection.Left);

            }

            tileScript.SpawnNeighbours(tileScript);
        });

        buttons[5].onClick.AddListener(() => {
            if (tileScript.GetNeighbour(NeighboursDirection.Left_top) == null) {
                tileScript.spawnTile(tileScript, NeighboursDirection.Left_top);
                tileScript = tileScript.GetNeighbour(NeighboursDirection.Left_top);

            }
            else {
                tileScript = tileScript.GetNeighbour(NeighboursDirection.Left_top);

            }
            
            tileScript.SpawnNeighbours(tileScript);
        });

        tileScript.SpawnNeighbours(tileScript);

        //GreatGeneration();
    }

    public void GreatGeneration() {
        //int braker = 100;
        int maxIteration = 7;
        int currentIteration = 0;
        int currentStep = 0;
        NeighboursDirection direction = NeighboursDirection.Left_top;
        tileScript.spawnTile(tileScript, direction);
        tileScript = tileScript.GetNeighbour(direction);
        direction = NeighboursDirection.Right;
        currentIteration++;
        for (int i = 0; i < maxIteration;) {

            if ((int)direction == 6) {
                direction = NeighboursDirection.Right_top;
            }

            if (direction == NeighboursDirection.Right_top && currentStep >= currentIteration - 1) {

                tileScript = tileScript.GetNeighbour(NeighboursDirection.Right_top);
                direction = NeighboursDirection.Left_top;

                if (currentIteration < maxIteration) {
                    tileScript.spawnTile(tileScript, direction);
                    tileScript = tileScript.GetNeighbour(direction);
                    direction = NeighboursDirection.Right;

                }
                currentStep = 0;
                currentIteration++;
                i++;

            }


            else {
                tileScript.spawnTile(tileScript, direction);
                tileScript = tileScript.GetNeighbour(direction);
                currentStep++;

                if (currentStep >= currentIteration) {
                    direction++;
                    currentStep = 0;
                }
                else {
                    //currentStep++;
                }
            }

        }

    }

    public static void spawnTileByCoords(GameObject tile, Vector3 origin) {
                                                     // Vector3 coords = origin;
        //Vector3 origin = new Vector3(int.Parse(posX.text), int.Parse(posY.text), int.Parse(posZ.text));
        Debug.Log(origin);
        //Vector3 coords = origin;
        Vector3 targetPosition = Vector3.zero;


        targetPosition.x = (origin.x + origin.z / 2) * (math.sqrt(3) * 2.887f);
        targetPosition.z = (origin.x + origin.y) * (0.75f * 2.887f*2);

        if (HexTileMap.instance.GetHexByCoords(origin) == null) {
            Debug.Log("Tile not Present");

            //GameObject newTile = Instantiate(getRandomTile(calculateSideQuantity(getNeighboursSides(coords)), getNeighboursSides(coords)), targetPosition, quaternion.identity, null);
            GameObject newTile = Instantiate(tile, targetPosition, quaternion.identity, null);
            HexTileScript newTileHexScript = newTile.GetComponent<HexTileScript>();

            newTileHexScript.setCoords(origin);

            HexTileMap.instance.AddHexToDictionary(origin, newTileHexScript);
            newTileHexScript.checkNeighbours(origin);

        }
        else {
            Debug.Log("tile on coords " + origin.ToString() + "  is instatiated");
        }
    }
}
