using System;
using CodeBase.Features.Calls.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.Features.Menu
{
    public class LevelCardView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _description;

        private string _levelLabel = "notfound";
        
        public event Action<string> Clicked;

        public void Setup(LevelConfig config)
        {
            _levelLabel = config.Name;
            _icon.sprite = config.Icon;
            _title.text = config.Name;
            _description.text = config.Description;
        }

        public void OnPointerClick(PointerEventData eventData) => 
            Clicked?.Invoke(_levelLabel);
    }
}