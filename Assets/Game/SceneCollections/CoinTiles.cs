using System.Collections.Generic;
using UnityEngine;

namespace Game.SceneCollections
{
    public class CoinTiles
    {
        public IEnumerable<GameObject> AvailableTiles { get; private set; }

        public CoinTiles()
        {
            AvailableTiles = new[]
            {
                Resources.Load<GameObject>("Prefabs/Coin")
            };
        }
    }
}