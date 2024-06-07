using System;
using UnityEngine;
using UnityEngine.Rendering;
using FronkonGames.SpiceUp.Rain;

/// <summary> Spice Up: Rain demo. </summary>
/// <remarks>
/// This code is designed for a simple demo, not for production environments.
/// </remarks>
public class RainDemo : MonoBehaviour
{
  [Space, SerializeField]
  private RenderPipelineAsset overrideRenderPipelineAsset;

  private RenderPipelineAsset defaultRenderPipelineAsset;
  private Rain.Settings settings;

  private GUIStyle styleTitle;
  private GUIStyle styleLabel;
  private GUIStyle styleButton;

  private void Start()
  {
    defaultRenderPipelineAsset = GraphicsSettings.currentRenderPipeline;

    GraphicsSettings.defaultRenderPipeline = overrideRenderPipelineAsset;
    QualitySettings.renderPipeline = overrideRenderPipelineAsset;

    if (Rain.IsInRenderFeatures() == false)
      Rain.AddRenderFeature();

    settings = Rain.GetSettings();
    ResetDemo();
  }

  private void OnGUI()
  {
    styleTitle = new GUIStyle(GUI.skin.label)
    {
      alignment = TextAnchor.LowerCenter,
      fontSize = 32,
      fontStyle = FontStyle.Bold
    };

    styleLabel = new GUIStyle(GUI.skin.label)
    {
      alignment = TextAnchor.UpperLeft,
      fontSize = 24
    };

    styleButton = new GUIStyle(GUI.skin.button)
    {
      fontSize = 24
    };

    GUILayout.BeginHorizontal("box", GUILayout.Width(450.0f), GUILayout.Height(Screen.height));
    {
      const float space = 20.0f;

      GUILayout.BeginVertical(GUILayout.Width(space));
      GUILayout.FlexibleSpace();
      GUILayout.EndVertical();

      GUILayout.BeginVertical();
      {
        GUILayout.Space(space);

        GUILayout.Label("RAIN DEMO", styleTitle);

        GUILayout.Space(space * 2.0f);

        settings.intensity = Slider("Intensity", settings.intensity);

        GUILayout.Space(space);

        settings.amount = Slider("Amount", settings.amount);
        settings.size = Slider("Size", settings.size, 0.1f, 2.0f);
        settings.distortion = Slider("Distortion", settings.distortion);
        settings.speed = Slider("Speed", settings.speed, 0.0f, 10.0f);
        settings.rotation = Slider("Rotation", settings.rotation, -180.0f, 180.0f);
        settings.blend = Enum("Blend", settings.blend);
        settings.tintTrails = Toogle("Trails", settings.tintTrails);
        settings.staticLayer = Slider("Static layer", settings.staticLayer);
        settings.dynamicLayer0 = Slider("Layer #0", settings.dynamicLayer0);
        settings.dynamicLayer1 = Slider("Layer #1", settings.dynamicLayer1);

        GUILayout.Space(space);

        if (GUILayout.Button("Light Rain", styleButton) == true)
          SetPreset(0);

        if (GUILayout.Button("Normal Rain", styleButton) == true)
          SetPreset(1);

        if (GUILayout.Button("Heavy Rain", styleButton) == true)
          SetPreset(2);

        if (GUILayout.Button("Bloody Rain", styleButton) == true)
          SetPreset(3);

        if (GUILayout.Button("Toxic Rain", styleButton) == true)
          SetPreset(4);

        if (GUILayout.Button("Oil Rain", styleButton) == true)
          SetPreset(5);

        GUILayout.FlexibleSpace();
      }
      GUILayout.EndVertical();

      GUILayout.BeginVertical(GUILayout.Width(space));
      GUILayout.FlexibleSpace();
      GUILayout.EndVertical();
    }
    GUILayout.EndHorizontal();
  }

  private void ResetDemo()
  {
    settings.ResetDefaultValues();

    SetPreset(0);
  }

  private void OnDestroy()
  {
    settings.ResetDefaultValues();

    GraphicsSettings.defaultRenderPipeline = defaultRenderPipelineAsset;
    QualitySettings.renderPipeline = defaultRenderPipelineAsset;
  }

  private void SetPreset(int preset)
  {
    settings.ResetDefaultValues();

    switch (preset)
    {
      case 0:
        settings.amount = 1.0f;
        settings.size = 2.0f;
        settings.speed = 0.05f;
        settings.distortion = 0.1f;
        settings.blend = ColorBlends.Color;
        settings.ambient = Color.white;
        settings.staticLayer = 0.75f;
        settings.dynamicLayer0 = 0.25f;
        settings.dynamicLayer1 = 0.0f;
        break;

      case 1:
        settings.amount = 1.0f;
        settings.blend = ColorBlends.Color;
        break;

      case 2:
        settings.amount = 1.0f;
        settings.size = 1.5f;
        settings.speed = 1.5f;
        settings.distortion = 1.0f;
        settings.blend = ColorBlends.Color;
        break;

      case 3:
        settings.amount = 0.55f;
        settings.size = 1.5f;
        settings.speed = 0.02f;
        settings.distortion = 1.0f;
        settings.blend = ColorBlends.Dodge;
        settings.tintTrails = true;
        settings.staticLayer = 0.0f;
        settings.ambient = new Color(1.0f, 0.8f, 0.8f);
        settings.color = Color.red;
        break;

      case 4:
        settings.amount = 0.3f;
        settings.size = 2.0f;
        settings.speed = 0.075f;
        settings.distortion = 0.5f;
        settings.rotation = 180.0f;
        settings.blend = ColorBlends.Divide;
        settings.ambient = new Color(0.8f, 1.0f, 0.8f);
        settings.color = Color.green;
        break;

      case 5:
        settings.amount = 0.6f;
        settings.size = 1.5f;
        settings.speed = 0.02f;
        settings.distortion = 0.0f;
        settings.blend = ColorBlends.Burn;
        settings.tintTrails = true;
        settings.ambient = new Color(1.0f, 0.6f, 0.0f);
        settings.color = Color.black;
        break;
    }
  }

  private bool Toogle(string label, bool value)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Label(label, styleLabel);

      value = GUILayout.Toggle(value, string.Empty);
    }
    GUILayout.EndHorizontal();

    return value;
  }

  private float Slider(string label, float value, float min = 0.0f, float max = 1.0f)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Label(label, styleLabel);

      value = GUILayout.HorizontalSlider(value, min, max);
    }
    GUILayout.EndHorizontal();

    return value;
  }

  private int Slider(string label, int value, int min, int max)
  {
    GUILayout.BeginHorizontal();
    {
      GUILayout.Label(label, styleLabel);

      value = (int)GUILayout.HorizontalSlider(value, min, max);
    }
    GUILayout.EndHorizontal();

    return value;
  }

  private Vector3 Vector3(string label, Vector3 value, string x = "X", string y = "Y", string z = "Z", float min = 0.0f, float max = 1.0f)
  {
    GUILayout.Label(label, styleLabel);

    value.x = Slider($"   {x}", value.x, min, max);
    value.y = Slider($"   {y}", value.y, min, max);
    value.z = Slider($"   {z}", value.z, min, max);

    return value;
  }

  private T Enum<T>(string label, T value) where T : Enum
  {
    string[] names = System.Enum.GetNames(typeof(T));
    Array values = System.Enum.GetValues(typeof(T));
    int index = Array.IndexOf(values, value);

    GUILayout.BeginHorizontal();
    {
      GUILayout.Label(label, styleLabel);

      if (GUILayout.Button("<", styleButton) == true)
        index = index > 0 ? index - 1 : values.Length - 1;

      GUILayout.Label(names[index], styleLabel);

      if (GUILayout.Button(">", styleButton) == true)
        index = index < values.Length - 1 ? index + 1 : 0;
    }
    GUILayout.EndHorizontal();

    return (T)(object)index;
  }
}
