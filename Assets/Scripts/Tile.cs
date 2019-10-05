using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare
{
    public class Tile
    {
        public TileType Type { get; set; }
        public int Health { get; set; }
        public Sprite Sprite { get; set; }

        public Tile(TileType type, int health, Sprite sprite)
        {
            Type = type;
            Health = health;
            Sprite = sprite;
        }
    }
}