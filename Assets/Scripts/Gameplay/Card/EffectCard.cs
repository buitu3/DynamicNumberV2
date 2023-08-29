using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using Ftech.Utilities;

namespace DynamicNumber.GamePlay
{
    public class EffectCard : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        protected Image CardIcon;
        [SerializeField]
        protected Image CardBG;

        protected Sprite UpFlipSprite;
        protected bool IsUpFlip = false;
        protected bool CanFlip = true;

        void Awake()
        {
            SetCardDown();
        }        

        void Start()
        {
            EventDispatcher.RegisterListener(EventID.ON_CARD_QUEUE_FULL, OnQueueFullHandler);
        }

        void OnDestroy()
        {
            EventDispatcher.RemoveListener(EventID.ON_CARD_QUEUE_FULL, OnQueueFullHandler);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(!IsUpFlip && CanFlip) UpFlip();
        }        

        public virtual void SetCardDown()
        {
            if(CardIcon) CardIcon.gameObject.SetActive(false);
            CardBG.sprite = GameDataManager.Instance.CardDatas.DownFlipCardBG;
        }

        /// <summary>
        /// Do flip card up animation and show its content
        /// </summary>
        public virtual void UpFlip()
        {
            transform.DOScaleX(0f, Constant.CARD_FLIP_TIME / 2).OnComplete(() =>
            {
                ShowContent();
                IsUpFlip = true;

                transform.DOScaleX(1f, Constant.CARD_FLIP_TIME / 2).OnComplete(() => {
                    var message = new Dictionary<string, object>();
                    message.Add(EventID.EVENT_DATA_KEY, this);
                    EventDispatcher.PostEvent(EventID.ON_CARD_FLIPPED, message);
                });
            });
        }

        public virtual void ShowContent()
        {
            if (CardIcon) CardIcon.gameObject.SetActive(true);
            CardBG.sprite = UpFlipSprite;
        }

        public virtual void ActiveEffect()
        {
            transform.DOScale(Vector3.zero, 1f).OnComplete(() =>
            {
                Destroy(this.gameObject);
            });
        }

        #region Events Handler

        private void OnQueueFullHandler(Dictionary<string, object> param)
        {
            // Deactive this card so player cannot flip it up anymore
            if (!IsUpFlip) CanFlip = false;
        }

        #endregion
    }
}
