using UnityEngine;

namespace CodeBase.Gameplay.Teams
{
    
    [CreateAssetMenu(fileName = "Team Colors", menuName = "UI/Team Colors")]
    public class TeamColors : ScriptableObject
    {
        [field: SerializeField] public Color HumansFillColor { get; private set; }
        [field: SerializeField] public Color UndeadsFillColor { get; private set; }
    }
}