using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DynamicNumber.UI
{
    public class LobbyGUI : MonoBehaviour
    {
        public void OnButtonBetClickedHandler(int betValue)
        {
            GameDataManager.Instance.PlayerBetValue = betValue;
            SceneManager.LoadScene("GamePlayScene");
        }
    }

}
