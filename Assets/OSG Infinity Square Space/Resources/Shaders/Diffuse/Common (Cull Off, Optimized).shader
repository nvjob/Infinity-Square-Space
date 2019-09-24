// The foundation - Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)
// The modification was made by Nicholas Veselov (#NVJOB | https://nvjob.pro).


Shader "#NVJOB/Diffuse/Common (Cull Off, Optimized)" {


//========================================================================================================================


Properties {
_Color ("Main Color", Color) = (1,1,1,1)
_MainTex ("Base (RGB)", 2D) = "white" {}
}


//========================================================================================================================


SubShader {
Tags { "RenderType"="Opaque" }
LOD 200
Cull Off

CGPROGRAM
#pragma surface surf Lambert exclude_path:prepass nolightmap nometa nolppv noshadowmask noforwardadd halfasview interpolateview novertexlights

//======================

sampler2D _MainTex;
fixed4 _Color;

//======================

struct Input {
float2 uv_MainTex;
};

//======================

void surf (Input IN, inout SurfaceOutput o) {
fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
o.Albedo = c.rgb;
o.Alpha = c.a;
}

//======================
ENDCG
}


//========================================================================================================================

Fallback "Legacy Shaders/VertexLit"
}
