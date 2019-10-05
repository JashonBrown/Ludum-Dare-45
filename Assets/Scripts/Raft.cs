using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare
{
    public class Raft
    {
        public Tile[,] Tiles { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Raft(int width, int height)
        {
            // Store
            Width = width;
            Height = height;

            // Populate
            Tiles = new Tile[width,height];
        }
    }
}