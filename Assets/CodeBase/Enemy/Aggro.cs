using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float _cooldown = 1f;
        [SerializeField] private TriggerObserver _observer;

        /*References*/
        private AgentMoveToPlayer _follow;

        /*Fields*/
        Coroutine _aggroCoroutine;
        private bool _hasAggroTarget;

        private void Awake()
        {
            _follow = GetComponent<AgentMoveToPlayer>();
        }

        private void Start()
        {            
            _observer.TriggerEnter += TriggerEnter;
            _observer.TriggerExit += TriggerExit;

            SwitchFollowOff();
        }
        private void TriggerEnter(Collider obj)
        {
            if(!_hasAggroTarget)
            {
                _hasAggroTarget = true;
                StopAggroCoroutine();
                SwitchFollowOn();
            }
            
        }

        private void TriggerExit(Collider obj)
        {
            if(_hasAggroTarget)
            {
                _hasAggroTarget= false;
                _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            }           
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            SwitchFollowOff();
            yield return null;
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }

        private void SwitchFollowOn() => _follow.enabled = true;

        private void SwitchFollowOff() => _follow.enabled = false;
    }
}

