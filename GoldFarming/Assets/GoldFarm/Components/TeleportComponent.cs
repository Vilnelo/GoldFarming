using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoldFarm.Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destPosition;
        [SerializeField] private TeleportComponent _teleportDestination;
        //[SerializeField] private float _alphaTime = 1;
        //[SerializeField] private float _moveTime = 1;

        public bool _IsTeleported;
        public void Teleport(GameObject target)
        {

            if (_IsTeleported)
            {
                _IsTeleported = false;
            }
            else
            {
                _teleportDestination._IsTeleported = true;
                target.transform.position = _destPosition.position;
                _IsTeleported = false;
            }

            //StartCoroutine(AnimateTeleport(target));

        }

        //private IEnumerator AnimateTeleport(GameObject target)
        //{
        //    var sprite = target.GetComponent<SpriteRenderer>();

        //    if (_IsTeleported)
        //    {
        //        _IsTeleported = false;
        //    }
        //    else
        //    {
        //        _teleportDestination._IsTeleported = true;
        //        var input = target.GetComponent<HeroMovement>();
        //        SetLockInput(input, true);
        //        yield return SetAlpha(sprite, 0);
        //        target.SetActive(false);
        //        var hero = target.GetComponent<Hero>();

        //        MoveAnimation(target);

        //        target.SetActive(true);
        //        yield return SetAlpha(sprite, 1);
        //        SetLockInput(input, false);
        //        _IsTeleported = false;
        //    }
        //}

        //private void SetLockInput(HeroMovement input, bool isLocked)
        //{

        //    if (input != null)
        //    {
        //        input.enabled = !isLocked;
        //    }
        //}

        //private IEnumerator MoveAnimation(GameObject target)
        //{
        //    var moveTime = 0f;
        //    while (moveTime < _moveTime)
        //    {
        //        moveTime += Time.deltaTime;
        //        var progress = moveTime / _moveTime;
        //        target.transform.position = Vector3.Lerp(target.transform.position, _destPosition.position, progress);

        //        yield return null;
        //    }
        //}

        //private IEnumerator SetAlpha(SpriteRenderer sprite, float destAlpha)
        //{
        //    var time = 0f;
        //    var spriteAlpha = sprite.color.a;
        //    while (time < _alphaTime)
        //    {
        //        time += Time.deltaTime;
        //        var progress = time / _alphaTime;
        //        var tmpAlpha = Mathf.Lerp(spriteAlpha, destAlpha, progress);
        //        var color = sprite.color;
        //        color.a = tmpAlpha;
        //        sprite.color = color;

        //        yield return null;
        //    }
        //}

    }
}