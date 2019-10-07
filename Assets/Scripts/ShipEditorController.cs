using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare
{
    public class ShipEditorController : MonoBehaviour
    {
        public static ShipEditorController Instance;

        public GameObject shipGridView;
        public GameObject editorTilePrefab;
        [NonSerialized] public TileData selectedTile;
        public Text MoneyText;
        public Text TooltipText;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        // ---------------------------------------------------------------------

        private void Start()
        {
            selectedTile = DataManager.Instance.GetWallDataByTier(1);
            RefreshShipEditor();
        }

        // ---------------------------------------------------------------------

        public void RefreshShipEditor()
        {
            Raft raft = DataManager.Instance.PlayerRaft;
            MoneyText.text = $"${DataManager.Instance.Money}";
            var gridLayout = shipGridView.GetComponent<GridLayoutGroup>();
            gridLayout.constraintCount = raft.Height;

            for (int i = 0; i < raft.Width; i++)
            {
                for (int j = 0; j < raft.Height; j++)
                {
                    // Skip if no tile assigned

                    // Add tile to screen
                    GameObject go = Instantiate(editorTilePrefab, new Vector3(i, j, 0), Quaternion.identity);
                    go.transform.SetParent(shipGridView.transform);
                    go.transform.localScale = new Vector3(1,1,1); // Resets some bull shit scaling thing
                    go.GetComponent<ShipEditorButton>().Init(i,j);
                }
            }
        }

        public void UpdateMoney() {
            MoneyText.text = $"${DataManager.Instance.Money}";
        }

        // ---------------------------------------------------------------------

        public void SetSelectedTile(TileData tileData)
        {
            selectedTile = tileData;
        }

        public void UpdateTooltip(TileData tileData) {
            if (tileData == null) return;

            ClearTooltip();
            
            TooltipText.text += tileData.Type + "\n";
            TooltipText.text += $"Costs: {string.Join(", ", tileData.Costs.Select(x => $"${x.ToString()}"))}";
        }
        
        public void UpdateTooltip(Tile tile) {
            if (tile == null) return;
            
            ClearTooltip();
            
            TooltipText.text += tile.Type + "\n";
            TooltipText.text += $"Tier: {tile.Tier + 1}\n";

            if (selectedTile.Type == tile.Type) {
                if (tile.Tier == 2) {
                    TooltipText.text += "Fully upgraded!";
                }
                else {
                    TooltipText.text += $"Click to upgrade to tier {tile.Tier + 2} for ${selectedTile.Costs[tile.Tier + 1]}";
                }
            }
            else {
                TooltipText.text += $"Click to replace with {selectedTile.Type} for ${selectedTile.Costs[0]}";
            }
        }

        public void ClearTooltip() {
            TooltipText.text = "";
        }
    }
}