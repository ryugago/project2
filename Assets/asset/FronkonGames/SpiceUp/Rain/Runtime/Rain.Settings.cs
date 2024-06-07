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
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FronkonGames.SpiceUp.Rain
{
  ///------------------------------------------------------------------------------------------------------------------
  /// <summary> Settings. </summary>
  /// <remarks> Only available for Universal Render Pipeline. </remarks>
  ///------------------------------------------------------------------------------------------------------------------
  public sealed partial class Rain
  {
    /// <summary> Settings. </summary>
    [Serializable]
    public sealed class Settings
    {
      public Settings() => ResetDefaultValues();

#region Common settings.
      /// <summary> Controls the intensity of the effect [0, 1]. Default 1. </summary>
      /// <remarks> An effect with Intensity equal to 0 will not be executed. </remarks>
      public float intensity = 1.0f;
#endregion

#region Rain settings.
      /// <summary> Rain amount [0, 1]. Default 0. </summary>
      public float amount = 0.0f;

      /// <summary> Droplet size [0.1, 2]. Default 1. </summary>
      public float size = 1.0f;

      /// <summary> Droplet distortion [0, 1]. Default 0.5. </summary>
      public float distortion = 0.5f;

      /// <summary> Droplet speed [0.0, ...]. Default 0.25. </summary>
      public float speed = 0.25f;

      /// <summary> Rotation [-180, 180]. Default 0. </summary>
      public float rotation = 0.25f;

      /// <summary> Droplet color. </summary>
      public Color color = Color.white;

      /// <summary> Ambient color. Alpha controls the intensity. </summary>
      public Color ambient = AmbientColorDefault;

      /// <summary> Color blend operation. Default 'Solid'. </summary>
      public ColorBlends blend = ColorBlends.Solid;

      /// <summary> Tint droplet trails. </summary>
      public bool tintTrails = false;

      /// <summary> Static layer intensity [0, 1]. Default 1. </summary>
      public float staticLayer = 1.0f;

      /// <summary> Dynamic layer 0 intensity [0, 1]. Default 1. </summary>
      public float dynamicLayer0 = 1.0f;

      /// <summary> Dynamic layer 1 intensity [0, 1]. Default 1. </summary>
      public float dynamicLayer1 = 1.0f;

      public static readonly Color AmbientColorDefault = new(0.8f, 0.9f, 1.0f, 0.95f);
#endregion

#region Color settings.
      /// <summary> Brightness [-1.0, 1.0]. Default 0. </summary>
      public float brightness = 0.0f;

      /// <summary> Contrast [0.0, 10.0]. Default 1. </summary>
      public float contrast = 1.0f;

      /// <summary>Gamma [0.1, 10.0]. Default 1. </summary>
      public float gamma = 1.0f;

      /// <summary> The color wheel [0.0, 1.0]. Default 0. </summary>
      public float hue = 0.0f;

      /// <summary> Intensity of a colors [0.0, 2.0]. Default 1. </summary>
      public float saturation = 1.0f;
#endregion

#region Advanced settings.
      /// <summary> Does it affect the Scene View? </summary>
      public bool affectSceneView = false;

      /// <summary> Filter mode. Default Bilinear. </summary>
      public FilterMode filterMode = FilterMode.Bilinear;

      /// <summary> Render pass injection. Default BeforeRenderingPostProcessing. </summary>
      public RenderPassEvent whenToInsert = RenderPassEvent.BeforeRenderingPostProcessing;

      /// <summary> Enable render pass profiling. </summary>
      public bool enableProfiling = false;
#endregion

      /// <summary> Reset to default values. </summary>
      public void ResetDefaultValues()
      {
        intensity = 1.0f;

        amount = 0.0f;
        size = 1.0f;
        distortion = 0.5f;
        speed = 0.25f;
        rotation = 0.25f;
        color = Color.white;
        ambient = AmbientColorDefault;
        blend = ColorBlends.Solid;
        tintTrails = false;
        staticLayer = 1.0f;
        dynamicLayer0 = 1.0f;
        dynamicLayer1 = 1.0f;

        brightness = 0.0f;
        contrast = 1.0f;
        gamma = 1.0f;
        hue = 0.0f;
        saturation = 1.0f;

        affectSceneView = false;
        filterMode = FilterMode.Bilinear;
        whenToInsert = RenderPassEvent.BeforeRenderingPostProcessing;
        enableProfiling = false;
      }
    }
  }
}
