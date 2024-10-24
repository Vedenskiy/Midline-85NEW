using System;
using CodeBase.Features.Calls.Configs;
using UnityEngine;

namespace CodeBase.Features.Menu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private RectTransform _levelsLayout;
        [SerializeField] private LevelDetailsUI _levelDetailsUI;
        [SerializeField] private LevelCardView _levelCardPrefab;

        [SerializeField] private LevelsCatalog _catalog;

        private void OnEnable()
        {
            _levelDetailsUI.Returned += OnReturnToMenu;
        }

        private void OnDisable()
        {
            _levelDetailsUI.Returned -= OnReturnToMenu;
        }

        private void Start()
        {
            foreach (var config in _catalog.Levels)
            {
                var prefab = Instantiate(_levelCardPrefab, _levelsLayout);
                prefab.Setup(config);
                prefab.Clicked += OnLevelCardSelected;
            }
        }

        private void OnReturnToMenu()
        {
            _levelsLayout.gameObject.SetActive(true);
            _levelDetailsUI.gameObject.SetActive(false);
        }

        private void OnLevelCardSelected(string level)
        {
            var config = FindByName(level);
            _levelDetailsUI.Setup(config);
            
            _levelsLayout.gameObject.SetActive(false);
            _levelDetailsUI.gameObject.SetActive(true);
        }

        private LevelConfig FindByName(string levelName)
        {
            foreach (var config in _catalog.Levels)
            {
                if (config.Name == levelName)
                    return config;
            }

            throw new Exception($"Level {levelName} not found in catalog!");
        }
    }
}