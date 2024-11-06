using System;
using CodeBase.Features.Calls.UI;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Features.Calls.Handlers.Images.UI
{
    public class ImageUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private BackgroundShaderController _shaderController;
        
        private ImagesService _images;

        [Inject]
        public void Construct(ImagesService images)
        {
            _images = images;
        }

        private void OnEnable()
        {
            _images.ImageShown += OnImageShown;
            _images.ImageHide += OnImageHide;
        }

        private void OnDisable()
        {
            _images.ImageShown -= OnImageShown;
            _images.ImageHide -= OnImageHide;
        }
        
        private void OnImageShown(Sprite image)
        {
            _image.sprite = image;
            _shaderController.Show();
        }
        
        private void OnImageHide()
        {
            _shaderController.Hide();
        }
    }
}