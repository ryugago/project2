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
using UnityEngine.Rendering.Universal;

namespace FronkonGames.SpiceUp.DoubleVision
{
  ///------------------------------------------------------------------------------------------------------------------
  /// <summary>
  ///        ___     ___   __ __  ____   _        ___      __ __  ____ _____ ____  ___   ____
  ///       |   \   /   \ |  |  ||    \ | |      /  _]    |  |  ||    / ___/|    |/   \ |    \
  ///       |    \ |     ||  |  ||  o  )| |     /  [_     |  |  | |  (   \_  |  ||     ||  _  |
  ///       |  D  ||  O  ||  |  ||     || |___ |    _]    |  |  | |  |\__  | |  ||  O  ||  |  |
  ///       |     ||     ||  :  ||  O  ||     ||   [_     |  :  | |  |/  \ | |  ||     ||  |  |
  ///       |     ||     ||     ||     ||     ||     |     \   /  |  |\    | |  ||     ||  |  |
  ///       |_____| \___/  \__,_||_____||_____||_____|      \_/  |____|\___||____|\___/ |__|__|
  ///                           Dual vision effect with steroids
  ///                    https://fronkongames.github.io/store/spiceup.html
  /// </summary>
  /// <remarks> Only available for Universal Render Pipeline. </remarks>
  ///------------------------------------------------------------------------------------------------------------------
  public sealed partial class DoubleVision : ScriptableRendererFeature
  {
    // MUST be named "settings" (lowercase) to be shown in the Render Features inspector.
    public Settings settings = new();

    private RenderPass renderPass;

    /// <summary> Initializes this feature's resources. </summary>
    public override void Create() => renderPass = new RenderPass(settings);

    /// <summary> Injects one or multiple ScriptableRenderPass in the renderer. Called every frame once per camera. </summary>
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
      renderPass.renderPassEvent = settings.whenToInsert;

      renderer.EnqueuePass(renderPass);
    }
  }
}
