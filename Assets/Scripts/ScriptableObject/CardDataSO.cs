using System.Collections.Generic;
using DynamicNumber.GamePlay;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DynamicNumber
{
    [CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CreateCardData", order = 1)]
    [System.Serializable]
    public class CardDataSO : ScriptableObject
    {
        [Title("General Card Data")]
        public Sprite DownFlipCardBG;

        [Title("Value Card")]
        public float ValueCardRate;
        public List<ValueCardData> ValueCardLst;
        public ValueCard ValueCardPrefab;
        
        [Title("Special Card")]
        public List<SpecialCardData> SpecialCardLst;

        public ValueCardData GetRandomValueCard()
        {
            if(ValueCardLst.Count > 0)
            {
                var randomIndex = UnityEngine.Random.Range(0, ValueCardLst.Count);
                return ValueCardLst[randomIndex];
            }
            return null;
        }

        public SpecialCardData GetRandomSpecialCard()
        {
            if(SpecialCardLst.Count > 0)
            {
                var randomIndex = Random.Range(0, SpecialCardLst.Count);
                return SpecialCardLst[randomIndex];
            }
            return null;
        }
    }

    [System.Serializable]
    public class CardData
    {
        public Sprite UpFlipBGSprite;
    }
    
    [System.Serializable]
    public class ValueCardData : CardData
    {
        public ValueCardType Type;
        public int ValueAmount;
        public Color TextColor;
    }

    [System.Serializable]
    public class SpecialCardData : CardData
    {
        public string CardName;
        public string CardDescription;
        public Sprite CardIcon;
        public SpecialCard CardPrefab;
    }
}

