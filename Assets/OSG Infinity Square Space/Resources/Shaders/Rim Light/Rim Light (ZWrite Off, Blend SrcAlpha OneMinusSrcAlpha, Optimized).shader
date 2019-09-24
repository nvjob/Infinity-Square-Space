// The foundation - Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)
// The modification was made by Nicholas Veselov (#NVJOB | https://nvjob.pro).


Shader "#NVJOB/Rim Light/Rim Light (ZWrite Off, Blend SrcAlpha OneMinusSrcAlpha, Optimized)" {


//========================================================================================================================


Properties{
_RimValue("Rim value", Range(0, 10)) = 0.5
_Color("Color", Color) = (1, 1, 1, 1)
}


//========================================================================================================================


SubShader{
//======================

Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
Blend SrcAlpha OneMinusSrcAlpha
LOD 200
ZWrite Off

CGPROGRAM
#pragma surface surf Lambert alpha exclude_path:prepass nolightmap nometa noforwardadd nolppv noshadowmask halfasview interpolateview novertexlights

//======================

fixed _RimValue;
fixed4 _Color;

//======================

struct Input {
float3 viewDir;
float3 worldNormal;
};

//======================

void surf(Input IN, inout SurfaceOutput o) {
half4 c = _Color;
o.Albedo = c.rgb;
float3 normal = normalize(IN.worldNormal);
float3 dir = normalize(IN.viewDir);
float val = 1 - (abs(dot(dir, normal)));
float rim = val * val *  _RimValue;
o.Alpha = c.a * rim;
}

//======================
ENDCG
}


//========================================================================================================================
}