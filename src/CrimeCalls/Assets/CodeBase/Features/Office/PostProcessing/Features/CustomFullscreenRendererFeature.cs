using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace CodeBase.Features.Office.PostProcessing.Features
{
    public abstract class CustomFullscreenRendererFeature : ScriptableRendererFeature
    {
        public Shader Shader;
        public RenderPassEvent RenderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;

        private CustomFullscreenRenderPass _pass;

        protected abstract CustomFullscreenRenderPass CreateCustomPass();
        
        public override void Create()
        {
            _pass = CreateCustomPass();
            _pass.renderPassEvent = RenderPassEvent;
        }
        
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (_pass == null) 
                Debug.LogWarning($"{name} shader is null and will be skipped");
            
            if (renderingData.cameraData.cameraType == CameraType.Game)
                renderer.EnqueuePass(_pass);
        }

        protected override void Dispose(bool disposing) => 
            _pass.Dispose();
    }
}