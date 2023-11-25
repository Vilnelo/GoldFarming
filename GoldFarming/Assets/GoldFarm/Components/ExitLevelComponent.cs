using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GoldFarm.Components
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private UnityEvent _onExit;
        public void Exit()
        {
            _onExit?.Invoke();
            SceneManager.LoadScene(_sceneName);
        }
    }
}