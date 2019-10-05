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
        public TileData[,] Tiles { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Raft(int width, int height)
        {
            // Store
            Width = width;
            Height = height;

            // Populate
            Tiles = new TileData[width, height];

            Tiles[0, 0] = DataManager.Instance.WallTier1;
            Tiles[0, 1] = DataManager.Instance.WallTier1;
            Tiles[0, 2] = DataManager.Instance.WallTier1;
            Tiles[0, 3] = DataManager.Instance.WallTier1;
            Tiles[0, 4] = DataManager.Instance.WallTier1;
            Tiles[0, 5] = DataManager.Instance.WallTier1;

        }
    }
}