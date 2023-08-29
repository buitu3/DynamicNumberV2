using Ftech.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DynamicNumber.GamePlay
{
    public class ResetCard : SpecialCard
    {
        public override void ActiveEffect()
        {
            var playerPoint = GameManager.Instance.PlayerCurrentValue;
            playerPoint = GameManager.Instance.PlayerFirstBet;

            var message = new Dictionary<string, object>();
            message.Add(EventID.EVENT_DATA_KEY, playerPoint);
            EventDispatcher.PostEvent(EventID.ON_PLAYER_POINT_CHANGE, message);

            base.ActiveEffect();
        }
    }
}

