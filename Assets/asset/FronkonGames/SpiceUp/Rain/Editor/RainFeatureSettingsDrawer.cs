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
using UnityEngine;
using UnityEditor;
using static FronkonGames.SpiceUp.Rain.Inspector;

namespace FronkonGames.SpiceUp.Rain.Editor
{
  /// <summary> Spice up Rain inspector. </summary>
  [CustomPropertyDrawer(typeof(Rain.Settings))]
  public class RainFeatureSettingsDrawer : Drawer
  {
    private Rain.Settings settings;

    protected override void InspectorGUI()
    {
      settings ??= GetSettings<Rain.Settings>();

      /////////////////////////////////////////////////
      // Common.
      /////////////////////////////////////////////////
      settings.intensity = Slider("Intensity", "Controls the intensity of the effect [0, 1]. Default 1.", settings.intensity, 0.0f, 1.0f, 1.0f);

      /////////////////////////////////////////////////
      // Rain.
      /////////////////////////////////////////////////
      Separator();

      Label("Droplets");
      {
        IndentLevel++;

        settings.amount = Slider("Amount", "Rain amount.", settings.amount, 0.0f, 1.0f, 0.5f);
        settings.size = Slider("Size", "Droplet size.", settings.size, 0.1f, 2.0f, 1.0f);
        settings.speed = Slider("Speed", "Droplet speed.", settings.speed, 0.0f, 3.0f, 0.25f);
        settings.rotation = Slider("Rotation", "Droplet rotation.", settings.rotation, -180.0f, 180.0f, 0.0f);
        settings.distortion = Slider("Distortion", "Droplet distortion.", settings.distortion, 0.0f, 1.0f, 0.5f);

        IndentLevel--;
      }

      Label("Color");
      {
        IndentLevel++;

        settings.blend = (ColorBlends)EnumPopup("Blend", "Color blend operation. Default Solid.", settings.blend, ColorBlends.Solid);
        settings.color = ColorField("Droplet", "Droplet color.", settings.color, Color.white);
        settings.ambient = ColorField("Ambient", "Ambient color.", settings.ambient, Rain.Settings.AmbientColorDefault);
        settings.tintTrails = Toggle("Tint trails", "Tint trails.", settings.tintTrails);

        IndentLevel--;
      }

      Label("Layers");
      {
        IndentLevel++;

        settings.staticLayer = Slider("Static", "Static droplets layer.", settings.staticLayer, 0.0f, 1.0f, 1.0f);
        settings.dynamicLayer0 = Slider("Dynamic #0", "Dynamic droplets layer 0.", settings.dynamicLayer0, 0.0f, 1.0f, 1.0f);
        settings.dynamicLayer1 = Slider("Dynamic #1", "Dynamic droplets layer 0.", settings.dynamicLayer1, 0.0f, 1.0f, 1.0f);

        IndentLevel--;
      }

      Separator();

      /////////////////////////////////////////////////
      // Color.
      /////////////////////////////////////////////////
      Separator();

      if (Foldout("Color") == true)
      {
        IndentLevel++;

        settings.brightness = Slider("Brightness", "Brightness [-1.0, 1.0]. Default 0.", settings.brightness, -1.0f, 1.0f, 0.0f);
        settings.contrast = Slider("Contrast", "Contrast [0.0, 10.0]. Default 1.", settings.contrast, 0.0f, 10.0f, 1.0f);
        settings.gamma = Slider("Gamma", "Gamma [0.1, 10.0]. Default 1.", settings.gamma, 0.01f, 10.0f, 1.0f);
        settings.hue = Slider("Hue", "The color wheel [0.0, 1.0]. Default 0.", settings.hue, 0.0f, 1.0f, 0.0f);
        settings.saturation = Slider("Saturation", "Intensity of a colors [0.0, 2.0]. Default 1.", settings.saturation, 0.0f, 2.0f, 1.0f);

        IndentLevel--;
      }

      /////////////////////////////////////////////////
      // Advanced.
      /////////////////////////////////////////////////
      Separator();

      if (Foldout("Advanced") == true)
      {
        IndentLevel++;

        settings.affectSceneView = Toggle("Affect the Scene View?", "Does it affect the Scene View?", settings.affectSceneView);
        settings.filterMode = (FilterMode)EnumPopup("Filter mode", "Filter mode. Default Bilinear.", settings.filterMode, FilterMode.Bilinear);
        settings.whenToInsert = (UnityEngine.Rendering.Universal.RenderPassEvent)EnumPopup("RenderPass event",
          "Render pass injection. Default BeforeRenderingPostProcessing.",
          settings.whenToInsert,
          UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing);
        settings.enableProfiling = Toggle("Enable profiling", "Enable render pass profiling", settings.enableProfiling);

        IndentLevel--;
      }

      /////////////////////////////////////////////////
      // Misc.
      /////////////////////////////////////////////////
      Separator();

      BeginHorizontal();
      {
        if (MiniButton("documentation", "Online documentation") == true)
          Application.OpenURL(Constants.Support.Documentation);

        if (MiniButton("support", "Do you have any problem or suggestion?") == true)
          SupportWindow.ShowWindow();

        if (EditorPrefs.GetBool($"{Constants.Asset.AssemblyName}.Review") == false)
        {
          Separator();

          if (MiniButton("write a review <color=#800000>❤️</color>", "Write a review, thanks!") == true)
          {
            Application.OpenURL(Constants.Support.Store);

            EditorPrefs.SetBool($"{Constants.Asset.AssemblyName}.Review", true);
          }
        }

        FlexibleSpace();

        if (Button("Reset") == true)
          settings.ResetDefaultValues();
      }
      EndHorizontal();
    }
  }
}
