using UnityEngine;
namespace Rubik.ScoopingGame
{
    public class PlayGameController : MonoBehaviour
    {
        [SerializeField] private PlayGameUI playGameUI;
        public void OnPlayGame()
        {
            ScoopTinyManager.Instance.StartCoroutine(ScoopTinyManager.Instance.OpenScoopTiny(()=>
            {
                Debug.Log("OpenScoopTiny done, now spawn blind bag");
                playGameUI.HidePlayGamePanel();
                GameController.instance.creatBindBag.StartCoroutine(GameController.instance.creatBindBag.SpawnBlindBag());
            }));
            
        }

    }
}
