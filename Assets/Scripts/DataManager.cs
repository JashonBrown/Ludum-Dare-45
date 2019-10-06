using System.Collections;
using System.Collections.Generic;
using LudumDare.Characters;
using UnityEngine;

namespace LudumDare
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance; // Singleton

        public TileData Wall;
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public int RaftHeight;
        public int RaftWidth;
        public Sprite DefaultSlotImage;

        public int Money;
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public Raft PlayerRaft { get; set; }
        public int Level { get; set; }

        private void Awake()
        {
            #region [Singleton]
            if (Instance == null) Instance = this;

            DontDestroyOnLoad(this);

            // Destroy other instances
            if (FindObjectsOfType(GetType()).Length > 1) {
                Destroy(gameObject);
            }
            #endregion

            // Create new raft
            PlayerRaft = new Raft(RaftWidth, RaftHeight);
        }

        // ---------------------------------------------------------------------

        public TileData GetWallDataByTier(int wallTier) {
            return Wall;
        }
    }
}