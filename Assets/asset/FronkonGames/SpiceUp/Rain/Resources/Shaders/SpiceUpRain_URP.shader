////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Martin Bustos @FronkonGames <fronkongames@gmail.com>. All rights reserved.
//
// THIS FILE CAN NOT BE HOSTED IN PUBLIC REPOSITORIES.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
Shader "Hidden/Fronkon Games/Spice Up/Rain URP"
{
  Properties
  {
    _MainTex("Main Texture", 2D) = "white" {}
  }

  SubShader
  {
    Tags
    {
      "RenderType" = "Opaque"
      "RenderPipeline" = "UniversalPipeline"
    }
    LOD 100
    ZTest Always ZWrite Off Cull Off

    Pass
    {
      Name "Fronkon Games Spice Up Rain"

      HLSLPROGRAM
      #include "SpiceUp.hlsl"
      #include "ColorBlend.hlsl"
      #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

      #pragma vertex SpiceUpVert
      #pragma fragment SpiceUpFrag
      #pragma fragmentoption ARB_precision_hint_fastest
      #pragma exclude_renderers d3d9 d3d11_9x ps3 flash
      #pragma multi_compile _ _USE_DRAW_PROCEDURAL
      #pragma multi_compile _ TINT_TRAILS

      float _Amount;
      float _Size;
      float _Distortion;
      float _Speed;
      int _Blend;
      float4 _AmbientColor;
      float4 _DropletColor;
      float _StaticLayer;
      float _DynamicLayer0;
      float _DynamicLayer1;
      float _Rotation;

      inline float3 N13(float p)
      {
        float3 p3 = frac((float3)p * float3(0.1031, 0.11369, 0.13787));
        p3 += dot(p3, p3.yzx + 19.19);

        return frac(float3((p3.x + p3.y) * p3.z, (p3.x + p3.z) * p3.y, (p3.y + p3.z) * p3.x));
      }

      inline float4 N14(float t)
      {
        return frac(sin(t * float4(123.0, 1024.0, 1456.0, 264.0)) * float4(6547.0, 345.0, 8799.0, 1564.0));
      }

      inline float N(float t)
      {
        return frac(sin(t * 12345.564) * 7658.76);
      }

      inline float Saw(float b, float t)
      {
        return smoothstep(0.0, b, t) * smoothstep(1.0, b, t);
      }

      inline float2 DropLayer2(float2 uv, float t)
      {
        float2 UV = uv;

        uv.y += t * 0.75;
        float2 a = float2(6.0, 1.0);
        float2 grid = a * 2.0;
        float2 id = floor(uv * grid);
        
        float colShift = N(id.x); 
        uv.y += colShift;
        
        id = floor(uv * grid);
        float3 n = N13(id.x * 35.2 + id.y * 2376.1);
        float2 st = frac(uv * grid) - float2(0.5, 0);
        
        float x = n.x - 0.5;
        
        float y = UV.y * 20.0;
        float wiggle = sin(y + sin(y));
        x += wiggle * (0.5 - abs(x)) * (n.z - 0.5);
        x *= 0.7;
        float ti = frac(t + n.z);
        y = (Saw(0.85, ti) - 0.5) * 0.9 + 0.5;
        float2 p = float2(x, y);

        float d = length((st - p) * a.yx);
        
        float mainDrop = smoothstep(0.4, 0.0, d);
        
        float r = sqrt(smoothstep(1.0, y, st.y));
        float cd = abs(st.x - x);
        float trail = smoothstep(0.23 * r, 0.15 * r * r, cd);
        float trailFront = smoothstep(-0.02, 0.02, st.y - y);
        trail *= trailFront * r * r;
        
        y = UV.y;
        float trail2 = smoothstep(0.2 * r, 0.0, cd);
        float droplets = max(0.0, (sin(y * (1.0 - y) * 120.0) - st.y)) * trail2 * trailFront * n.z;
        y = frac(y * 10.0) + (st.y - 0.5);
        float dd = length(st - float2(x, y));
        droplets = smoothstep(0.3, 0.0, dd);
        float m = mainDrop + droplets * r * trailFront;
        
        return float2(m, trail);
      }

      inline float StaticDrops(float2 uv, float t)
      {
        uv *= 40.0;

        float2 id = floor(uv);
        uv = frac(uv) - 0.5;
        float3 n = N13(id.x * 107.45 + id.y * 3543.654);
        float2 p = (n.xy - 0.5) * 0.7;
        float d = length(uv - p);

        float fade = Saw(0.025, frac(t + n.z));
        float c = smoothstep(0.3, 0.0, d) * frac(n.z * 10.0) * fade;

        return c;
      }

      inline float2 Drops(float2 uv, float t, float l0, float l1, float l2)
      {
        uv /= _Size;
        uv.x *= _ScreenParams.x / _ScreenParams.y;

        float s = StaticDrops(uv, t) * l0;
        float2 m1 = DropLayer2(uv, t) * l1;
        float2 m2 = DropLayer2(uv * 1.85, t) * l2;

        float c = s + m1.x + m2.x;
        c = smoothstep(0.3, 1.0, c);

        return float2(c, max(m1.y * l0, m2.y * l1));
      }

      half4 SpiceUpFrag(const VertexOutput input) : SV_Target 
      {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
        const float2 uv = UnityStereoTransformScreenSpaceTex(input.uv).xy;

        const half4 color = SAMPLE_MAIN(uv);
        half4 pixel = color;

        float staticDrops = smoothstep(-0.5, 1.0, _Amount) * 2.0 * _StaticLayer;
        float layer1 = smoothstep(0.25, 0.75, _Amount) * _DynamicLayer0;
        float layer2 = smoothstep(0.0, 0.5, _Amount) * _DynamicLayer1;

        float t = _Time.y * _Speed;
        float2 uv2 = uv - 0.5;

        float s0 = sin(_Rotation);
        float c0 = cos(_Rotation);

        float2x2 rotationMatrix = float2x2(c0, -s0, s0, c0);
        rotationMatrix *= 0.5;
        rotationMatrix += 0.5;
        rotationMatrix = rotationMatrix * 2.0 - 1.0;
        uv2 = mul(uv2, rotationMatrix);
        uv2 += 0.5; 

        float2 c = Drops(uv2, t, staticDrops, layer1, layer2);

        float2 e = float2(0.0025 * _Distortion, 0.0);
        float cx = Drops(uv2 + e, t, staticDrops, layer1, layer2).x;
        float cy = Drops(uv2 + e.yx, t, staticDrops, layer1, layer2).x;
        float2 n = float2(cx - c.x, cy - c.x);

        pixel = SAMPLE_MAIN(uv + n);
        
        // Color adjust.
        pixel.rgb = ColorAdjust(pixel.rgb);

        pixel.rgb *= lerp((float3)1.0, _AmbientColor.rgb, _AmbientColor.a);
#ifdef TINT_TRAILS
        pixel.rgb = lerp(color.rgb, ColorBlend(_Blend, color.rgb, pixel.rgb * _DropletColor.rgb), c.y + cy);
#else
        pixel.rgb = lerp(color.rgb, ColorBlend(_Blend, color.rgb, pixel.rgb * _DropletColor.rgb), c.x + cx);
#endif
        return lerp(color, pixel, _Intensity);
      }

      ENDHLSL
    }
  }
  
  FallBack "Diffuse"
}
