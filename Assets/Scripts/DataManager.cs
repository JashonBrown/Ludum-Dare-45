using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance; // Singleton

        public TileData WallTier1;
        public int RaftHeight;
        public int RaftWidth;

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public Raft PlayerRaft { get; set; }

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
            PlayerRaft = new Raft(RaftHeight, RaftHeight);
        }
    }
}