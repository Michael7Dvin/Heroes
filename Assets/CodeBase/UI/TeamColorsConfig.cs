using UnityEngine;

namespace CodeBase.UI
{
    
    [CreateAssetMenu(fileName = "Team Colors Config", menuName = "Configs/UI/Team Colors")]
    public class TeamColorsConfig : ScriptableObject
    {
        [field: SerializeField] public Color HumansFillColor { get; private set; }
        [field: SerializeField] public Color UndeadsFillColor { get; private set; }
    }
}