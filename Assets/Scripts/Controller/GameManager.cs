using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Ftech.Utilities;
using TMPro;
using UnityEngine;

namespace DynamicNumber.GamePlay
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private CardBoard Board;
        [SerializeField]
        private CardQueue Queue;

        [SerializeField] private TextMeshProUGUI PlayerBetText;
        [SerializeField] private TextMeshProUGUI PlayerCurrentPointText;
        [SerializeField] private TextMeshProUGUI RemainPickText;

        public int PlayerCurrentValue { get; private set; }
        public int PlayerFirstBet { get; private set; }

        private Coroutine _jumpGoldTextRoutine;

        private void Start()
        {
            PlayerFirstBet = 100000;
            PlayerCurrentValue = PlayerFirstBet;

            PlayerBetText.text = PlayerFirstBet.ToString();
            PlayerCurrentPointText.text = PlayerCurrentValue.ToString();

            UpdatePickRemainText();

            EventDispatcher.RegisterListener(EventID.ON_PLAYER_POINT_CHANGE, OnPlayerPointChangeHandler);
            EventDispatcher.RegisterListener(EventID.ON_CARD_ENQUEUED, OnCardEnqueedHander);
        }

        private void OnDestroy()
        {
            EventDispatcher.RemoveListener(EventID.ON_PLAYER_POINT_CHANGE, OnPlayerPointChangeHandler);
            EventDispatcher.RemoveListener(EventID.ON_CARD_ENQUEUED, OnCardEnqueedHander);
        }

        #region Events Handler

        private void OnPlayerPointChangeHandler(Dictionary<string, object> param)
        {
            var data = param[EventID.EVENT_DATA_KEY];
            var point = (int) data;

            if(_jumpGoldTextRoutine != null) StopCoroutine(_jumpGoldTextRoutine);
            _jumpGoldTextRoutine = StartCoroutine(DoJumpGoldTextRoutine(PlayerCurrentValue, point, 0.8f));

            PlayerCurrentValue = point;
            PlayerCurrentPointText.text = PlayerCurrentValue.ToString();
        }

        private void OnCardEnqueedHander(Dictionary<string, object> param)
        {
            UpdatePickRemainText();
        }

        #endregion

        private void UpdatePickRemainText()
        {
            var remainPick = Constant.CARD_QUEUE_SIZE - Queue.GetQueueSize();
            RemainPickText.text = remainPick + "/" + Constant.CARD_QUEUE_SIZE + " picks left";
        }

        private IEnumerator DoJumpGoldTextRoutine(int startValue, int endValue, float duration)
        {
            float stepTime = 0.05f;
            float stepCount = duration / stepTime;
            float stepValue = ((float)(endValue - startValue)) / stepCount;
            StringBuilder goldStr = new StringBuilder();
            int jumpValue = startValue;

            bool isFinished = false;
            while (!isFinished)
            {
                jumpValue = (int)(jumpValue + stepValue);

                if (Mathf.Abs(jumpValue - endValue) < stepCount)
                {
                    jumpValue = endValue;
                    isFinished = true;
                }

                goldStr.Clear();
                goldStr.Append(jumpValue.ToString());
                PlayerCurrentPointText.text = goldStr.ToString();

                yield return new WaitForSecondsRealtime(stepTime);
            }
        }
    }
}

