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
Shader "Hidden/Fronkon Games/Spice Up/Blurry URP"
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
      Name "Fronkon Games Spice Up Blurry"

      HLSLPROGRAM
      #include "SpiceUp.hlsl"
      #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

      #pragma vertex SpiceUpVert
      #pragma fragment SpiceUpFrag
      #pragma fragmentoption ARB_precision_hint_fastest
      #pragma exclude_renderers d3d9 d3d11_9x ps3 flash
      #pragma multi_compile _ _USE_DRAW_PROCEDURAL
      #pragma multi_compile ___ FRAMES_1 FRAMES_2 FRAMES_3 FRAMES_4 FRAMES_5 FRAMES_6 FRAMES_7 FRAMES_8 FRAMES_9

      float _Strength;
      int _FrameCount;

      TEXTURE2D(_RT0);
      TEXTURE2D(_RT1);
      TEXTURE2D(_RT2);
      TEXTURE2D(_RT3);
      TEXTURE2D(_RT4);
      TEXTURE2D(_RT5);
      TEXTURE2D(_RT6);
      TEXTURE2D(_RT7);
      TEXTURE2D(_RT8);
      TEXTURE2D(_RT9);
      
      half4 SpiceUpFrag(const VertexOutput input) : SV_Target 
      {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
        const float2 uv = UnityStereoTransformScreenSpaceTex(input.uv).xy;

        const half4 color = SAMPLE_MAIN(uv);
        half4 pixel = color;

        #if FRAMES_1
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT0, sampler_LinearClamp, uv).rgb, 0.5);
        #elif FRAMES_2
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT0, sampler_LinearClamp, uv).rgb, 0.75);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT1, sampler_LinearClamp, uv).rgb, 0.25);
        #elif FRAMES_3
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT0, sampler_LinearClamp, uv).rgb, 0.75);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT1, sampler_LinearClamp, uv).rgb, 0.50);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT2, sampler_LinearClamp, uv).rgb, 0.25);
        #elif FRAMES_4
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT0, sampler_LinearClamp, uv).rgb, 0.64);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT1, sampler_LinearClamp, uv).rgb, 0.48);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT2, sampler_LinearClamp, uv).rgb, 0.32);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT3, sampler_LinearClamp, uv).rgb, 0.16);
        #elif FRAMES_5
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT0, sampler_LinearClamp, uv).rgb, 0.70);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT1, sampler_LinearClamp, uv).rgb, 0.56);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT2, sampler_LinearClamp, uv).rgb, 0.42);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT3, sampler_LinearClamp, uv).rgb, 0.28);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT4, sampler_LinearClamp, uv).rgb, 0.14);
        #elif FRAMES_6
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT0, sampler_LinearClamp, uv).rgb, 0.750);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT1, sampler_LinearClamp, uv).rgb, 0.625);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT2, sampler_LinearClamp, uv).rgb, 0.500);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT3, sampler_LinearClamp, uv).rgb, 0.375);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT4, sampler_LinearClamp, uv).rgb, 0.250);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT5, sampler_LinearClamp, uv).rgb, 0.125);
        #elif FRAMES_7
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT0, sampler_LinearClamp, uv).rgb, 0.777);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT1, sampler_LinearClamp, uv).rgb, 0.666);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT2, sampler_LinearClamp, uv).rgb, 0.555);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT3, sampler_LinearClamp, uv).rgb, 0.444);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT4, sampler_LinearClamp, uv).rgb, 0.333);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT5, sampler_LinearClamp, uv).rgb, 0.222);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT6, sampler_LinearClamp, uv).rgb, 0.111);
        #elif FRAMES_8
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT0, sampler_LinearClamp, uv).rgb, 0.8);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT1, sampler_LinearClamp, uv).rgb, 0.7);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT2, sampler_LinearClamp, uv).rgb, 0.6);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT3, sampler_LinearClamp, uv).rgb, 0.5);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT4, sampler_LinearClamp, uv).rgb, 0.4);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT5, sampler_LinearClamp, uv).rgb, 0.3);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT6, sampler_LinearClamp, uv).rgb, 0.2);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT7, sampler_LinearClamp, uv).rgb, 0.1);
        #elif FRAMES_9
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT0, sampler_LinearClamp, uv).rgb, 0.81);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT1, sampler_LinearClamp, uv).rgb, 0.72);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT2, sampler_LinearClamp, uv).rgb, 0.63);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT3, sampler_LinearClamp, uv).rgb, 0.54);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT4, sampler_LinearClamp, uv).rgb, 0.45);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT5, sampler_LinearClamp, uv).rgb, 0.36);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT6, sampler_LinearClamp, uv).rgb, 0.27);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT7, sampler_LinearClamp, uv).rgb, 0.18);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT8, sampler_LinearClamp, uv).rgb, 0.09);
        #else
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT0, sampler_LinearClamp, uv).rgb, 0.83);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT1, sampler_LinearClamp, uv).rgb, 0.74);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT2, sampler_LinearClamp, uv).rgb, 0.66);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT3, sampler_LinearClamp, uv).rgb, 0.58);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT4, sampler_LinearClamp, uv).rgb, 0.50);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT5, sampler_LinearClamp, uv).rgb, 0.42);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT6, sampler_LinearClamp, uv).rgb, 0.33);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT7, sampler_LinearClamp, uv).rgb, 0.25);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT8, sampler_LinearClamp, uv).rgb, 0.16);
        pixel.rgb = lerp(pixel.rgb, SAMPLE_TEXTURE2D(_RT9, sampler_LinearClamp, uv).rgb, 0.08);
        #endif

        pixel = lerp(color, pixel, _Strength);

        // Color adjust.
        pixel.rgb = ColorAdjust(pixel.rgb);

        return lerp(color, pixel, _Intensity);
      }

      ENDHLSL
    }
  }
  
  FallBack "Diffuse"
}
