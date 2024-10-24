using System;
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

        public event Action<string> Clicked; 

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke("none");
        }
    }
}