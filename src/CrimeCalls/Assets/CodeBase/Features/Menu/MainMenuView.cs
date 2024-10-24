using UnityEngine;

namespace CodeBase.Features.Menu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private LevelCardView _levelCardView;
        [SerializeField] private LevelDetailsUI _levelDetailsUI;

        private void OnEnable()
        {
            _levelCardView.Clicked += OnLevelCardSelected;
            _levelDetailsUI.Returned += OnReturnToMenu;
        }

        private void OnDisable()
        {
            _levelCardView.Clicked -= OnLevelCardSelected;
            _levelDetailsUI.Returned -= OnReturnToMenu;
        }

        private void OnReturnToMenu()
        {
            _levelCardView.gameObject.SetActive(true);
            _levelDetailsUI.gameObject.SetActive(false);
        }

        private void OnLevelCardSelected(string level)
        {
            Debug.Log($"Selected {level}");
            _levelCardView.gameObject.SetActive(false);
            _levelDetailsUI.gameObject.SetActive(true);
        }
    }
}