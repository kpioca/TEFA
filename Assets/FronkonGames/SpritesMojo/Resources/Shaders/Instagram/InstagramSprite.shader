///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Fronkon Games @FronkonGames <fronkongames@gmail.com>. All rights reserved.
//
// THIS FILE CAN NOT BE HOSTED IN PUBLIC REPOSITORIES.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
Shader "Fronkon Games/Sprites Mojo/Instagram"
{
  Properties
  {
    [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
    _Color("Tint", Color) = (1.0, 1.0, 1.0, 1.0)

    [MaterialToggle] PixelSnap("Pixel snap", Float) = 0.0
    [HideInInspector] _RendererColor("RendererColor", Color) = (1.0, 1.0, 1.0, 1.0)
    [HideInInspector] _Flip("Flip", Vector) = (1.0, 1.0, 1.0, 1.0)
    [PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
    [PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0.0

    _ZWrite("Depth Write", Float) = 0.0
    _Cutoff("Depth alpha cutoff", Range(0.0, 1.0)) = 0.0
    _ShadowAlphaCutoff("Shadow alpha cutoff", Range(0.0, 1.0)) = 0.1
    _CustomRenderQueue("Custom Render Queue", Float) = 0.0

    [HideInInspector] _SrcBlend("__src", Float) = 1.0
    [HideInInspector] _DstBlend("__dst", Float) = 0.0
    [HideInInspector] _RenderQueue("__queue", Float) = 0.0
    [HideInInspector] _Cull("__cull", Float) = 0.0

    // Common.
    _Amount("Amount", Range(0.0, 1.0)) = 1.0

    // Color.
    _Brightness("Brightness", Range(-1.0, 1.0)) = 0.0
    _Contrast("Contrast", Range(-1.0, 1.0)) = 0.0
    _Gamma("Gamma", Range(0.01, 10.0)) = 1.0
    _Hue("Hue", Range(0.0, 1.0)) = 0.0
    _Saturation("Saturation", Range(0.0, 5.0)) = 1.0
    _Vibrance("Vibrance", Range(-1.0, 10.0)) = 0.0

    // Instagram.
    _Filter("Filter", Float) = 0
    _BW("Saturation", Range(-5.0, 5.0)) = 1.0
    _Slope("Slope", Vector) = (1.0, 1.0, 1.0, 1.0)
    _Offset("Offset", Vector) = (0.0, 0.0, 0.0, 0.0)
    _Power("Power", Vector) = (1.0, 1.0, 1.0, 1.0)
    _FilmContrast("FilmContrast", Float) = 0

  }

  SubShader
  {
    Tags
    {
      "Queue" = "Transparent"
      "IgnoreProjector" = "True"
      "RenderType" = "Transparent"
      "PreviewType" = "Plane"
      "CanUseSpriteAtlas" = "True"
      "AlphaDepth" = "False"
    }

    LOD 100

    Pass
    {
      Blend [_SrcBlend] [_DstBlend]
      Lighting Off
      ZWrite [_ZWrite]
      ZTest LEqual
      Cull [_Cull]
      Lighting Off

      CGPROGRAM
      #pragma shader_feature _ _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON _ADDITIVEBLEND _ADDITIVEBLEND_SOFT _MULTIPLYBLEND _MULTIPLYBLEND_X2
      #pragma shader_feature _ALPHA_CLIP
      #pragma shader_feature _COLOR_ADJUST
      #pragma shader_feature _FOG

      #pragma fragmentoption ARB_precision_hint_fastest
      #pragma multi_compile_fog

      #pragma vertex vert
      #pragma fragment frag

      #include "../SpritesMojo.cginc"
      ENDCG
    }
  }

  CustomEditor "FronkonGames.SpritesMojo.Editor.InstagramSpriteGUI"
  
  Fallback "Sprites/Default"
}