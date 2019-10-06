﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare
{
    public class ShipEditorButton : MonoBehaviour
    {
        public Image image;
        public int indexI;
        public int indexJ;

        public void Init(int i, int j)
        {
            // Cache
            image = GetComponent<Image>();
            indexI = i;
            indexJ = j;
            _UpdateVisual();

            // On click
            GetComponent<Button>().onClick.AddListener(delegate
            {
                // Simplify
                TileData selectedTile = ShipEditorController.Instance.selectedTile;
                Tile currentTile = DataManager.Instance.PlayerRaft.Tiles[i, j];

                // Increment walls tier?
                if (selectedTile.Type == TileType.Wall && currentTile != null && currentTile.Type == TileType.Wall)
                {
                    // Don't upgrade
                    if (currentTile.Tier == 2) return;

                    // Upgrade wall tier
                    currentTile.Tier++;
                    currentTile.Sprite = selectedTile.Sprites[currentTile.Tier];
                }
                // Otherwise, add new tile
                else {
                    DataManager.Instance.PlayerRaft.Tiles[i, j] = new Tile(selectedTile.Type, 0, selectedTile.Sprites[0]);
                }

                _UpdateVisual();
            });
        }

        // ---------------------------------------------------------------------

        private void _UpdateVisual()
        {
            var tile = DataManager.Instance.PlayerRaft.Tiles[indexI, indexJ];

            // Is tile empty?
            if (tile == null) {
                image.sprite = DataManager.Instance.DefaultSlotImage;
            }
            // Is a wall?
            else if (tile.Type == TileType.Wall)
            {
                var newTile = DataManager.Instance.GetWallDataByTier(tile.Tier);

                // Skip if nothing here brah
                if (newTile == null) return;

                image.sprite = newTile.Sprites[tile.Tier];
            }
            // Tile is something other than wall?
            else {
                image.sprite = tile.Sprite;
            }
        }
    }
}