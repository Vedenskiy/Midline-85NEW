using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace CodeBase.Features.Office.PostProcessing.Features
{
    public abstract class CustomFullscreenRenderPass : ScriptableRenderPass, IDisposable
    {
        private static readonly Vector4 ScaleBias = new(1, 1, 0, 0);

        public readonly Material Material;
        
        protected CustomFullscreenRenderPass(Material material)
        {
            Material = material;
            requiresIntermediateTexture = true;
        }

        protected virtual string GetPassName() => 
            "Custom Fullscreen Render Pass";

        protected abstract void UpdateShaderParameters();
        
        private class PassData
        {
            internal TextureHandle Source;
            internal Material Material;
        }
        
        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            var resourceData = frameData.Get<UniversalResourceData>();
            var isPostProcessingEnabled = frameData.Get<UniversalCameraData>().postProcessEnabled;
            var isSceneViewCamera = frameData.Get<UniversalCameraData>().isSceneViewCamera;

            if (!isPostProcessingEnabled || isSceneViewCamera)
                return;

            if (resourceData.isActiveTargetBackBuffer)
                return;

            var src = resourceData.activeColorTexture;
            var destination = renderGraph.GetTextureDesc(src);
            destination.name = $"CameraColor-{GetPassName()}";
            destination.clearBuffer = false;

            var handle = renderGraph.CreateTexture(destination);
            
            if (Material != null)
                UpdateShaderParameters();
            else
                Debug.LogError($"{GetPassName()}: material is null, can't update shader parameters");
            
            using var builder = renderGraph.AddRasterRenderPass<PassData>(GetPassName(), out var passData, profilingSampler);
            
            passData.Source = src;
            passData.Material = Material;

            builder.UseTexture(src);
            builder.SetRenderAttachment(handle, 0);
            builder.SetRenderFunc((PassData data, RasterGraphContext context) => ExecutePass(data, context, 0));

            resourceData.cameraColor = handle;
        }
        
        private static void ExecutePass(PassData data, RasterGraphContext context, int pass) => 
            Blitter.BlitTexture(context.cmd, data.Source, ScaleBias, data.Material, pass);

        public void Dispose() => 
            CoreUtils.Destroy(Material);
    }
}