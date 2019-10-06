using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare
{
    [Serializable]
    public class Raft
    {
        [BoxGroup("Two Dimensional array without the TableMatrix attribute.")]
        public Tile[,] Tiles { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Raft(int width, int height)
        {
            // Store
            Width = width;
            Height = height;

            // Populate
            Tiles = new Tile[width, height];
        }
    }
}