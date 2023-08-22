using UnityEngine;

namespace CodeBase.Gameplay.Services.RandomService
{
    public class RandomService : IRandomService
    {
        public bool DoFiftyFifty() => 
            Random.value > 0.5f;
    }
}