using UnityEngine;
using UnityEngine.Rendering;
using FronkonGames.SpiceUp.Blurry;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(BlurryDemo))]
public class DemoWarning : Editor
{
  private GUIStyle Style => style ?? new GUIStyle(GUI.skin.GetStyle("HelpBox")) { richText = true, fontSize = 14, alignment = TextAnchor.MiddleCenter };
  private GUIStyle style;
  public override void OnInspectorGUI() =>
    EditorGUILayout.TextArea($"\nThis code is only for the demo\n\n<b>DO NOT USE</b> it in your projects\n\nIf you have any questions,\ncheck the <a href='{Constants.Support.Documentation}'>online help</a> or use the <a href='mailto:{Constants.Support.Email}'>support email</a>,\n<b>thanks!</b>\n", Style);
}
#endif

/// <summary> Spice Up: Blurry demo. </summary>
/// <remarks>
/// This code is designed for a simple demo, not for production environments.
/// </remarks>
public class BlurryDemo : MonoBehaviour
{
  [Space, SerializeField]
  private RenderPipelineAsset overrideRenderPipelineAsset;

  private RenderPipelineAsset defaultRenderPipelineAsset;
  private Blurry.Settings settings;

  private GUIStyle styleTitle;
  private GUIStyle styleLabel;
  private GUIStyle styleButton;

  private void Start()
  {
    defaultRenderPipelineAsset = GraphicsSettings.currentRenderPipeline;

    GraphicsSettings.defaultRenderPipeline = overrideRenderPipelineAsset;
    QualitySettings.renderPipeline = overrideRenderPipelineAsset;

    if (Blurry.IsInRenderFeatures() == false)
      Blurry.AddRenderFeature();

    settings = Blurry.GetSettings();
    ResetBlurry();
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

    GUILayout.BeginHorizontal("box", GUILayout.Width(350.0f), GUILayout.Height(Screen.height));
    {
      const float space = 20.0f;

      GUILayout.BeginVertical(GUILayout.Width(space));
      GUILayout.FlexibleSpace();
      GUILayout.EndVertical();

      GUILayout.BeginVertical();
      {
        GUILayout.Space(space);

        GUILayout.Label("BLURRY DEMO", styleTitle);

        GUILayout.Space(space * 2.0f);

        settings.strength = settings.intensity = Slider("Intensity", settings.intensity);

        GUILayout.Space(space);

        settings.frames = Slider("Frames", settings.frames, 0, 10);
        settings.frameStep = Slider("Steps", settings.frameStep, 0, 10);
        settings.frameResolution = Slider("Resolution", settings.frameResolution, 0.1f, 1.0f);

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("RESET", styleButton) == true)
          ResetBlurry();

        GUILayout.Space(space);
      }
      GUILayout.EndVertical();

      GUILayout.BeginVertical(GUILayout.Width(space));
      GUILayout.FlexibleSpace();
      GUILayout.EndVertical();
    }
    GUILayout.EndHorizontal();
  }

  private void ResetBlurry()
  {
    settings.ResetDefaultValues();
    settings.frames = 10;
    settings.frameStep = 10;
    settings.frameResolution = 0.5f;
  }

  private void OnDestroy()
  {
    settings.ResetDefaultValues();

    GraphicsSettings.defaultRenderPipeline = defaultRenderPipelineAsset;
    QualitySettings.renderPipeline = defaultRenderPipelineAsset;
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
}
