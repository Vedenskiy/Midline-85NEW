using System;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Images
{
    public class ImagesService
    {
        public event Action<Sprite> ImageShown;
        public event Action ImageHide;

        public void Show(Sprite image) => 
            ImageShown?.Invoke(image);

        public void Hide() => 
            ImageHide?.Invoke();
    }
}