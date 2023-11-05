using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GoldFarm.Components
{
    public class ReloadLevelComponent : MonoBehaviour
    {
        public void Reload()
        {
            var scene = SceneManager.GetActiveScene();
            PlayerPrefs.SetInt("coins", 0);
            SceneManager.LoadScene(scene.name);
        }
    }
}

