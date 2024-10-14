using TMPro;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Phrases.UI
{
    public class CallPhraseView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _personName;
        [SerializeField] private TextMeshProUGUI _personMessage;

        public void Setup(string personKey, string messageKey)
        {
            _personName.text = personKey;
            _personMessage.text = messageKey;
        }

        public void HideMessage()
        {
            _personMessage.text = string.Empty;
        }
    }
}