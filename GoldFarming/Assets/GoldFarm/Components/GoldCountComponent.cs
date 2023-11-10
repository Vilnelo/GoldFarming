using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace GoldFarm.Components
{
    public class GoldCountComponent : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        [SerializeField] private GameObject _objectToCount;

        private int _coins;

        public void Awake()
        {
            _coins = PlayerPrefs.GetInt("coins");
        }
        public void CountGold()
        {

            if (_objectToCount.tag == "Gold")
            {
                if ((_coins + 10) >= 100)
                {
                    PlayerPrefs.SetInt("coins", 100);
                }
                else
                {
                    PlayerPrefs.SetInt("coins", _coins + 10);
                }
                

            }
            else if (_objectToCount.tag == "Silver")
            {
                if ((_coins + 1) >= 100)
                {
                    PlayerPrefs.SetInt("coins", 100);
                }
                else
                {
                    PlayerPrefs.SetInt("coins", _coins + 1);
                }
            }
            else if (_objectToCount.tag == "Spikes")
            {

                if (_coins == 0)
                {
                    var scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
                else if ((_coins - 30) <= 0)
                {
                    PlayerPrefs.SetInt("coins", 0);
                }
                else
                {
                    PlayerPrefs.SetInt("coins", _coins - 30);
                }

            }

            Debug.Log(PlayerPrefs.GetInt("coins"));
        }
    }
}
