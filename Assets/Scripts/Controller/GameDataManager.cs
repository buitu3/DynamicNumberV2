using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DynamicNumber
{
    public class GameDataManager : Singleton<GameDataManager>
    {
        public CardDataSO CardDatas;

        [HideInInspector]
        public int PlayerBetValue;
    }

}
