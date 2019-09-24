// The foundation - Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)
// The modification was made by Nicholas Veselov (#NVJOB | https://nvjob.pro).


Shader "#NVJOB/Particles/VertexLit Blended (Cull Off, ZWrite Off, No Fog, Lighting On, Blend SrcAlpha OneMinusSrcAlpha)" {


//========================================================================================================================


Properties {
_EmisColor ("Emissive Color", Color) = (.2,.2,.2,0)
_MainTex ("Particle Texture", 2D) = "white" {}
}


//========================================================================================================================


Category {
Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
Blend SrcAlpha OneMinusSrcAlpha
Cull Off 
ZWrite Off 
Fog {Mode Off}
Lighting On

Material { Emission [_EmisColor] }
ColorMaterial AmbientAndDiffuse

SubShader {
Pass {
SetTexture [_MainTex] {
combine texture * primary
}
}
}

}


//========================================================================================================================
}
