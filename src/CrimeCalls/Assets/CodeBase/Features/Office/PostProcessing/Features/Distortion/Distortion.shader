Shader "Midline/PostProcessing/Distortion"
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
            #define TEXTURE2D_SAMPLER2D(textureName, samplerName) sampler2D textureName
            float2 _Strength;
            sampler2D _DistortionTexture;


            
            float hash (float2 n)
            {
                return frac(sin(dot(n, float2(123.456789, 987.654321))) * 54321.9876 );
            }

            float noise(float2 p)
            {
                float2 i = floor(p);
                float2 u = smoothstep(0.0, 1.0, frac(p));
                float a = hash(i + float2(0,0));
                float b = hash(i + float2(1,0));
                float c = hash(i + float2(0,1));
                float d = hash(i + float2(1,1));
                float r = lerp(lerp(a, b, u.x),lerp(c, d, u.x), u.y);
                return r * r;
            }

            float fbm(float2 p, int octaves)
            {
                float value = 0.0;
                float amplitude = 0.5;
                float e = 3.0;
                for (int i = 0; i < octaves; ++ i)
                {
                    value += amplitude * noise(p); 
                    p = p * e; 
                    amplitude *= 0.5; 
                    e *= 0.95;
                }
                return value;
            }
            
            half2 FlowUV(half2 uv, float2 flowVector, float time)
            {
                float progress = frac(time);
                progress = 0.1f;
                return uv - fbm(uv + _Time.y, 2)/2 * _Strength;
            }

            float3 FlowUVW (float2 uv, float2 flowVector, float time, bool flowB) {
	            float phaseOffset = flowB ? 0.5 : 0;
	            float progress = frac(time + phaseOffset) * _Strength.x;
	            float3 uvw;
	            uvw.xy = uv - flowVector * progress + phaseOffset;
	            uvw.z = 1 - abs(1 - 2 * progress) * _Strength.x;
	            return uvw;
            }
            
            half4 Fragment(Varyings i) : SV_Target
            {
                float u = i.texcoord.x;
                float v = i.texcoord.y;
                float2 uv = float2(u, v);
                half4 flow = tex2D(_DistortionTexture, uv * 3);
                //return flow;
                
                //uv = FlowUV(uv, flow.rg, _Time.y);
                //uv = frac(uv);
                float time = _Time.y + flow.a;
                float3 uvwA = FlowUVW(uv, flow * 2 - 1, time, false);
			    float3 uvwB = FlowUVW(uv, flow * 2 - 1, time, true);
                half4 screen = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearRepeat, uvwA.xy) * uvwA.z;
                half4 screen2 = SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearRepeat, uvwB.xy) * uvwB.z;
                half4 result = screen + screen2;


                float2 newUv = uv;
                return SAMPLE_TEXTURE2D(_BlitTexture, sampler_LinearRepeat, newUv + fbm(uv + _Time.y, 3) * _Strength);
                
                return result;
            }
            ENDHLSL
        }
    }
}