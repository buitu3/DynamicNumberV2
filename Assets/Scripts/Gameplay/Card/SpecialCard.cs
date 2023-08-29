using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DynamicNumber.GamePlay
{
    public class SpecialCard : EffectCard
    {
        private string CardName;
        private string CardDesctiption;

        public void SetContent(string cardName, string cardDesctiption, Sprite iconImage, Sprite upFlipSprite)
        {
            CardName = cardName;
            CardDesctiption = cardDesctiption;
            CardIcon.sprite = iconImage;
            UpFlipSprite = upFlipSprite;
        }
    }

}
