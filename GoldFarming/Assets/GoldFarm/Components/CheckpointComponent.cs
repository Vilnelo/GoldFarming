using System.IO;
using UnityEngine;

namespace GoldFarm.Components
{
    public class CheckpointComponent
    {
        private string _savePath;

        public CheckpointComponent()
        {
            _savePath = Application.persistentDataPath + "/saveData.json";
        }

        public void SavePlayerData(PlayerData data)
        {
            string saveData = JsonUtility.ToJson(data);
            File.WriteAllText(_savePath, saveData);
        }

        public PlayerData LoadPlayerData()
        {
            if (File.Exists(_savePath))
            {
                string saveData = File.ReadAllText(_savePath);
                return JsonUtility.FromJson<PlayerData>(saveData);
            }
            else
            {
                Debug.LogWarning("Save file not found.");
                return null;
            }
        }

        public void OnApplicationQuit()
        {
            if (File.Exists(_savePath))
            {
                File.Delete(_savePath);
            }
        }
    }
}

