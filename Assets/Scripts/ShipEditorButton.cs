using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LudumDare
{
    public class ShipEditorButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
                if (currentTile != null && currentTile.Type == selectedTile.Type)
                {
                    // Don't upgrade
                    if (currentTile.Tier == 2) return;

                    // Upgrade wall tier
                    if (DataManager.Instance.Money >= selectedTile.Costs[currentTile.Tier + 1]) {
                        DataManager.Instance.Money -= selectedTile.Costs[currentTile.Tier + 1];
                        currentTile.Tier++;
                        currentTile.Sprite = selectedTile.Sprites[currentTile.Tier];
                        
                        ShipEditorController.Instance.UpdateMoney();
                        ShipEditorController.Instance.UpdateTooltip(currentTile);
                    }
                    else {
                        return;
                    }
                }
                // Otherwise, add new tile
                else if (selectedTile.Costs[0] <= DataManager.Instance.Money) {
                    DataManager.Instance.Money -= selectedTile.Costs[0];
                    DataManager.Instance.PlayerRaft.Tiles[i, j] = new Tile(selectedTile.Type, 0, selectedTile.Sprites[0]);
                    
                    ShipEditorController.Instance.UpdateMoney(); 
                    ShipEditorController.Instance.UpdateTooltip(DataManager.Instance.PlayerRaft.Tiles[i, j]);
                }
                else {
                    return;
                }

                _UpdateVisual();
            });
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            ShipEditorController.Instance.UpdateTooltip(DataManager.Instance.PlayerRaft.Tiles[indexI,indexJ]);
        }
        
        public void OnPointerExit(PointerEventData eventData) {
            ShipEditorController.Instance.ClearTooltip();
        }

        // ---------------------------------------------------------------------

        private void _UpdateVisual() {
            var tile = DataManager.Instance.PlayerRaft.Tiles[indexI, indexJ];

            // Is tile empty?
            if (tile == null) {
                image.sprite = DataManager.Instance.DefaultSlotImage;
            }
            // Is a wall?
            else if (tile.Type == TileType.Wall) {
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