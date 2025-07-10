Shader "Battlehub/Particles/HB_Alpha Blended"
{
  Properties
  {
    _TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
    _MainTex ("Particle Texture", 2D) = "white" {}
    _InvFade ("Soft Particles Factor", Range(0.01, 3)) = 1
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "PreviewType" = "Plane"
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZWrite Off
      Cull Off
      Blend SrcAlpha OneMinusSrcAlpha
      ColorMask RGB
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      uniform float _Curvature;
      uniform float _HorizonYOffset;
      uniform float _HorizonZOffset;
      uniform float _Flatten;
      uniform float4 _HBWorldSpaceCameraPos;
      uniform float4 _MainTex_ST;
      uniform sampler2D _MainTex;
      uniform float4 _TintColor;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 color :COLOR;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float4 xlv_COLOR :COLOR;
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float4 xlv_COLOR :COLOR;
          float2 xlv_TEXCOORD0 :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float4 tmpvar_1;
          tmpvar_1 = mul(unity_ObjectToWorld, in_v.vertex);
          float4 tmpvar_2;
          tmpvar_2 = (tmpvar_1 - _HBWorldSpaceCameraPos);
          float tmpvar_3;
          tmpvar_3 = max(0, (abs((_HorizonZOffset - tmpvar_2.z)) - _Flatten));
          float tmpvar_4;
          tmpvar_4 = max(0, (abs((_HorizonYOffset - tmpvar_2.y)) - _Flatten));
          float4 tmpvar_5;
          tmpvar_5.yzw = float3(0, 0, 0);
          tmpvar_5.x = (((tmpvar_3 * tmpvar_3) + (tmpvar_4 * tmpvar_4)) * (-_Curvature));
          float4 tmpvar_6;
          tmpvar_6.w = 1;
          tmpvar_6.xyz = mul(unity_WorldToObject, (tmpvar_1 + tmpvar_5)).xyz;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_6));
          out_v.xlv_COLOR = in_v.color;
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 tmpvar_1;
          tmpvar_1 = ((2 * in_f.xlv_COLOR) * (_TintColor * tex2D(_MainTex, in_f.xlv_TEXCOORD0)));
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack "Particles/Alpha Blended"
}
