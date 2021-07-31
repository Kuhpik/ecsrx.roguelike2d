using System;
using System.Collections.Generic;
using System.Linq;
using SystemsRx.Attributes;
using SystemsRx.Events;
using SystemsRx.Extensions;
using SystemsRx.Systems.Conventional;
using EcsRx.Extensions;
using EcsRx.Groups;
using EcsRx.Groups.Observable;
using EcsRx.Plugins.GroupBinding.Attributes;
using EcsRx.Systems;
using EcsRx.Unity.Extensions;
using Game.Components;
using Game.Events;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Game.Systems
{
    [Priority(10)]
    public class CoinTextUpdateSystem : IManualSystem, IGroupSystem
    {
        public IGroup Group { get; } = new Group(typeof(PlayerComponent));

        [FromGroup]
        public IObservableGroup ObservableGroup;

        private const float _duration = 0.75f;
        
        private readonly IEventSystem _eventSystem;
        private PlayerComponent _playerComponent;
        private Text _coinText;
        private readonly IList<IDisposable> _subscriptions = new List<IDisposable>();
        private int _lastCoinsCount;
        private IDisposable _textAnimationRoutine; //Have no idea how to stop MicroCoroutine

        public CoinTextUpdateSystem(IEventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }

        public void StartSystem()
        {
            this.WaitForScene().Subscribe(x =>
            {
                var player = ObservableGroup.First();
                _playerComponent = player.GetComponent<PlayerComponent>();
                _coinText = GameObject.Find("CoinText").GetComponent<Text>();
                _lastCoinsCount = _playerComponent.Coins.Value;
                SetupSubscriptions();
            });
        }

        private void SetupSubscriptions()
        {
            _eventSystem.Receive<CoinPickupEvent>()
                .Subscribe(x =>
                {
                    var coinComponent = x.Coin.GetComponent<CoinComponent>();
                    var coins = coinComponent.Count;

                    if (_textAnimationRoutine != null) { _textAnimationRoutine.Dispose(); }
                    _textAnimationRoutine = Observable.FromCoroutine(TextAnimationRoutine).Subscribe();
                })
                .AddTo(_subscriptions);
        }

        private IEnumerator TextAnimationRoutine()
        {
            var timer = 0f;
            var lastCoinsCashed = _lastCoinsCount;
            var coins = _playerComponent.Coins.Value;

            _lastCoinsCount = coins;

            while (timer <= _duration)
            {
                timer += Time.deltaTime;

                var percent = Mathf.Clamp01(timer / _duration);
                var value = Mathf.Lerp(lastCoinsCashed, coins, percent);

                _coinText.text = $"Coins: {value:F0}";

                yield return null;
            }
        }

        public void StopSystem()
        { 
            _subscriptions.DisposeAll();

            if (_textAnimationRoutine != null)  
                _textAnimationRoutine.Dispose(); 
        }
    }
}