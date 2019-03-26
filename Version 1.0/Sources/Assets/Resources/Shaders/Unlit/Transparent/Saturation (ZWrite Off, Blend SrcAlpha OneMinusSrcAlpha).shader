// The foundation - Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)
// The modification was made by Nicholas Veselov (#NVJOB | https://nvjob.pro).


Shader "#NVJOB/Unlit/Transparent/Saturation (ZWrite Off, Blend SrcAlpha OneMinusSrcAlpha)" {


//========================================================================================================================


Properties{
_Color("Water Color (RGB) Transparency (A)", COLOR) = (1, 1, 1, 0.5)
_MainTex("Base (RGB)", 2D) = "white" {}
_Saturation("Saturation", Range(0, 1)) = 1
}


//========================================================================================================================


SubShader{
Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
Blend SrcAlpha OneMinusSrcAlpha
LOD 200
ZWrite Off

Pass {
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0
#pragma multi_compile_fog

#include "UnityCG.cginc"

struct appdata_t {
float4 vertex : POSITION;
float2 texcoord : TEXCOORD0;
UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct v2f {
float4 vertex : SV_POSITION;
float2 texcoord : TEXCOORD0;
UNITY_FOG_COORDS(1)
UNITY_VERTEX_OUTPUT_STEREO
};

fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;
half _Saturation;

v2f vert(appdata_t v) {
v2f o;
UNITY_SETUP_INSTANCE_ID(v);
UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
o.vertex = UnityObjectToClipPos(v.vertex);
o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
UNITY_TRANSFER_FOG(o,o.vertex);
return o;
}

fixed4 frag(v2f i) : SV_Target {
fixed4 col = tex2D(_MainTex, i.texcoord) * _Color;
float Lum = dot(col, float3(0.2126, 0.7152, 0.0722));
fixed4 color = fixed4(lerp(Lum.xxx, col, _Saturation), col.a);
UNITY_APPLY_FOG(i.fogCoord, color);
return color;
}

ENDCG
}
}


//========================================================================================================================
}
