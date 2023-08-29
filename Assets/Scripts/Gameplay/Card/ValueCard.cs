using System.Collections;
using System.Collections.Generic;
using Ftech.Utilities;
using UnityEngine;
using TMPro;

namespace DynamicNumber.GamePlay
{
    public enum ValueCardType
    {
        multiply = 0,
        divide,
        plus,
        minus
    }

    public class ValueCard : EffectCard
    {
        [SerializeField]
        private TextMeshProUGUI ValueText;

        private ValueCardType ValueType;
        private int ValueAmount;

        public void SetContent(ValueCardType type, int amount, Color textColor, Sprite upflipSprite)
        {
            ValueType = type;
            ValueAmount = amount;

            ValueText.color = textColor;
            UpFlipSprite = upflipSprite;
            switch (type)
            {
                case ValueCardType.multiply:
                    {
                        ValueText.text = "X" + amount;
                        break;
                    }
                case ValueCardType.divide:
                    {
                        ValueText.text = 1 + "/" + amount;
                        break;
                    }
                case ValueCardType.plus:
                    {
                        ValueText.text = "+" + amount + "%";
                        break;
                    }
                case ValueCardType.minus:
                    {
                        ValueText.text = "-" + amount + "%";
                        break;
                    }
            }
        }

        public override void SetCardDown()
        {
            if (ValueText) ValueText.gameObject.SetActive(false);
            base.SetCardDown();
        }

        public override void ShowContent()
        {
            if (ValueText)
            {
                ValueText.gameObject.SetActive(true);
            }
            base.ShowContent();
        }

        public override void ActiveEffect()
        {
            var playerPoint = GameManager.Instance.PlayerCurrentValue;

            switch (ValueType)
            {
                case ValueCardType.multiply:
                    {
                        playerPoint *= ValueAmount;
                        break;
                    }
                case ValueCardType.divide:
                    {
                        playerPoint /= ValueAmount;
                        break;
                    }
                case ValueCardType.plus:
                    {
                        var plusValue = (int)(GameManager.Instance.PlayerFirstBet * ((float)ValueAmount / 100));
                        playerPoint += plusValue; 
                        break;
                    }
                case ValueCardType.minus:
                    {
                        var minusValue = (int)(GameManager.Instance.PlayerFirstBet * ((float)ValueAmount / 100));
                        playerPoint -= minusValue;
                        break;
                    }
            }

            var message = new Dictionary<string, object>();
            message.Add(EventID.EVENT_DATA_KEY, playerPoint);
            EventDispatcher.PostEvent(EventID.ON_PLAYER_POINT_CHANGE, message);

            base.ActiveEffect();
        }
    }
}

