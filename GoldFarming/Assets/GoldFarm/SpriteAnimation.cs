using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GoldFarm
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] private int _framerate;
        [SerializeField] private bool _loop;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private UnityEvent _onComplete;

        private SpriteRenderer _renderer;
        private float _secondsPerFrame;
        private int _currentSpriteIndex;
        private float _nextFrameTime;


        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            

        }
        private void OnEnable()
        {
            _secondsPerFrame = 1f / _framerate;
            _nextFrameTime = Time.time + _secondsPerFrame;
            _currentSpriteIndex = 0;
        }
        private void Update()
        {
            if (_nextFrameTime > Time.time) return;

            if (_currentSpriteIndex >= _sprites.Length)
            {
                if (_loop)
                {
                    _currentSpriteIndex = 0;
                }
                else
                {
                    enabled = false;
                    _onComplete.Invoke();
                    return;

                }

            }
            _renderer.sprite = _sprites[_currentSpriteIndex];
            _nextFrameTime += _secondsPerFrame;
            _currentSpriteIndex++;
        }

        //public static void SetClip(string Animation)
        //{
        //    string _nameAnimation = Animation;

        //    switch (_nameAnimation)
        //    {
        //        case "idle":
        //            break;

        //        case "jump":
        //            break;

        //        case "fall":
        //            break;

        //        case "run":
        //            break;

        //        default:
        //            break;
        //    }
        //}
    }
}
