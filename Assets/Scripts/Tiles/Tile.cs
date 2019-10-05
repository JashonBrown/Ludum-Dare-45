using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare
{
    [CreateAssetMenu(menuName = "Tile")]
    public class Tile : ScriptableObject
    {
        public TileType Type;
        public Sprite Sprite;
        public int Health;
    }
}