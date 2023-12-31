using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DynamicNumber.GamePlay
{
    public class CardBoard : MonoBehaviour
    {
        [SerializeField]
        private RectTransform CardContainer;
        
        [SerializeField]
        private ValueCard ValueCardPrefab;

        [SerializeField] private EffectCard SpecialCardPrefab;

        private RectTransform CardPrefabRect;
        private Vector2 BoardStartPos;

        private int OffSetX = 8;
        private int OffSetY = 8;

        private void Start()
        {
            CardPrefabRect = ValueCardPrefab.GetComponent<RectTransform>();

            // Calculate board start position
            var boardCenter = Camera.main.WorldToScreenPoint(CardContainer.position);            

            var boardSize = CalculateRectSize(CardContainer);            
            BoardStartPos = new Vector2(boardCenter.x - boardSize.x/ 2, boardCenter.y - boardSize.y/ 2);

            InitBoard();
        }

        private void InitBoard()
        {
            // Calculate the amount of value card and special card on board
            var cardCount = Constant.BOARD_SIZE_HEIGHT * Constant.BOARD_SIZE_WIDTH;            
            var valueCardCount = (int)(cardCount * GameDataManager.Instance.CardDatas.ValueCardRate);
            var specialCardCount = cardCount - valueCardCount;

            // Generate pool of both value and special card to use
            var cardPool = new List<EffectCard>();
            cardPool.AddRange(GenerateValueCards(valueCardCount));
            cardPool.AddRange(GenerateSpecialCard(specialCardCount));
            
            // Assign each card in pool to each board item in order
            for (int yIndex = 0; yIndex < Constant.BOARD_SIZE_HEIGHT; yIndex++)
            {
                for (int xIndex = 0; xIndex < Constant.BOARD_SIZE_WIDTH; xIndex++)
                {
                    if (cardPool.Count <= 0) continue;
                    var randomIndex = Random.Range(0, cardPool.Count);
                    var card = cardPool[randomIndex];                                        

                    // Calculate card position based on board index
                    var cardPos = Camera.main.ScreenToWorldPoint(CalculateCardPos(xIndex, yIndex, card.GetComponent<RectTransform>()));
                    card.transform.position = new Vector3(cardPos.x, cardPos.y, 0);

                    cardPool.Remove(card);
                }
            }
        }

        private List<EffectCard> GenerateValueCards(int amount)
        {
            List<EffectCard> result = new List<EffectCard>();
            for (int i = 0; i < amount; i++)
            {
                var randomCardData = GameDataManager.Instance.CardDatas.GetRandomValueCard();
                if (randomCardData == null) continue;

                var newCard = Instantiate(GameDataManager.Instance.CardDatas.ValueCardPrefab,
                    CardContainer.transform);
                newCard.SetContent(randomCardData.Type, randomCardData.ValueAmount, randomCardData.TextColor, randomCardData.UpFlipBGSprite);
                result.Add(newCard);
            }

            return result;
        }

        private List<EffectCard> GenerateSpecialCard(int amount)
        {
            List<EffectCard> result = new List<EffectCard>();
            for(int i = 0; i < amount; i++)
            {
                var randomCardData = GameDataManager.Instance.CardDatas.GetRandomSpecialCard();
                if (randomCardData == null) continue;

                var newCard = Instantiate(randomCardData.CardPrefab, CardContainer.transform);
                newCard.SetContent(randomCardData.CardName, randomCardData.CardDescription, randomCardData.CardIcon, randomCardData.UpFlipBGSprite);
                result.Add(newCard);
            }

            return result;
        }

        private Vector2 CalculateCardPos(int xIndex, int yIndex, RectTransform cardRect)
        {
            var result = Vector2.zero;

            var cardSize = CalculateRectSize(cardRect);           
            var cardWidth = cardSize.x;
            var cardHeight = cardSize.y;

            result.x = BoardStartPos.x + xIndex * cardWidth + xIndex * OffSetX + cardWidth/2;
            result.y = BoardStartPos.y + yIndex * cardHeight + yIndex * OffSetY + cardHeight/2;

            return result;
        }

        private Vector2 CalculateRectSize(RectTransform rect)
        {
            Vector3[] v = new Vector3[4];
            rect.GetWorldCorners(v);
            var bottomLeft = Camera.main.WorldToScreenPoint(v[0]);
            var topLeft = Camera.main.WorldToScreenPoint(v[1]);
            var topRight = Camera.main.WorldToScreenPoint(v[2]);
            var bottomRight = Camera.main.WorldToScreenPoint(v[3]);

            var height = topLeft.y - bottomLeft.y;
            var width = bottomRight.x - bottomLeft.x;

            return new Vector2(width, height);
        }
    }
}

