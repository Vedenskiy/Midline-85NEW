using System.Threading;
using CodeBase.Features.Calls.Infrastructure.Handlers;
using CodeBase.Infrastructure.Common.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Features.Calls.Handlers.Images
{
    public class ImageHandler : RequestHandler<ImageNode>
    {
        private readonly AssetProvider _assets;
        private readonly ImagesService _images;

        public ImageHandler(AssetProvider assets, ImagesService images)
        {
            _assets = assets;
            _images = images;
        }

        protected override async UniTask Handle(ImageNode request, CancellationToken token)
        {
            Debug.Log($"Start image handler");
            var sprite = await _assets.Load<Sprite>(request.PathToImage);
            _images.Show(sprite);
            Debug.Log($"Show image: {request.PathToImage}");
        }
    }
}