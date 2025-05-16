using UnityEngine;

namespace CodeBase.Features.Common.Cursor
{
    public class CustomCursor
    {
        public void HideCursor()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
        
        public void ShowCursor()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }
}