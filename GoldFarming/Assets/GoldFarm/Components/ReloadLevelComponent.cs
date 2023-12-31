using System.Collections;
using System.Collections.Generic;
using GoldFarm.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GoldFarm.Components
{
    public class ReloadLevelComponent : MonoBehaviour
    {

        public void Reload()
        {
            var session = FindObjectOfType<GameSession>();
                Destroy(session.gameObject);


            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}

