using System.Threading;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Images
{
    public class ImageHandler : RequestHandler<ImageNode>
    {
        private readonly ImagesService _images;

        public ImageHandler(ImagesService images) => 
            _images = images;

        protected override async UniTask Handle(ImageNode request, CancellationToken token)
        {
            var sprite = Resources.Load<Sprite>(request.PathToImage);
            _images.Show(sprite);
        }
    }
}