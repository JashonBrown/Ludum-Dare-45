using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare
{
    public class Tile
    {
        public TileType Type { get; set; }
        public int Health => Tier + (Type == TileType.Wall ? 2 : 1);
        public int Tier { get; set; }

        public Sprite Sprite { get; set; }

        public Tile(TileType type, int tier, Sprite sprite)
        {
            Type = type;
            Tier = tier;
            Sprite = sprite;
        }
    }
}