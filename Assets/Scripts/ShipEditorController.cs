﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare
{
    public class ShipEditorController : MonoBehaviour
    {
        public static ShipEditorController Instance;

        public GameObject shipGridView;
        public GameObject editorTilePrefab;
        public TileData SelectedTile;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        // Start is called before the first frame update
        void OnEnable()
        {
            RefreshShipEditor();
        }

        // ---------------------------------------------------------------------

        public void RefreshShipEditor()
        {
            Raft raft = DataManager.Instance.PlayerRaft;

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

        // ---------------------------------------------------------------------

        public void SetSelectedTile()
        {

        }

    }
}