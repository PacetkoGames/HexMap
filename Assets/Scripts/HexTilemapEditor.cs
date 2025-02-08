using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HexTilemapEditor;

public class HexTilemapEditor : MonoBehaviour {

    [SerializeField]
    public static EditorState editorState = EditorState.SpawnTiles;

    public void SetEditorToNoneMode() {
        editorState = EditorState.Empty;
    }
    public void SetEditorToTilePlaceMode() {
        editorState = EditorState.SpawnTiles;
    }

    public void SetEditorToRoadPlaceMode() {
        editorState = EditorState.PlaceRoad;
    }


    public static void PlaceRoad(HexTileScript originHex) {
        if (originHex != null) {
            Vector3 coords = originHex.GetCoords();
            HexTileScript[] neighbours = HexTileScript.GetNeighbours(coords);

            int roadQ = 0;
            for (int i = 0; i < 6; i++) {
                if (neighbours[i]!=null && neighbours[i].GlobalType == SideType.Road) {
                    roadQ++;
                }
            }

            originHex.GlobalType = SideType.Road;
            originHex.Sides = new SideType[6] { SideType.Road, SideType.Road, SideType.Road, SideType.Road, SideType.Road, SideType.Road };
            for (int i = 0; i < 6; i++) {
                if (neighbours[i] != null && neighbours[i].GlobalType == SideType.Road) {
                    Vector3 coord = neighbours[i].GetCoords();
                    neighbours[i].DestroyHex();
                    HexTilemapGenerator.spawnTileByCoords(HexTileScript.getRandomTile(HexTileScript.calculateSideQuantity(HexTileScript.getNeighboursSides(coord)), HexTileScript.getNeighboursSides(coord)), coord);
                }
            }
            originHex.DestroyHex();

            switch (roadQ) {
                case 0:
                    HexTilemapGenerator.spawnTileByCoords(HexTileMap.instance.MountainTiles.TileArray[0], coords);
                    break;
                case 1:
                    
                    HexTilemapGenerator.spawnTileByCoords(HexTileScript.getRandomTile(HexTileScript.calculateSideQuantity(HexTileScript.getNeighboursSides(coords)), HexTileScript.getNeighboursSides(coords), new List<TileArraySO>{ HexTileMap.instance.Roads[0]}), coords);
                    break;
                case 2:
                    HexTilemapGenerator.spawnTileByCoords(HexTileScript.getRandomTile(HexTileScript.calculateSideQuantity(HexTileScript.getNeighboursSides(coords)), HexTileScript.getNeighboursSides(coords), new List<TileArraySO> { HexTileMap.instance.Roads[1] }), coords);
                    break;
                case 3:
                    HexTilemapGenerator.spawnTileByCoords(HexTileScript.getRandomTile(HexTileScript.calculateSideQuantity(HexTileScript.getNeighboursSides(coords)), HexTileScript.getNeighboursSides(coords), new List<TileArraySO> { HexTileMap.instance.Roads[2] }), coords);
                    break;
                case 4:
                    HexTilemapGenerator.spawnTileByCoords(HexTileScript.getRandomTile(HexTileScript.calculateSideQuantity(HexTileScript.getNeighboursSides(coords)), HexTileScript.getNeighboursSides(coords), new List<TileArraySO> { HexTileMap.instance.Roads[3] }), coords);
                    break;
                case 5:
                    HexTilemapGenerator.spawnTileByCoords(HexTileScript.getRandomTile(HexTileScript.calculateSideQuantity(HexTileScript.getNeighboursSides(coords)), HexTileScript.getNeighboursSides(coords), new List<TileArraySO> { HexTileMap.instance.Roads[4] }), coords);
                    break;
                case 6:
                    HexTilemapGenerator.spawnTileByCoords(HexTileScript.getRandomTile(HexTileScript.calculateSideQuantity(HexTileScript.getNeighboursSides(coords)), HexTileScript.getNeighboursSides(coords), new List<TileArraySO> { HexTileMap.instance.Roads[5] }), coords);
                    break;
                }
        }


    }

}
