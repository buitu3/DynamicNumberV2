using Ftech.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DynamicNumber.GamePlay
{
    public class SwapCard : SpecialCard
    {
        public override void ActiveEffect()
        {
            var playerPoint = GameManager.Instance.PlayerCurrentValue;

            int tempValue = playerPoint;
            int digitCount = (int)Mathf.Log10(tempValue);
            int lastDigit = tempValue % 10;

            while (tempValue >= 10)
            {
                tempValue /= 10;
            }
            int firstDigit = tempValue;
            
            int swapPoint = (lastDigit * (int)Mathf.Pow(10, digitCount) + firstDigit) + (playerPoint - (firstDigit * (int)Mathf.Pow(10, digitCount) + lastDigit));

            var message = new Dictionary<string, object>();
            message.Add(EventID.EVENT_DATA_KEY, swapPoint);
            EventDispatcher.PostEvent(EventID.ON_PLAYER_POINT_CHANGE, message);

            base.ActiveEffect();
        }
    }

}
