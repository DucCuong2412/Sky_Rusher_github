// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: replaced tex2D unity_Lightmap with UNITY_SAMPLE_TEX2D

Shader "Battlehub/Legacy Shaders/HB_VertexLit"
{
  Properties
  {
    _Color ("Main Color", Color) = (1,1,1,1)
    _Emission ("Emissive Color", Color) = (0,0,0,0)
    _MainTex ("Base (RGB)", 2D) = "white" {}
  }
  SubShader
  {
    Tags
    { 
      "RenderType" = "Opaque"
    }
    LOD 150
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "LIGHTMODE" = "Vertex"
        "RenderType" = "Opaque"
      }
      LOD 150
      Lighting On
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      #define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 unity_LightColor[8];
      //uniform float4 unity_LightPosition[8];
      //uniform float4 unity_LightAtten[8];
      //uniform float4 unity_SpotDirection[8];
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4 glstate_lightmodel_ambient;
      //uniform float4x4 unity_MatrixV;
      //uniform float4x4 unity_MatrixInvV;
      //uniform float4x4 unity_MatrixVP;
      uniform float _Curvature;
      uniform float _HorizonYOffset;
      uniform float _HorizonZOffset;
      uniform float _Flatten;
      uniform float4 _HBWorldSpaceCameraPos;
      uniform float4 _MainTex_ST;
      uniform float4 _Color;
      uniform float4 _Emission;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float3 normal :NORMAL;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_COLOR0 :COLOR0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 xlv_COLOR0 :COLOR0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float4x4 m_1;
          m_1 = mul(unity_WorldToObject, unity_MatrixInvV);
          float4 tmpvar_2;
          float4 tmpvar_3;
          float4 tmpvar_4;
          tmpvar_2.x = conv_mxt4x4_0(m_1).x;
          tmpvar_2.y = conv_mxt4x4_1(m_1).x;
          tmpvar_2.z = conv_mxt4x4_2(m_1).x;
          tmpvar_2.w = conv_mxt4x4_3(m_1).x;
          tmpvar_3.x = conv_mxt4x4_0(m_1).y;
          tmpvar_3.y = conv_mxt4x4_1(m_1).y;
          tmpvar_3.z = conv_mxt4x4_2(m_1).y;
          tmpvar_3.w = conv_mxt4x4_3(m_1).y;
          tmpvar_4.x = conv_mxt4x4_0(m_1).z;
          tmpvar_4.y = conv_mxt4x4_1(m_1).z;
          tmpvar_4.z = conv_mxt4x4_2(m_1).z;
          tmpvar_4.w = conv_mxt4x4_3(m_1).z;
          float4 tmpvar_5;
          float4 tmpvar_6;
          float4 tmpvar_7;
          tmpvar_7 = mul(unity_ObjectToWorld, in_v.vertex);
          float4 tmpvar_8;
          tmpvar_8 = (tmpvar_7 - _HBWorldSpaceCameraPos);
          float tmpvar_9;
          tmpvar_9 = max(0, (abs((_HorizonZOffset - tmpvar_8.z)) - _Flatten));
          float tmpvar_10;
          tmpvar_10 = max(0, (abs((_HorizonYOffset - tmpvar_8.y)) - _Flatten));
          float4 tmpvar_11;
          tmpvar_11.yzw = float3(0, 0, 0);
          tmpvar_11.x = (((tmpvar_9 * tmpvar_9) + (tmpvar_10 * tmpvar_10)) * (-_Curvature));
          tmpvar_5 = mul(unity_WorldToObject, (tmpvar_7 + tmpvar_11));
          float4 tmpvar_12;
          tmpvar_12.w = 1;
          tmpvar_12.xyz = tmpvar_5.xyz;
          float3 lightColor_13;
          float3 viewN_14;
          float3 viewpos_15;
          float4 tmpvar_16;
          tmpvar_16.w = 1;
          tmpvar_16.xyz = tmpvar_5.xyz;
          viewpos_15 = mul(unity_MatrixV, mul(unity_ObjectToWorld, tmpvar_16)).xyz;
          float3x3 tmpvar_17;
          tmpvar_17[0] = tmpvar_2.xyz;
          tmpvar_17[1] = tmpvar_3.xyz;
          tmpvar_17[2] = tmpvar_4.xyz;
          viewN_14 = normalize(mul(tmpvar_17, in_v.normal));
          float3 tmpvar_18;
          tmpvar_18 = (glstate_lightmodel_ambient * 2).xyz;
          lightColor_13 = tmpvar_18;
          float3 toLight_19;
          float3 tmpvar_20;
          tmpvar_20 = (unity_LightPosition[0].xyz - (viewpos_15 * unity_LightPosition[0].w));
          float tmpvar_21;
          tmpvar_21 = max(dot(tmpvar_20, tmpvar_20), 1E-06);
          toLight_19 = (tmpvar_20 * rsqrt(tmpvar_21));
          lightColor_13 = (lightColor_13 + (unity_LightColor[0].xyz * (max(0, dot(viewN_14, toLight_19)) * ((1 / (1 + (tmpvar_21 * unity_LightAtten[0].z))) * clamp(((max(0, dot(toLight_19, unity_SpotDirection[0].xyz)) - unity_LightAtten[0].x) * unity_LightAtten[0].y), 0, 1)))));
          float3 toLight_22;
          float3 tmpvar_23;
          tmpvar_23 = (unity_LightPosition[1].xyz - (viewpos_15 * unity_LightPosition[1].w));
          float tmpvar_24;
          tmpvar_24 = max(dot(tmpvar_23, tmpvar_23), 1E-06);
          toLight_22 = (tmpvar_23 * rsqrt(tmpvar_24));
          lightColor_13 = (lightColor_13 + (unity_LightColor[1].xyz * (max(0, dot(viewN_14, toLight_22)) * ((1 / (1 + (tmpvar_24 * unity_LightAtten[1].z))) * clamp(((max(0, dot(toLight_22, unity_SpotDirection[1].xyz)) - unity_LightAtten[1].x) * unity_LightAtten[1].y), 0, 1)))));
          float3 toLight_25;
          float3 tmpvar_26;
          tmpvar_26 = (unity_LightPosition[2].xyz - (viewpos_15 * unity_LightPosition[2].w));
          float tmpvar_27;
          tmpvar_27 = max(dot(tmpvar_26, tmpvar_26), 1E-06);
          toLight_25 = (tmpvar_26 * rsqrt(tmpvar_27));
          lightColor_13 = (lightColor_13 + (unity_LightColor[2].xyz * (max(0, dot(viewN_14, toLight_25)) * ((1 / (1 + (tmpvar_27 * unity_LightAtten[2].z))) * clamp(((max(0, dot(toLight_25, unity_SpotDirection[2].xyz)) - unity_LightAtten[2].x) * unity_LightAtten[2].y), 0, 1)))));
          float3 toLight_28;
          float3 tmpvar_29;
          tmpvar_29 = (unity_LightPosition[3].xyz - (viewpos_15 * unity_LightPosition[3].w));
          float tmpvar_30;
          tmpvar_30 = max(dot(tmpvar_29, tmpvar_29), 1E-06);
          toLight_28 = (tmpvar_29 * rsqrt(tmpvar_30));
          lightColor_13 = (lightColor_13 + (unity_LightColor[3].xyz * (max(0, dot(viewN_14, toLight_28)) * ((1 / (1 + (tmpvar_30 * unity_LightAtten[3].z))) * clamp(((max(0, dot(toLight_28, unity_SpotDirection[3].xyz)) - unity_LightAtten[3].x) * unity_LightAtten[3].y), 0, 1)))));
          float4 tmpvar_31;
          tmpvar_31.w = 1;
          tmpvar_31.xyz = float3(lightColor_13);
          tmpvar_6 = ((tmpvar_31 * _Color) + _Emission);
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.xlv_COLOR0 = tmpvar_6;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_12));
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 c_1;
          float4 tmpvar_2;
          tmpvar_2 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          float4 tmpvar_3;
          tmpvar_3 = ((tmpvar_2 * in_f.xlv_COLOR0) + _Emission);
          c_1.xyz = tmpvar_3.xyz;
          c_1.w = 1;
          out_f.color = c_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 2, name: 
    {
      Tags
      { 
        "LIGHTMODE" = "VertexLM"
        "RenderType" = "Opaque"
      }
      LOD 150
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
      // uniform float4 unity_LightmapST;
      uniform float _Curvature;
      uniform float _HorizonYOffset;
      uniform float _HorizonZOffset;
      uniform float _Flatten;
      uniform float4 _HBWorldSpaceCameraPos;
      uniform float4 _MainTex_ST;
      // uniform sampler2D unity_Lightmap;
      uniform sampler2D _MainTex;
      uniform float4 _Color;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 texcoord :TEXCOORD0;
          float4 texcoord1 :TEXCOORD1;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float2 xlv_TEXCOORD1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float2 xlv_TEXCOORD1 :TEXCOORD1;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float2 tmpvar_1;
          float2 tmpvar_2;
          float4 tmpvar_3;
          tmpvar_3 = mul(unity_ObjectToWorld, in_v.vertex);
          float4 tmpvar_4;
          tmpvar_4 = (tmpvar_3 - _HBWorldSpaceCameraPos);
          float tmpvar_5;
          tmpvar_5 = max(0, (abs((_HorizonZOffset - tmpvar_4.z)) - _Flatten));
          float tmpvar_6;
          tmpvar_6 = max(0, (abs((_HorizonYOffset - tmpvar_4.y)) - _Flatten));
          float4 tmpvar_7;
          tmpvar_7.yzw = float3(0, 0, 0);
          tmpvar_7.x = (((tmpvar_5 * tmpvar_5) + (tmpvar_6 * tmpvar_6)) * (-_Curvature));
          float4 tmpvar_8;
          tmpvar_8.w = 1;
          tmpvar_8.xyz = mul(unity_WorldToObject, (tmpvar_3 + tmpvar_7)).xyz;
          tmpvar_1 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          tmpvar_2 = ((in_v.texcoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
          out_v.xlv_TEXCOORD0 = tmpvar_1;
          out_v.xlv_TEXCOORD1 = tmpvar_2;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_8));
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 c_1;
          float4 lm_2;
          float4 tmpvar_3;
          tmpvar_3 = (UNITY_SAMPLE_TEX2D(unity_Lightmap, in_f.xlv_TEXCOORD1) * _Color);
          lm_2 = tmpvar_3;
          c_1.xyz = (tex2D(_MainTex, in_f.xlv_TEXCOORD0).xyz * (lm_2.xyz * 2));
          c_1.w = 1;
          out_f.color = c_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 3, name: 
    {
      Tags
      { 
        "LIGHTMODE" = "VertexLMRGBM"
        "RenderType" = "Opaque"
      }
      LOD 150
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
      // uniform float4 unity_LightmapST;
      uniform float _Curvature;
      uniform float _HorizonYOffset;
      uniform float _HorizonZOffset;
      uniform float _Flatten;
      uniform float4 _HBWorldSpaceCameraPos;
      uniform float4 _MainTex_ST;
      // uniform sampler2D unity_Lightmap;
      uniform sampler2D _MainTex;
      uniform float4 _Color;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 texcoord :TEXCOORD0;
          float4 texcoord1 :TEXCOORD1;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float2 xlv_TEXCOORD1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float2 xlv_TEXCOORD1 :TEXCOORD1;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float2 tmpvar_1;
          float2 tmpvar_2;
          float4 tmpvar_3;
          tmpvar_3 = mul(unity_ObjectToWorld, in_v.vertex);
          float4 tmpvar_4;
          tmpvar_4 = (tmpvar_3 - _HBWorldSpaceCameraPos);
          float tmpvar_5;
          tmpvar_5 = max(0, (abs((_HorizonZOffset - tmpvar_4.z)) - _Flatten));
          float tmpvar_6;
          tmpvar_6 = max(0, (abs((_HorizonYOffset - tmpvar_4.y)) - _Flatten));
          float4 tmpvar_7;
          tmpvar_7.yzw = float3(0, 0, 0);
          tmpvar_7.x = (((tmpvar_5 * tmpvar_5) + (tmpvar_6 * tmpvar_6)) * (-_Curvature));
          float4 tmpvar_8;
          tmpvar_8.w = 1;
          tmpvar_8.xyz = mul(unity_WorldToObject, (tmpvar_3 + tmpvar_7)).xyz;
          tmpvar_1 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          tmpvar_2 = ((in_v.texcoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
          out_v.xlv_TEXCOORD0 = tmpvar_1;
          out_v.xlv_TEXCOORD1 = tmpvar_2;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_8));
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 c_1;
          float4 lm_2;
          float4 tmpvar_3;
          tmpvar_3 = UNITY_SAMPLE_TEX2D(unity_Lightmap, in_f.xlv_TEXCOORD1);
          lm_2 = tmpvar_3;
          lm_2 = (lm_2 * (lm_2.w * 2));
          lm_2 = (lm_2 * _Color);
          c_1.xyz = (tex2D(_MainTex, in_f.xlv_TEXCOORD0).xyz * (lm_2.xyz * 4));
          c_1.w = 1;
          out_f.color = c_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 4, name: SHADOWCASTER
    {
      Name "SHADOWCASTER"
      Tags
      { 
        "LIGHTMODE" = "SHADOWCASTER"
        "RenderType" = "Opaque"
        "SHADOWSUPPORT" = "true"
      }
      LOD 150
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile SHADOWS_DEPTH
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 _WorldSpaceLightPos0;
      //uniform float4 unity_LightShadowBias;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      uniform float _Curvature;
      uniform float _HorizonYOffset;
      uniform float _HorizonZOffset;
      uniform float _Flatten;
      uniform float4 _HBWorldSpaceCameraPos;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float3 normal :NORMAL;
      };
      
      struct OUT_Data_Vert
      {
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float4 vertex :Position;
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
          float4 wPos_7;
          float4 tmpvar_8;
          tmpvar_8 = mul(unity_ObjectToWorld, mul(unity_WorldToObject, (tmpvar_1 + tmpvar_5)));
          wPos_7 = tmpvar_8;
          if((unity_LightShadowBias.z!=0))
          {
              float3x3 tmpvar_9;
              tmpvar_9[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
              tmpvar_9[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
              tmpvar_9[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
              float3 tmpvar_10;
              tmpvar_10 = normalize(mul(in_v.normal, tmpvar_9));
              float tmpvar_11;
              tmpvar_11 = dot(tmpvar_10, normalize((_WorldSpaceLightPos0.xyz - (tmpvar_8.xyz * _WorldSpaceLightPos0.w))));
              wPos_7.xyz = (tmpvar_8.xyz - (tmpvar_10 * (unity_LightShadowBias.z * sqrt((1 - (tmpvar_11 * tmpvar_11))))));
          }
          tmpvar_6 = mul(unity_MatrixVP, wPos_7);
          float4 clipPos_12;
          clipPos_12.xyw = tmpvar_6.xyw;
          clipPos_12.z = (tmpvar_6.z + clamp((unity_LightShadowBias.x / tmpvar_6.w), 0, 1));
          clipPos_12.z = lerp(clipPos_12.z, max(clipPos_12.z, (-tmpvar_6.w)), unity_LightShadowBias.y);
          out_v.vertex = clipPos_12;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          out_f.color = float4(0, 0, 0, 0);
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack "Legacy Shaders/VertexLit"
}
