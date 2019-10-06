using UnityEngine;

namespace LudumDare.Characters {
    [CreateAssetMenu(menuName = "Character")]
    public class Character : ScriptableObject {
        public string charactrName;
        public Sprite portrait;
        public Sprite standing;
    }
}