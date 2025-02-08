using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System.Drawing;
using UnityEditor.Experimental.GraphView;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

public class HexTileScript : MonoBehaviour {



    [SerializeField]
    int TileID;

    //[SerializeField]
    //TileSO tileSO;

    [SerializeField]
    Vector3 coords = Vector3.zero;

    [SerializeField]
    float size = 2.887f;

    [SerializeField]
    Transform TileVisual;

    [SerializeField]
    bool IsAbsoluteSimetric;

    [SerializeField]
    HexTileScript[] neighbours = new HexTileScript[6];

    [SerializeField]
    SideType[] sides;

    [SerializeField]
    SideType globalType;

    [SerializeField]
    BiomeType biome;

    

    public Vector3 Coords { get => Coords; set => Coords = value; }
    public SideType[] Sides { get => sides; set => sides = value; }
    public SideType GlobalType { get => globalType; set => globalType = value; }
    public BiomeType Biome { get => biome; set => biome = value; }
    public bool IsAbsoluteSimetric1 { get => IsAbsoluteSimetric; set => IsAbsoluteSimetric = value; }
    public float Size { get => size; set => size = value; }

    public void setCoords(Vector3 coord) {
        coords = coord;
    }
    public Vector3 GetCoords( ) {
        return coords;
    }
    public void spawnTile(HexTileScript startTile, NeighboursDirection direction) {//, GameObject tile
        Vector3 origin = startTile.transform.position;
        Vector3 targetPosition = new Vector3(0, 0, 0);
        Vector3 coords = startTile.coords;
        //Debug.Log("Spawning tile...");
        switch (direction) {
            case NeighboursDirection.Right_top:
                targetPosition.x = origin.x + math.sqrt(3) * Size / 2;
                targetPosition.z = origin.z + (0.75f * Size * 2);
                coords += HexTileMap.neighboursDirectionVectors[0];
                break;

            case NeighboursDirection.Right:
                targetPosition.x = origin.x + math.sqrt(3) * Size;
                targetPosition.z = origin.z;
                coords += HexTileMap.neighboursDirectionVectors[1];
                break;
            case NeighboursDirection.Right_bottom:
                targetPosition.x = origin.x + math.sqrt(3) * Size / 2;
                targetPosition.z = origin.z - (0.75f * Size * 2);
                coords += HexTileMap.neighboursDirectionVectors[2];
                break;

            case NeighboursDirection.Left_bottom:
                targetPosition.x = origin.x - math.sqrt(3) * Size / 2;
                targetPosition.z = origin.z - (0.75f * Size * 2);
                coords += HexTileMap.neighboursDirectionVectors[3];

                break;
            case NeighboursDirection.Left:
                targetPosition.x = origin.x - math.sqrt(3) * Size;
                targetPosition.z = origin.z;
                coords += HexTileMap.neighboursDirectionVectors[4];

                break;

            case NeighboursDirection.Left_top:
                targetPosition.x = origin.x - math.sqrt(3) * Size / 2;
                targetPosition.z = origin.z + (0.75f * Size * 2);
                coords += HexTileMap.neighboursDirectionVectors[5];
                break;
        }


        if (HexTileMap.instance.GetHexByCoords(coords) == null) {
            //Debug.Log("Tile not Present");

            GameObject newTile = Instantiate(getRandomTile(calculateSideQuantity(getNeighboursSides(coords)), getNeighboursSides(coords)), targetPosition, quaternion.identity, null);
            //GameObject newTile = Instantiate(HexTileMap.instance.MountainTiles.TileArray[2], targetPosition, quaternion.identity, null);
            HexTileScript newTileHexScript = newTile.GetComponent<HexTileScript>();

            newTileHexScript.coords = coords;

            if (newTileHexScript.IsAbsoluteSimetric) {
                newTileHexScript.TileVisual.transform.Rotate(new Vector3(0,60*(coords.x* coords.y+coords.z),0));
            }
            //Debug.Log(newTileHexScript.coords.ToString() + " is added to dictionary");
            HexTileMap.instance.AddHexToDictionary(coords, newTileHexScript);
            //Debug.Log(HexTileMap.instance.GetHexByCoords(coords) + " hex tile");
            newTileHexScript.checkNeighbours(newTileHexScript.coords);
            //PairNeighbours(startTile, newTileHexScript, direction);
        }
        else {
            Debug.Log("tile on coords " + coords.ToString() + "  is instatiated");
        }



    }

    

    public void localToWorldCoord(Vector3 coord) {
        Debug.Log("Coord x" + (coord.x + coord.z / 2) * (math.sqrt(3) * Size));
        Debug.Log("Coord y" + (coord.x + coord.y) * (0.75f * Size));
    }

    public void placeTile(HexTileScript startTile, NeighboursDirection direction) {
        Vector3 origin = startTile.transform.position;
        Vector3 targetPosition = new Vector3(0, 0, 0);
        Vector3 coords = startTile.coords;
        Debug.Log("Spawning tile...");
        switch (direction) {
            case NeighboursDirection.Right_top:
                targetPosition.x = origin.x + math.sqrt(3) * Size / 2;
                targetPosition.z = origin.z + (0.75f * Size * 2);
                coords += HexTileMap.neighboursDirectionVectors[0];
                break;

            case NeighboursDirection.Right:
                targetPosition.x = origin.x + math.sqrt(3) * Size;
                targetPosition.z = origin.z;
                coords += HexTileMap.neighboursDirectionVectors[1];
                break;
            case NeighboursDirection.Right_bottom:
                targetPosition.x = origin.x + math.sqrt(3) * Size / 2;
                targetPosition.z = origin.z - (0.75f * Size * 2);
                coords += HexTileMap.neighboursDirectionVectors[2];
                break;

            case NeighboursDirection.Left_bottom:
                targetPosition.x = origin.x - math.sqrt(3) * Size / 2;
                targetPosition.z = origin.z - (0.75f * Size * 2);
                coords += HexTileMap.neighboursDirectionVectors[3];

                break;
            case NeighboursDirection.Left:
                targetPosition.x = origin.x - math.sqrt(3) * Size;
                targetPosition.z = origin.z;
                coords += HexTileMap.neighboursDirectionVectors[4];

                break;

            case NeighboursDirection.Left_top:
                targetPosition.x = origin.x - math.sqrt(3) * Size / 2;
                targetPosition.z = origin.z + (0.75f * Size * 2);
                coords += HexTileMap.neighboursDirectionVectors[5];
                break;
        }

        if (HexTileMap.instance.GetHexByCoords(coords) == null) {
            Debug.Log("Tile not Present");

            GameObject newTile = Instantiate(HexTileMap.instance.gameTiles[0], targetPosition, quaternion.identity, null);
            HexTileScript newTileHexScript = newTile.GetComponent<HexTileScript>();
            newTileHexScript.coords = coords;

            Debug.Log(newTileHexScript.coords.ToString() + " is added to dictionary");
            HexTileMap.instance.AddHexToDictionary(coords, newTileHexScript);
            Debug.Log(HexTileMap.instance.GetHexByCoords(coords) + " hex tile");
            newTileHexScript.checkNeighbours(newTileHexScript.coords);
            Debug.Log("===============================================");
            HexTileMap.instance.gameTiles.RemoveAt(0);
            Debug.Log("===============================================");
            //PairNeighbours(startTile, newTileHexScript, direction);

        }
        else {
            Debug.Log("tile on coords " + coords.ToString() + "  is instatiated");
        }



    }


    public static SideType[] getNeighboursSides(Vector3 coord) {
        SideType[] neighboursSides = new SideType[6] { SideType.None, SideType.None, SideType.None, SideType.None, SideType.None, SideType.None };

        for (int i = 0; i < 6; i++) {
            if (i < 3) {
                if (HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[i]) != null) {
                    Debug.Log(i);
                    neighboursSides[i] = HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[i]).GetSideType(i + 3);
                }
            }
            else {
                if (HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[i]) != null) {
                    Debug.Log(i);
                    neighboursSides[i] = HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[i]).GetSideType(i - 3);
                }
            }


        }

        Debug.LogWarning(neighboursSides.ToString());
        return neighboursSides;
    }

    public static int[] calculateSideQuantity(SideType[] sides) {
        int[] quantity = new int[11];
        for (int i = 0; i < sides.Length; i++) {
            quantity[(int)sides[i]] += 1;
        }
        Debug.LogWarning(quantity[0].ToString());
        return quantity;
    }

    public static GameObject getRandomTile(int[] sidesQuantity, SideType[] sides, List<TileArraySO> array = null) {
        List<TileArraySO> tileArraySOs = new List<TileArraySO>();
        if (array == null) {
            
            if (sidesQuantity[(int)SideType.Default] != 0) {
                tileArraySOs.Add(HexTileMap.instance.MountainTiles);
                tileArraySOs.Add(HexTileMap.instance.Roads[0]);
            }
            if (sidesQuantity[(int)SideType.Road] != 0) {
                if (sidesQuantity[(int)SideType.Road] == 1) {
                    //tileArraySOs.Add(HexTileMap.instance.Roads[0]);
                    tileArraySOs.Add(HexTileMap.instance.Roads[1]);
                }
                if (sidesQuantity[(int)SideType.Road] == 2) {
                    tileArraySOs.Add(HexTileMap.instance.Roads[1]);
                    tileArraySOs.Add(HexTileMap.instance.Roads[2]);
                }
                if (sidesQuantity[(int)SideType.Road] == 3) {
                    tileArraySOs.Add(HexTileMap.instance.Roads[2]);
                    tileArraySOs.Add(HexTileMap.instance.Roads[3]);
                }
                if (sidesQuantity[(int)SideType.Road] == 4) {
                    tileArraySOs.Add(HexTileMap.instance.Roads[3]);
                    tileArraySOs.Add(HexTileMap.instance.Roads[4]);
                }
                if (sidesQuantity[(int)SideType.Road] == 5) {
                    tileArraySOs.Add(HexTileMap.instance.Roads[4]);
                    tileArraySOs.Add(HexTileMap.instance.Roads[5]);
                }
                if (sidesQuantity[(int)SideType.Road] == 6) {
                    tileArraySOs.Add(HexTileMap.instance.Roads[5]);
                }


            }
        }
        else {
            tileArraySOs = array;
        }

        List<GameObject> validHexList = new List<GameObject>();
        for (int i = 0; i < tileArraySOs.Count; i++) {
            for (int j = 0; j < tileArraySOs[i].TileArray.Capacity; j++) {
                if (compareSides(tileArraySOs[i].TileArray[j].GetComponent<HexTileScript>().Sides, sides)) {
                    validHexList.Add(tileArraySOs[i].TileArray[j]);
                }
            }
        }
        UnityEngine.Random.InitState(Time.frameCount + (int)Time.time + validHexList.Count);
        return validHexList[UnityEngine.Random.Range(0, validHexList.Count)];
    }

    public static bool compareSides(SideType[] hexSides, SideType[] neighboursSides) {
        for (int i = 0; i < neighboursSides.Length; i++) {

            if (neighboursSides[i] == SideType.None) {
                continue;
            }

            else if (hexSides[i] != neighboursSides[i]) {
                return false;
            }
        }

        return true;
    }

    public void checkNeighbours(Vector3 coord) {
        for (int i = 0; i < 6; i++) {

            if (i < 3) {
                if (HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[i]) != null) {
                    //Debug.Log(i);
                    this.neighbours[i] = HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[i]);
                    HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[i]).SetNeighbour(this, (NeighboursDirection)i + 3);
                }
            }
            else {
                if (HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[i]) != null) {
                    //Debug.Log(i);
                    this.neighbours[i] = HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[i]);
                    HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[i]).SetNeighbour(this, (NeighboursDirection)i - 3);
                }
            }

        }

    //    if (HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[0]) != null) {
    //        Debug.Log("0");
    //        this.neighbours[0] = HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[0]);
    //        HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[0]).SetNeighbour(this, NeighboursDirection.Left_bottom);
    //    }
    //    if (HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[1]) != null) {
    //        Debug.Log("1");
    //        this.neighbours[1] = HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[1]);
    //        HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[1]).SetNeighbour(this, NeighboursDirection.Left);
    //    }
    //    if (HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[2]) != null) {
    //        Debug.Log("2");
    //        this.neighbours[2] = HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[2]);
    //        HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[2]).SetNeighbour(this, NeighboursDirection.Left_top);
    //    }
    //    if (HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[3]) != null) {
    //        Debug.Log("3");
    //        this.neighbours[3] = HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[3]);
    //        HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[3]).SetNeighbour(this, NeighboursDirection.Right_top);
    //    }
    //    if (HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[4]) != null) {
    //        Debug.Log("4");
    //        this.neighbours[4] = HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[4]);
    //        HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[4]).SetNeighbour(this, NeighboursDirection.Right);
    //    }
    //    if (HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[5]) != null) {
    //        Debug.Log("5");
    //        this.neighbours[5] = HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[5]);
    //        HexTileMap.instance.GetHexByCoords(coord + HexTileMap.neighboursDirectionVectors[5]).SetNeighbour(this, NeighboursDirection.Right_bottom);
    //    }
    }



    public void SpawnNeighbours(HexTileScript tile) {

        for (int i = 0; i < 6; i++) {
            if (tile.neighbours[i] == null) {
                spawnTile(tile, (NeighboursDirection)i);
            }
        }
        HexTilemapGenerator.instance.tileCursor.transform.position = this.gameObject.transform.position;
    }

    public void PlaceNeighbours(HexTileScript tile) {
        for (int i = 0; i < 6; i++) {
            if (tile.neighbours[i] == null) {
                placeTile(tile, (NeighboursDirection)i);
            }
        }

    }

    public void RecursiveSpawn() {

    }

    public static void PairNeighbours(HexTileScript startTile, HexTileScript secondTile, NeighboursDirection direction) {
        startTile.SetNeighbour(secondTile, direction);
        Debug.Log(direction);
        if ((int)direction <= 2) direction += 3;
        else direction -= 3;
        Debug.Log(direction);
        secondTile.SetNeighbour(startTile, direction);
    }

    public void SetNeighbour(HexTileScript neighbour, NeighboursDirection direction) {
        this.neighbours[(int)direction] = neighbour;
    }

    public HexTileScript GetNeighbour(NeighboursDirection direction) {
        return neighbours[(int)direction];
    }

    public HexTileScript GetNeighbour(HexTileScript Tile, NeighboursDirection direction) {
        return Tile.GetNeighbour(direction);
    }

    public static HexTileScript[] GetNeighbours(Vector3 coords) {
        HexTileScript[] neighbours = new HexTileScript[6] { null, null, null, null, null, null };
        for (int i = 0; i < 6; i++) {
            if (HexTileMap.instance.GetHexByCoords(coords + HexTileMap.neighboursDirectionVectors[i]) != null) {
                neighbours[i] = HexTileMap.instance.GetHexByCoords(coords + HexTileMap.neighboursDirectionVectors[i]);
            }
        }
       
        return neighbours;
    }

    public SideType GetSideType(int index) {
        return this.Sides[index];
    }
    public static SideType GetSideType(HexTileScript tile, int index) {
        return tile.GetSideType(index);
    }

    public void DestroyHex() {
        HexTileMap.instance.RemoveHexFromDictionary(coords);
        Destroy(this.gameObject);


    }
}
