using UnityEngine;
using UnityEngine.UI;
namespace Rubik.ScoopingGame
{
    public class PlayGameUI : MonoBehaviour
    {
        [SerializeField] private GameObject playGamePanelObj;
        [SerializeField] private GameObject scoopBtnObj;
        [SerializeField] private GameObject scoopUIObj;
        [SerializeField] private GameObject playGameBtnObj;
        void Start()
        {
            SetUp();
        }
        public void SetUp(){
            playGamePanelObj.SetActive(true);
            scoopBtnObj.SetActive(false);
            scoopUIObj.SetActive(false);
            playGameBtnObj.SetActive(true);
            
        }
        public void HidePlayGamePanel(){
            playGamePanelObj.SetActive(false);
            playGameBtnObj.SetActive(false);
            scoopBtnObj.SetActive(true);
            scoopUIObj.SetActive(true);
        }


    }

}