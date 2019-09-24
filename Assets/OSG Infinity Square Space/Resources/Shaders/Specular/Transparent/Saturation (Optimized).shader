// The foundation - Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)
// The modification was made by Nicholas Veselov (#NVJOB | https://nvjob.pro).


Shader "#NVJOB/Specular/Transparent/Saturation (Optimized)" {


//========================================================================================================================


Properties {
_Color ("Main Color", Color) = (1,1,1,1)
_SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
_MainTex ("Base (RGB)", 2D) = "white" {}
_Shininess("Shininess", Range(0, 1)) = 1
_Saturation("Saturation", Range(0, 1)) = 1
_Brightness("Brightness", Range(0, 1)) = 1
_Contrast("Contrast", Range(0, 1)) = 1
}


//========================================================================================================================


SubShader {
Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
LOD 200


CGPROGRAM
#pragma surface surf BlinnPhong alpha:fade exclude_path:prepass nolightmap nometa nolppv noshadowmask noforwardadd halfasview interpolateview novertexlights

//======================

sampler2D _MainTex;
fixed4 _Color;
half _Shininess, _Saturation, _Contrast, _Brightness;

//======================

struct Input {
float2 uv_MainTex;
};

//======================

void surf (Input IN, inout SurfaceOutput o) {
fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
float Lum = dot(c, float3(0.2126, 0.7152, 0.0722));
half3 color = lerp(Lum.xxx, c, _Saturation);
color = color * _Brightness;
o.Albedo = (color - 0.5) * _Contrast + 0.5;
o.Alpha = c.a;
o.Gloss = c.a;
o.Specular = _Shininess;
}

//======================
ENDCG
}


//========================================================================================================================

Fallback "Legacy Shaders/VertexLit"
}
