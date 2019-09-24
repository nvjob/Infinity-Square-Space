// The foundation - Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)
// The modification was made by Nicholas Veselov (#NVJOB | https://nvjob.pro).


Shader "#NVJOB/Rim Light/Blackhole (Cull Off, Blend SrcAlpha OneMinusSrcAlpha)" {


//========================================================================================================================


Properties{
_DistortionStrength("Distortion Strength", Range(0, 10)) = 3
_HoleSize("Hole Size", Range(0, 1)) = 0.7
_HoleEdgeSmoothness("Hole Edge Smoothness", Range(0.001, 1.0)) = 1
}


//========================================================================================================================


SubShader{
//======================

Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" }
Blend SrcAlpha OneMinusSrcAlpha
LOD 200
Cull Off
GrabPass{ }

//======================

Pass {
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

uniform sampler2D _GrabTexture;
uniform float _DistortionStrength;
uniform float _HoleSize;
uniform float _HoleEdgeSmoothness;

struct VertexInput {
float4 vertex : POSITION;
float3 normal : NORMAL;
};

struct VertexOutput {
float4 pos : SV_POSITION;
float4 posWorld : TEXCOORD0;
float3 normalDir : TEXCOORD1;
float4 projPos : TEXCOORD2;
};

VertexOutput vert(VertexInput v) {
VertexOutput o = (VertexOutput)0;
o.normalDir = UnityObjectToWorldNormal(v.normal);
o.posWorld = mul(unity_ObjectToWorld, v.vertex);
o.pos = UnityObjectToClipPos(v.vertex);
o.projPos = ComputeScreenPos(o.pos);
COMPUTE_EYEDEPTH(o.projPos.z);
return o;
}

float4 frag(VertexOutput i) : COLOR {
i.normalDir = normalize(i.normalDir);
float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
float3 normalDirection = i.normalDir;
float2 sceneUVs = (i.projPos.xy / i.projPos.w);
float hs = (_HoleSize * -1.0 + 1.0);
float smooth = smoothstep((hs + _HoleEdgeSmoothness), (hs - _HoleEdgeSmoothness), (1.0 - pow(1.0 - max(0, dot(normalDirection, viewDirection)), 0.15)));
float3 cf = (smooth * tex2D(_GrabTexture, ((pow((1.0 - pow(1.0 - max(0, dot(normalDirection, viewDirection)), _DistortionStrength)), 6.0) * (sceneUVs.rg * -2.0 + 1.0)) + sceneUVs.rg)).rgb);
return fixed4(cf, 1);
}

ENDCG
}

//======================
}


FallBack "Diffuse"
//========================================================================================================================
}