using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace DynamicNumber.UI
{
    public class ResultPopup : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI PlayerBetText;
        [SerializeField]
        private TextMeshProUGUI PlayerResultText;

        public void Show(int originalBet, int result)
        {
            if (PlayerBetText) PlayerBetText.text = originalBet.ToString();
            if (PlayerResultText) PlayerResultText.text = result.ToString();
            this.gameObject.SetActive(true);
        }

        public void OnButtonPlayAgainClickedHandler()
        {
            SceneManager.LoadScene("LobbyScene");
        }
    }
}
