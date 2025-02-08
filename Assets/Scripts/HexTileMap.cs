using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileMap : MonoBehaviour {

    public static HexTileMap instance;

    [SerializeField]
    public HexTileScript script;

    [SerializeField]
    public TileArraySO[] Roads;

    [SerializeField]
    public TileArraySO AloneBuildingsTiles;

    [SerializeField]
    public TileArraySO SettlementTiles;

    [SerializeField]
    public TileArraySO CityTiles;

    [SerializeField]
    public TileArraySO ForestTiles;

    [SerializeField]
    public TileArraySO MountainTiles;

    [SerializeField]
    public TileArraySO[] arrays;

    public List<GameObject> gameTiles;



    [SerializeField]
    Dictionary<Vector3, HexTileScript> tilesArray = new Dictionary<Vector3, HexTileScript>();

    [SerializeField]
    Dictionary<Vector3, HexTileScript> SettlementsArray = new Dictionary<Vector3, HexTileScript>();


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        tilesArray.Add(Vector3.zero, script);

        //gameTiles = new List<GameObject>(Fields.TileArray);
    }
    private void Start() {


    }

    public void AddHexToDictionary(Vector3 coords, HexTileScript script) {
        tilesArray.Add(coords, script);
    }

    public void RemoveHexFromDictionary(Vector3 coords) {
        tilesArray.Remove(coords);
    }

    public HexTileScript GetHexByCoords(Vector3 coords) {
        try {
            return tilesArray[coords];
        }
        catch (KeyNotFoundException e) {
            Debug.LogWarning("Исключение KeyNotFoundException: " + e.Message);
            return null;
        }
    }

    
    

    public static Vector3[] neighboursDirectionVectors = new Vector3[]{
        new Vector3(1, 0, -1), new Vector3(1, -1, 0),
        new Vector3(0, -1, 1), new Vector3(-1, 0, 1),
        new Vector3(-1, 1, 0), new Vector3(0, 1, -1) };


}
