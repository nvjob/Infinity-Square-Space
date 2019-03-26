// The foundation - Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)
// The modification was made by Nicholas Veselov (#NVJOB | https://nvjob.pro).


Shader "#NVJOB/Particles/Additive (Cull Off , No Fog, Lighting Off, ZWrite Off)" {


//========================================================================================================================


Properties {
_MainTex ("Particle Texture", 2D) = "white" {}
}


//========================================================================================================================


Category {
Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
Blend SrcAlpha One
Cull Off 
Lighting Off 
ZWrite Off 
Fog {Mode Off}

BindChannels {
Bind "Color", color
Bind "Vertex", vertex
Bind "TexCoord", texcoord
}

SubShader {
Pass {
SetTexture [_MainTex] { combine texture * primary }
}
}

}


//========================================================================================================================
}
