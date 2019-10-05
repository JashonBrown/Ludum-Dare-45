using System.Collections;
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
                TileData selectedTile = ShipEditorController.Instance.SelectedTile;
                TileData currentTile = DataManager.Instance.PlayerRaft.Tiles[i, j];

                if (selectedTile.Type == TileType.Wall && currentTile != null && currentTile.Type == TileType.Wall)
                {

                }
                else
                {
                    
                }

                DataManager.Instance.PlayerRaft.Tiles[i, j] = selectedTile;

                _UpdateVisual();
            });
        }

        // ---------------------------------------------------------------------

        private void _UpdateVisual()
        {
            var tile = DataManager.Instance.PlayerRaft.Tiles[indexI, indexJ];
            if (tile == null || tile.Sprite == null) return;
            image.sprite = DataManager.Instance.PlayerRaft.Tiles[indexI, indexJ].Sprite;
        }
    }
}