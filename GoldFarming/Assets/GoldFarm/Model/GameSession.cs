using System.Collections;
using GoldFarm.Components;
using UnityEngine;

namespace GoldFarm.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;

        private CheckpointComponent _checkpointComponent;


        private void Awake()
        {
            _checkpointComponent = new CheckpointComponent();

            if (IsSessionExit())
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(this);
                PlayerData saveData = _checkpointComponent.LoadPlayerData();
                if (saveData != null)
                {
                    _data = saveData;
                }
            }
        }
        
        public void CheckpiontSession()
        {
            _checkpointComponent.SavePlayerData(_data);
        }
        private bool IsSessionExit()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions)
            {
                if (gameSession != this) return true;
            }

            return false;
        }

        private void OnApplicationQuit()
        {
            _checkpointComponent.OnApplicationQuit();
        }

    }
}