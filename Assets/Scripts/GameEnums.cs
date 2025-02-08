using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NeighboursDirection {
    Right_top,
    Right,
    Right_bottom,
    Left_bottom,
    Left,
    Left_top
}

public enum SideType {
    Default,
    Road,
    Wall,
    Settlement,
    City,
    Forest,
    Mountain,
    River,
    Sea,
    Universal,
    None
}

public enum BiomeType {
    PlainLand,
    WoodLand,
    AshLand,
    SandLand,
    SnowLand,
    SeaLand,
    Sea
}

public enum EditorState {
    Empty,
    SpawnTiles,
    PlaceRoad
}
