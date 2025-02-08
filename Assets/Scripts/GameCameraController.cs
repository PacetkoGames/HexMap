using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class GameCameraController : MonoBehaviour {

    [SerializeField]
    CinemachineVirtualCamera _VirtualCamera;
    [SerializeField]
    Camera _Camera;

    [SerializeField]
    InputActionReference LeftClickAction;
    [SerializeField]
    InputActionReference RightClickAction;

    


    private void Start() {
        LeftClickAction.action.started += OnLeftClick;
        RightClickAction.action.started += OnRightClick;
    }


    public void OnRightClick(InputAction.CallbackContext context) {
        Debug.Log("OnRightClick execute ");
        changeCameraToTile(getHexTile());

    }

    

    public void OnLeftClick(InputAction.CallbackContext context) {
        switch (HexTilemapEditor.editorState) {
            case EditorState.Empty:
                break;
            case EditorState.SpawnTiles:
                if (context.started) {
                    HexTileScript tile = getHexTile();
                    if (tile != null) {
                        HexTilemapGenerator.instance.tileScript = tile;
                        HexTilemapGenerator.instance.tileScript.SpawnNeighbours(tile);
                    }
                }
                break;
            case EditorState.PlaceRoad:
                if (context.started) {
                    HexTilemapEditor.PlaceRoad(getHexTile());
                }
                break;
        }
        

    }
        
    public HexTileScript getHexTile() {
        Debug.Log("getHexTile execute ");
        RaycastHit hit;
        if (Physics.Raycast(_Camera.ScreenPointToRay(Input.mousePosition), out hit, 100f)) {
            return hit.collider.GetComponentInParent<HexTileScript>();
        }
        else return null;
    }

    public void changeCameraToTile(HexTileScript tile) {
        if (tile == null) {
            return;
        }
        _VirtualCamera.Follow = tile.transform;
        //_vCamera.LookAt = tile.transform;
    }

}

