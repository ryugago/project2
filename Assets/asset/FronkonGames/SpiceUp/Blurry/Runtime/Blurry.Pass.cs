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
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace FronkonGames.SpiceUp.Blurry
{
  ///------------------------------------------------------------------------------------------------------------------
  /// <summary> Render Pass. </summary>
  /// <remarks> Only available for Universal Render Pipeline. </remarks>
  ///------------------------------------------------------------------------------------------------------------------
  public sealed partial class Blurry
  {
    public const int MaxFrames = 10;

    private sealed class RenderPass : ScriptableRenderPass
    {
      private readonly Settings settings;

      private RenderTargetIdentifier colorBuffer;
      private RenderTextureDescriptor renderTextureDescriptor;

#if UNITY_2022_1_OR_NEWER
      RTHandle renderTextureHandle0;

      private readonly ProfilingSampler profilingSamples = new(Constants.Asset.AssemblyName);
      private ProfilingScope profilingScope;
#else
      private int renderTextureHandle0;
#endif
      private readonly Material material;

      private static readonly ProfilerMarker ProfilerMarker = new($"{Constants.Asset.AssemblyName}.Pass.Execute");

      private const string CommandBufferName = Constants.Asset.AssemblyName;

      private readonly List<RenderTexture> renderTextures = new();

      private int lastFrameAdded = -1;

      private static class ShaderIDs
      {
        public static readonly int Intensity = Shader.PropertyToID("_Intensity");

        public static readonly int Strength = Shader.PropertyToID("_Strength");
        public static readonly int[] RenderTargets = new int[MaxFrames]
        {
          Shader.PropertyToID("_RT0"),
          Shader.PropertyToID("_RT1"),
          Shader.PropertyToID("_RT2"),
          Shader.PropertyToID("_RT3"),
          Shader.PropertyToID("_RT4"),
          Shader.PropertyToID("_RT5"),
          Shader.PropertyToID("_RT6"),
          Shader.PropertyToID("_RT7"),
          Shader.PropertyToID("_RT8"),
          Shader.PropertyToID("_RT9"),
        };

        public static readonly int Brightness = Shader.PropertyToID("_Brightness");
        public static readonly int Contrast = Shader.PropertyToID("_Contrast");
        public static readonly int Gamma = Shader.PropertyToID("_Gamma");
        public static readonly int Hue = Shader.PropertyToID("_Hue");
        public static readonly int Saturation = Shader.PropertyToID("_Saturation");
      }

      /// <summary> Render pass constructor. </summary>
      public RenderPass(Settings settings)
      {
        this.settings = settings;

        string shaderPath = $"Shaders/{Constants.Asset.ShaderName}_URP";
        Shader shader = Resources.Load<Shader>(shaderPath);
        if (shader != null)
        {
          if (shader.isSupported == true)
          {
            material = CoreUtils.CreateEngineMaterial(shader);
          }
          else
            Log.Warning($"'{shaderPath}.shader' not supported");
        }
      }

      /// <inheritdoc/>
      public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
      {
        renderTextureDescriptor = renderingData.cameraData.cameraTargetDescriptor;
        renderTextureDescriptor.depthBufferBits = 0;

#if UNITY_2022_1_OR_NEWER
        colorBuffer = renderingData.cameraData.renderer.cameraColorTargetHandle;

        RenderingUtils.ReAllocateIfNeeded(ref renderTextureHandle0, renderTextureDescriptor, settings.filterMode, TextureWrapMode.Clamp, false, 1, 0, $"_RTHandle0_{Constants.Asset.Name}");
#else
        colorBuffer = renderingData.cameraData.renderer.cameraColorTarget;

        renderTextureHandle0 = Shader.PropertyToID($"_RTHandle0_{Constants.Asset.Name}");
        cmd.GetTemporaryRT(renderTextureHandle0, renderTextureDescriptor.width, renderTextureDescriptor.height, renderTextureDescriptor.depthBufferBits, settings.filterMode, RenderTextureFormat.ARGB32);
#endif
      }

      /// <inheritdoc/>
      public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
      {
        if (material == null ||
            renderingData.postProcessingEnabled == false ||
            settings.intensity == 0.0f ||
            settings.strength == 0.0f ||
            settings.affectSceneView == false && renderingData.cameraData.isSceneViewCamera == true)
          return;

        CommandBuffer cmd = CommandBufferPool.Get(CommandBufferName);

        if (settings.enableProfiling == true)
#if UNITY_2022_1_OR_NEWER
          profilingScope = new ProfilingScope(cmd, profilingSamples);
#else
          ProfilerMarker.Begin();
#endif  

        if (renderTextures.Count > 0)
        {
          if (lastFrameAdded != Time.frameCount)
          {
            if (settings.frameStep == 0 || (Time.frameCount - lastFrameAdded >= settings.frameStep))
            {
              lastFrameAdded = Time.frameCount;

              RenderTexture renderTexture = RenderTexture.GetTemporary((int)(Screen.width * settings.frameResolution),
                                                                       (int)(Screen.height * settings.frameResolution), 0);
#if UNITY_2022_1_OR_NEWER
              cmd.Blit(colorBuffer, renderTexture);
#else
              Blit(cmd, colorBuffer, renderTexture);
#endif
              renderTextures.Insert(0, renderTexture);

              if (renderTextures.Count >= MaxFrames)
              {
                RenderTexture.ReleaseTemporary(renderTextures[^1]);
                renderTextures.RemoveAt(renderTextures.Count - 1);
              }
            }
          }
        }
        else
        {
          lastFrameAdded = Time.frameCount;

          for (int i = 0; i < MaxFrames; ++i)
          {
            RenderTexture renderTexture = RenderTexture.GetTemporary((int)(Screen.width * settings.frameResolution),
                                                                     (int)(Screen.height * settings.frameResolution), 0);
#if UNITY_2022_1_OR_NEWER
            cmd.Blit(colorBuffer, renderTexture);
#else
            Blit(cmd, colorBuffer, renderTexture);
#endif
            renderTextures.Insert(0, renderTexture);
          }
        }

        material.shaderKeywords = null;
        material.SetFloat(ShaderIDs.Intensity, settings.intensity);

        material.SetFloat(ShaderIDs.Strength, settings.strength);
        material.EnableKeyword($"FRAMES_{settings.frames + 1}");

        for (int i = 0; i < settings.frames && i < renderTextures.Count; ++i)
          material.SetTexture(ShaderIDs.RenderTargets[i], renderTextures[i]);

        material.SetFloat(ShaderIDs.Brightness, settings.brightness);
        material.SetFloat(ShaderIDs.Contrast, settings.contrast);
        material.SetFloat(ShaderIDs.Gamma, 1.0f / settings.gamma);
        material.SetFloat(ShaderIDs.Hue, settings.hue);
        material.SetFloat(ShaderIDs.Saturation, settings.saturation);

#if UNITY_2022_1_OR_NEWER
        cmd.Blit(colorBuffer, renderTextureHandle0, material);
        cmd.Blit(renderTextureHandle0, colorBuffer);
#else
        Blit(cmd, colorBuffer, renderTextureHandle0, material);
        Blit(cmd, renderTextureHandle0, colorBuffer);
#endif

        if (settings.enableProfiling == true)
#if UNITY_2022_1_OR_NEWER
          profilingScope.Dispose();
#else
          ProfilerMarker.End();
#endif

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
      }

      internal void Dispose()
      {
        for (int i = 0; i < renderTextures.Count; ++i)
          RenderTexture.ReleaseTemporary(renderTextures[i]);

        renderTextures.Clear();
      }
    }
  }
}
