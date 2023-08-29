using Ftech.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DynamicNumber.GamePlay
{
    public class CardQueue : MonoBehaviour
    {
        private Queue<EffectCard> Queue = new Queue<EffectCard>();

        private void Start()
        {
            EventDispatcher.RegisterListener(EventID.ON_CARD_FLIPPED, OnCardUpflipHandler);
        }

        private void OnDestroy()
        {
            EventDispatcher.RemoveListener(EventID.ON_CARD_FLIPPED, OnCardUpflipHandler);
        }

        public int GetQueueSize()
        {
            return Queue.Count;
        }

        public bool IsQueueFull()
        {
            return Queue.Count >= Constant.CARD_QUEUE_SIZE;
        }

        private IEnumerator ActivateQueueRoutine()
        {
            yield return new WaitForSeconds(1f);
            while (Queue.Count > 0)
            {
                var card = Queue.Dequeue();
                card.ActiveEffect();

                yield return new WaitForSeconds(4f);
            }
        }


        #region Event Handlers

        private void OnCardUpflipHandler(Dictionary<string, object> param)
        {
            var data = param[EventID.EVENT_DATA_KEY];
            if (data is EffectCard)
            {
                var card = data as EffectCard;
                
                Queue.Enqueue(card);
                card.transform.SetParent(this.transform);

                EventDispatcher.PostEvent(EventID.ON_CARD_ENQUEUED);

                if(IsQueueFull())
                {
                    StartCoroutine(ActivateQueueRoutine());

                    EventDispatcher.PostEvent(EventID.ON_CARD_QUEUE_FULL);
                }
            }
        }
        
        #endregion
    }
}

