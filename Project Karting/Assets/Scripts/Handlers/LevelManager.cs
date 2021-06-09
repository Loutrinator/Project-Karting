using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Handlers {
    [CreateAssetMenu(fileName = "LevelManager", menuName = "ScriptableObject/LevelManager")]
    public class LevelManager : ScriptableObject
    {
        #region Singleton
        public static LevelManager instance;
        
        private void OnEnable()
        {
            if (instance != null)
                throw new UnityException(typeof(LevelManager) + " is already instantiated");
            instance = this;
        }

        private void OnDisable()
        {
            instance = null;
        }
        #endregion

        [HideInInspector] public GameConfig gameConfig;

        [HideInInspector] public Race currentRace;    // instantiated

        public void Init() {
            gameConfig = new GameConfig {
                players = new List<PlayerConfig>(), 
                races = new List<Race>()
            };
        }
        
        public Race InitLevel() {
            currentRace = Instantiate(gameConfig.races[0]);
            currentRace.Init();
            return currentRace;
        }
    }
}