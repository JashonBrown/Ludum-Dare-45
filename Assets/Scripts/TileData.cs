using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare
{
    [CreateAssetMenu(menuName = "Tile Data")]
    public class TileData : ScriptableObject
    {
        public TileType Type;
        public Sprite[] Sprites;
    }
}