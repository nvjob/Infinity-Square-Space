// The foundation - Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)
// The modification was made by Nicholas Veselov (#NVJOB | https://nvjob.pro).


Shader "#NVJOB/Unlit/Billboard (Cull Off, ZWrite Off, Blend SrcAlpha OneMinusSrcAlpha, No Fog)" {


//========================================================================================================================


Properties {
_Color("Main Color", Color) = (1,1,1,1)
_MainTex("Texture", 2D) = "white" {}
}


//========================================================================================================================


SubShader {
Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" "DisableBatching" = "True" }
LOD 200
ZWrite Off
Blend SrcAlpha OneMinusSrcAlpha
Cull Off

Pass {
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0

#include "UnityCG.cginc"

struct appdata {
float4 vertex : POSITION;
float2 texcoord : TEXCOORD0;
};

struct v2f {
float2 texcoord : TEXCOORD0;
float4 vertex : SV_POSITION;
};

fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;

v2f vert(appdata v) {
v2f o;
o.vertex = UnityObjectToClipPos(v.vertex);
o.texcoord = v.texcoord.xy;
float3 vpos = mul((float3x3)unity_ObjectToWorld, v.vertex.xyz);
float4 worldCoord = float4(unity_ObjectToWorld._m03, unity_ObjectToWorld._m13, unity_ObjectToWorld._m23, 1);
float4 viewPos = mul(UNITY_MATRIX_V, worldCoord) + float4(vpos, 0);
float4 outPos = mul(UNITY_MATRIX_P, viewPos);
o.vertex = outPos;
return o;
}

fixed4 frag(v2f i) : SV_Target {
fixed4 col = tex2D(_MainTex, i.texcoord) * _Color;
return col;
}

ENDCG
}
}

//========================================================================================================================
}