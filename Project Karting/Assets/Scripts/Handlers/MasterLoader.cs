using UnityEngine;

namespace Handlers {
    public class MasterLoader : MonoBehaviour {
        private void Awake() {
            LevelManager.instance.Init();
            SceneManager.instance.LoadMainMenu();
        }
    }
}
