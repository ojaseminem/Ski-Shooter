using UnityEngine;

namespace Gameplay.Scriptable_Objects
{
    [CreateAssetMenu(menuName = "SO/Data")]
    public class ScoreData : ScriptableObject
    {
        public int totalCoinCount;
        public int totalDiamondCount;
    }
}
