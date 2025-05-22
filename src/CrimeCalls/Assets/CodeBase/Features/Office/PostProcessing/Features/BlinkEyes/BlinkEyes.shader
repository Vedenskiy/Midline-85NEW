Shader "Midline/PostProcessing/BlinkEyes"
{
    SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Opaque"
        }

        Pass
        {
            Name "AnalogGlitchPass"

            ZWrite Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Fragment

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
            
            float _Progress;

            float Blink(float blink, float2 uv)
            {
                if (uv.y < 0.5)
                    uv.y *= 2.0;
                else
                {
                    uv.y = 1.0 - uv.y;
                    uv.y *= 2.0;
                }

                float x = uv.x - 0.5;
                float eyeline = x * x - 0.5;
                return eyeline;
                eyeline = lerp(eyeline, blink, blink);
                return eyeline;
            }

            half4 Fragment(Varyings i) : SV_Target
            {
                float u = i.texcoord.x;
                float v = i.texcoord.y;
                float2 uv = float2(u, v);
                float2 circleUv = uv - 0.5;
                circleUv.y *= lerp(0, 10, _Progress);
                float distance = length(circleUv);
                float circle = 1 - smoothstep(0.5, 0.6, distance);
                float mask = lerp(circle, 0, _Progress);
                return SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearRepeat, uv) * mask;
            }
            ENDHLSL
        }
    }
}