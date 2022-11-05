using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Scriptable_Objects
{
    [CreateAssetMenu(menuName = "SO/Data")]
    public class UserData : ScriptableObject
    {
        public int tutorialComplete;
        public int totalUserCoinCount;
        public int totalUserDiamondCount;
    }
}
