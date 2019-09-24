// The foundation - Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)
// The modification was made by Nicholas Veselov (#NVJOB | https://nvjob.pro).


Shader "#NVJOB/SkyBox/Two Colors" {


//========================================================================================================================


Properties {
_ColorA ("Color A", Color) = (0.0, 0.0, 0.0, 0)
_ColorB ("Color B", Color) = (0.0, 0.4, 0.4, 0)
_Direction ("Dir A", Vector) = (0.18, -1.64, -0.19, 0)
_Direction2 ("Dir B", Vector) = (1.42, -2.26, -0.50, 0)

}


//========================================================================================================================


CGINCLUDE
#include "UnityCG.cginc"

struct appdata {
float4 position : POSITION;
float3 texcoord : TEXCOORD0;
};

struct v2f {
float4 position : SV_POSITION;
float3 texcoord : TEXCOORD0;
float4 worldpos : any;
};

half4 _ColorA;
half4 _ColorB;
half3 _Direction;
half3 _Direction2;

v2f vert (appdata v) {
v2f o;
o.position = UnityObjectToClipPos(v.position);
o.texcoord = v.texcoord;
o.worldpos = o.position;
return o;
}

half4 frag (v2f i) : COLOR {
half d = dot(normalize(i.texcoord), _Direction) * 0.11f + 1.1f;
half d2 = dot(normalize(i.texcoord), _Direction2) * 0.11f + 1.1f;
float4 gradient = lerp (_ColorA, _ColorB, pow(d * d2, 2));

const float noiseScale = 240;
const half noiseIntensity = 1;
float2 wcoord = (i.worldpos.xy/i.worldpos.w) * noiseScale;
float4 dither = ( dot( float2( 171.0f, 231.0f ), wcoord.xy ) );
dither.rgb = frac( dither / float3( 103.0f, 71.0f, 97.0f ) ) - float3( 0.5f, 0.5f, 0.5f );
return gradient + (dither/255.0f) * noiseIntensity;
}

ENDCG


//========================================================================================================================


SubShader {
Tags { "RenderType"="Background" "Queue"="Background" }
Pass {
ZWrite Off
Cull Off
Fog { Mode Off }
CGPROGRAM
#pragma fragmentoption ARB_precision_hint_fastest
#pragma vertex vert
#pragma fragment frag
ENDCG
}
}


//========================================================================================================================
}