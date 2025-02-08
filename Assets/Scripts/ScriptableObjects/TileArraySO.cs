using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TileArraySO", fileName = "TileArraySO", order = 1)]
public class TileArraySO : ScriptableObject {

    public List<GameObject> TileArray;

    public void shuffleDeck(TileArraySO deck) {
        System.Random rng = new System.Random();

        int n = deck.TileArray.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            GameObject value = deck.TileArray[k];
            deck.TileArray[k] = deck.TileArray[n];
            deck.TileArray[n] = value;

        }
    }
}
