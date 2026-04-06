using System;
using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using Rubik.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rubik.Config
{
    // Scene
    public class SceneConfig
    {
        public const string Login_Screen = "Login";
        public const string Home_Screen = "Home";
        public const string MiniGame01 = "MiniGame01";
        public const string MiniGame02 = "MiniGame02";
        public const string MiniGame03 = "MiniGame03";
        public const string MiniGame04 = "MiniGame04";
        public const string MiniGame05 = "MiniGame05";
        public const string MiniGame06 = "MiniGame06";
        public const string City_Screen = "City";
        public const string Restaurant_Screen = "Restaurant";
        public const string CityLand = "CityLand";
        public const string Loading_Screen = "Loading";
        public const string Waiting_Room = "WaitingRoom";
        public const string ChangeCharacter = "ChangeCharacter";
        public const string MiniGameForestGame = "MiniGameForestGame";
        public const string MiniGameGuessNumber = "MiniGameGuessNumber";
        public const string MiniGameFishing = "Park";
        public const string MiniGameJumping = "MinigameJumping";
        public const string MiniGameDrawing = "Drawing";
    }

    public class SceneController : NTBehaviour
    {
        public static SceneController Instance;
        protected override void Awake()
        {
            base.Awake();
            if (Instance != null)
            {
                NTLog.LogWarning("Only 1 Instance allow");
                return;
            }
            Instance = this;
        }

        public void LoadScreenWithLoading(string screenName, Action done = null)
        {
            NTLog.LogMessage("LoadScreenWithLoading: " + screenName);
            StartCoroutine(LoadScreenProgress(screenName, done));
        }

        public IEnumerator LoadScreenProgress(string screenName, Action done = null)
        {
            HUDCanvas.Instance.ShowLoadingPanel();
            bl_SceneLoaderManager.LoadScene(SceneConfig.Loading_Screen);
            NTLog.LogMessage("Loading Screen: " + SceneConfig.Loading_Screen);
            yield return WaitForSceneLoad(SceneConfig.Loading_Screen);
            // yield return new WaitForSeconds(1f);
            bl_SceneLoaderManager.LoadScene(screenName);
            yield return WaitForSceneLoad(screenName);
            NTLog.LogMessage("Loading Screen: " + screenName);
            done?.Invoke();
            HUDCanvas.Instance.HideLoadingPanel();
        }

        public IEnumerator WaitForSceneLoad(string sceneName, Action done = null)
        {
            yield return new WaitUntil(() => SceneManager.GetSceneByName(sceneName).isLoaded);
            done?.Invoke();
        }
    }
}