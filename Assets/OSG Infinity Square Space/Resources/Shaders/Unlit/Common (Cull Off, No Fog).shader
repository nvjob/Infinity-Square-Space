// The foundation - Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)
// The modification was made by Nicholas Veselov (#NVJOB | https://nvjob.pro).


Shader "#NVJOB/Unlit/Common (Cull Off, No Fog)" {


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

Pass {  
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0
			
#include "UnityCG.cginc"

struct appdata_t {
float4 vertex : POSITION;
float2 texcoord : TEXCOORD0;
UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct v2f {
float4 vertex : SV_POSITION;
float2 texcoord : TEXCOORD0;
UNITY_VERTEX_OUTPUT_STEREO
};

fixed4 _Color;
sampler2D _MainTex;
float4 _MainTex_ST;

v2f vert (appdata_t v) {
v2f o;
UNITY_SETUP_INSTANCE_ID(v);
UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
o.vertex = UnityObjectToClipPos(v.vertex);
o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
return o;
}

fixed4 frag (v2f i) : SV_Target {
fixed4 col = tex2D(_MainTex, i.texcoord) * _Color;
UNITY_OPAQUE_ALPHA(col.a);
return col;
}

ENDCG
}
}


//========================================================================================================================
}
