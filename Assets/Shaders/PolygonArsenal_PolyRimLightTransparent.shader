// Upgrade NOTE: commented out 'float4 unity_DynamicLightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable

Shader "PolygonArsenal/PolyRimLightTransparent"
{
  Properties
  {
    _InnerColor ("Inner Color", Color) = (1,1,1,1)
    _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0)
    _RimWidth ("Rim Width", Range(0.2, 20)) = 3
    _RimGlow ("Rim Glow Multiplier", Range(0, 9)) = 1
  }
  SubShader
  {
    Tags
    { 
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    Pass // ind: 1, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "LIGHTMODE" = "FORWARDBASE"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
        "SHADOWSUPPORT" = "true"
      }
      Blend One One
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile DIRECTIONAL
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 unity_SHAr;
      //uniform float4 unity_SHAg;
      //uniform float4 unity_SHAb;
      //uniform float4 unity_SHBr;
      //uniform float4 unity_SHBg;
      //uniform float4 unity_SHBb;
      //uniform float4 unity_SHC;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      //uniform float3 _WorldSpaceCameraPos;
      //uniform float4 _WorldSpaceLightPos0;
      uniform float4 _LightColor0;
      uniform float4 _InnerColor;
      uniform float4 _RimColor;
      uniform float _RimWidth;
      uniform float _RimGlow;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float3 normal :NORMAL;
      };
      
      struct OUT_Data_Vert
      {
          float3 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float3 xlv_TEXCOORD2 :TEXCOORD2;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float3 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float3 xlv_TEXCOORD2 :TEXCOORD2;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float3 worldNormal_1;
          float3 tmpvar_2;
          float4 tmpvar_3;
          tmpvar_3.w = 1;
          tmpvar_3.xyz = in_v.vertex.xyz;
          float3x3 tmpvar_4;
          tmpvar_4[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_4[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_4[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_5;
          tmpvar_5 = normalize(mul(in_v.normal, tmpvar_4));
          worldNormal_1 = tmpvar_5;
          tmpvar_2 = worldNormal_1;
          float3 normal_6;
          normal_6 = worldNormal_1;
          float4 tmpvar_7;
          tmpvar_7.w = 1;
          tmpvar_7.xyz = float3(normal_6);
          float3 res_8;
          float3 x_9;
          x_9.x = dot(unity_SHAr, tmpvar_7);
          x_9.y = dot(unity_SHAg, tmpvar_7);
          x_9.z = dot(unity_SHAb, tmpvar_7);
          float3 x1_10;
          float4 tmpvar_11;
          tmpvar_11 = (normal_6.xyzz * normal_6.yzzx);
          x1_10.x = dot(unity_SHBr, tmpvar_11);
          x1_10.y = dot(unity_SHBg, tmpvar_11);
          x1_10.z = dot(unity_SHBb, tmpvar_11);
          res_8 = (x_9 + (x1_10 + (unity_SHC.xyz * ((normal_6.x * normal_6.x) - (normal_6.y * normal_6.y)))));
          float3 tmpvar_12;
          float _tmp_dvx_4 = max(((1.055 * pow(max(res_8, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
          tmpvar_12 = float3(_tmp_dvx_4, _tmp_dvx_4, _tmp_dvx_4);
          res_8 = tmpvar_12;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_3));
          out_v.xlv_TEXCOORD0 = tmpvar_2;
          out_v.xlv_TEXCOORD1 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          out_v.xlv_TEXCOORD2 = max(float3(0, 0, 0), tmpvar_12);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float3 tmpvar_1;
          float3 tmpvar_2;
          float4 c_3;
          float3 tmpvar_4;
          float3 worldViewDir_5;
          float3 lightDir_6;
          float3 tmpvar_7;
          float3 tmpvar_8;
          tmpvar_8 = _WorldSpaceLightPos0.xyz;
          lightDir_6 = tmpvar_8;
          float3 tmpvar_9;
          tmpvar_9 = normalize((_WorldSpaceCameraPos - in_f.xlv_TEXCOORD1));
          worldViewDir_5 = tmpvar_9;
          tmpvar_7 = worldViewDir_5;
          tmpvar_4 = in_f.xlv_TEXCOORD0;
          float3 tmpvar_10;
          float3 tmpvar_11;
          tmpvar_10 = _InnerColor.xyz;
          float tmpvar_12;
          tmpvar_12 = clamp(dot(normalize(tmpvar_7), tmpvar_4), 0, 1);
          float tmpvar_13;
          tmpvar_13 = (1 - tmpvar_12);
          float tmpvar_14;
          tmpvar_14 = pow(tmpvar_13, _RimWidth);
          tmpvar_11 = ((_RimColor.xyz * _RimGlow) * tmpvar_14);
          tmpvar_1 = _LightColor0.xyz;
          tmpvar_2 = lightDir_6;
          float4 c_15;
          float4 c_16;
          float diff_17;
          float tmpvar_18;
          tmpvar_18 = max(0, dot(tmpvar_4, tmpvar_2));
          diff_17 = tmpvar_18;
          c_16.xyz = float3(((tmpvar_10 * tmpvar_1) * diff_17));
          c_16.w = 0;
          c_15.w = c_16.w;
          c_15.xyz = (c_16.xyz + (tmpvar_10 * in_f.xlv_TEXCOORD2));
          c_3.xyz = (c_15.xyz + tmpvar_11);
          c_3.w = 1;
          out_f.color = c_3;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 2, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "LIGHTMODE" = "FORWARDADD"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZWrite Off
      Blend One One
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile POINT
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      //uniform float4 _WorldSpaceLightPos0;
      uniform float4 _LightColor0;
      uniform sampler2D _LightTexture0;
      uniform float4x4 unity_WorldToLight;
      uniform float4 _InnerColor;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float3 normal :NORMAL;
      };
      
      struct OUT_Data_Vert
      {
          float3 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float3 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float3 worldNormal_1;
          float3 tmpvar_2;
          float4 tmpvar_3;
          tmpvar_3.w = 1;
          tmpvar_3.xyz = in_v.vertex.xyz;
          float3x3 tmpvar_4;
          tmpvar_4[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_4[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_4[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_5;
          tmpvar_5 = normalize(mul(in_v.normal, tmpvar_4));
          worldNormal_1 = tmpvar_5;
          tmpvar_2 = worldNormal_1;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_3));
          out_v.xlv_TEXCOORD0 = tmpvar_2;
          out_v.xlv_TEXCOORD1 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float3 tmpvar_1;
          float3 tmpvar_2;
          float4 c_3;
          float3 lightCoord_4;
          float3 tmpvar_5;
          float3 lightDir_6;
          float3 tmpvar_7;
          tmpvar_7 = normalize((_WorldSpaceLightPos0.xyz - in_f.xlv_TEXCOORD1));
          lightDir_6 = tmpvar_7;
          tmpvar_5 = in_f.xlv_TEXCOORD0;
          float3 tmpvar_8;
          tmpvar_8 = _InnerColor.xyz;
          float4 tmpvar_9;
          tmpvar_9.w = 1;
          tmpvar_9.xyz = in_f.xlv_TEXCOORD1;
          lightCoord_4 = mul(unity_WorldToLight, tmpvar_9).xyz;
          float tmpvar_10;
          tmpvar_10 = dot(lightCoord_4, lightCoord_4);
          float tmpvar_11;
          tmpvar_11 = tex2D(_LightTexture0, float2(tmpvar_10, tmpvar_10)).w;
          tmpvar_1 = _LightColor0.xyz;
          tmpvar_2 = lightDir_6;
          tmpvar_1 = (tmpvar_1 * tmpvar_11);
          float4 c_12;
          float4 c_13;
          float diff_14;
          float tmpvar_15;
          tmpvar_15 = max(0, dot(tmpvar_5, tmpvar_2));
          diff_14 = tmpvar_15;
          c_13.xyz = float3(((tmpvar_8 * tmpvar_1) * diff_14));
          c_13.w = 0;
          c_12.w = c_13.w;
          c_12.xyz = c_13.xyz;
          c_3.xyz = c_12.xyz;
          c_3.w = 1;
          out_f.color = c_3;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 3, name: PREPASS
    {
      Name "PREPASS"
      Tags
      { 
        "LIGHTMODE" = "PREPASSBASE"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      Blend One One
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float3 normal :NORMAL;
      };
      
      struct OUT_Data_Vert
      {
          float3 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float3 xlv_TEXCOORD0 :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float3 worldNormal_1;
          float3 tmpvar_2;
          float4 tmpvar_3;
          tmpvar_3.w = 1;
          tmpvar_3.xyz = in_v.vertex.xyz;
          float3x3 tmpvar_4;
          tmpvar_4[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_4[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_4[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_5;
          tmpvar_5 = normalize(mul(in_v.normal, tmpvar_4));
          worldNormal_1 = tmpvar_5;
          tmpvar_2 = worldNormal_1;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_3));
          out_v.xlv_TEXCOORD0 = tmpvar_2;
          out_v.xlv_TEXCOORD1 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 res_1;
          float3 tmpvar_2;
          tmpvar_2 = in_f.xlv_TEXCOORD0;
          res_1.xyz = float3(((tmpvar_2 * 0.5) + 0.5));
          res_1.w = 0;
          out_f.color = res_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 4, name: PREPASS
    {
      Name "PREPASS"
      Tags
      { 
        "LIGHTMODE" = "PREPASSFINAL"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZWrite Off
      Blend One One
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float3 _WorldSpaceCameraPos;
      //uniform float4 _ProjectionParams;
      //uniform float4 unity_SHAr;
      //uniform float4 unity_SHAg;
      //uniform float4 unity_SHAb;
      //uniform float4 unity_SHBr;
      //uniform float4 unity_SHBg;
      //uniform float4 unity_SHBb;
      //uniform float4 unity_SHC;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _InnerColor;
      uniform float4 _RimColor;
      uniform float _RimWidth;
      uniform float _RimGlow;
      uniform sampler2D _LightBuffer;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float3 normal :NORMAL;
      };
      
      struct OUT_Data_Vert
      {
          float3 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float3 xlv_TEXCOORD2 :TEXCOORD2;
          float4 xlv_TEXCOORD3 :TEXCOORD3;
          float4 xlv_TEXCOORD4 :TEXCOORD4;
          float3 xlv_TEXCOORD5 :TEXCOORD5;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float3 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD2 :TEXCOORD2;
          float4 xlv_TEXCOORD3 :TEXCOORD3;
          float3 xlv_TEXCOORD5 :TEXCOORD5;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float3 worldNormal_1;
          float3 tmpvar_2;
          float3 tmpvar_3;
          float4 tmpvar_4;
          float3 tmpvar_5;
          float4 tmpvar_6;
          float4 tmpvar_7;
          tmpvar_7.w = 1;
          tmpvar_7.xyz = in_v.vertex.xyz;
          tmpvar_6 = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_7));
          float3 tmpvar_8;
          tmpvar_8 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          float3x3 tmpvar_9;
          tmpvar_9[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_9[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_9[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_10;
          tmpvar_10 = normalize(mul(in_v.normal, tmpvar_9));
          worldNormal_1 = tmpvar_10;
          tmpvar_2 = worldNormal_1;
          float3 tmpvar_11;
          tmpvar_11 = (_WorldSpaceCameraPos - tmpvar_8);
          tmpvar_3 = tmpvar_11;
          float4 o_12;
          float4 tmpvar_13;
          tmpvar_13 = (tmpvar_6 * 0.5);
          float2 tmpvar_14;
          tmpvar_14.x = tmpvar_13.x;
          tmpvar_14.y = (tmpvar_13.y * _ProjectionParams.x);
          o_12.xy = (tmpvar_14 + tmpvar_13.w);
          o_12.zw = tmpvar_6.zw;
          tmpvar_4.zw = float2(0, 0);
          tmpvar_4.xy = float2(0, 0);
          float3x3 tmpvar_15;
          tmpvar_15[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_15[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_15[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float4 tmpvar_16;
          tmpvar_16.w = 1;
          tmpvar_16.xyz = float3(normalize(mul(in_v.normal, tmpvar_15)));
          float4 normal_17;
          normal_17 = tmpvar_16;
          float3 res_18;
          float3 x_19;
          x_19.x = dot(unity_SHAr, normal_17);
          x_19.y = dot(unity_SHAg, normal_17);
          x_19.z = dot(unity_SHAb, normal_17);
          float3 x1_20;
          float4 tmpvar_21;
          tmpvar_21 = (normal_17.xyzz * normal_17.yzzx);
          x1_20.x = dot(unity_SHBr, tmpvar_21);
          x1_20.y = dot(unity_SHBg, tmpvar_21);
          x1_20.z = dot(unity_SHBb, tmpvar_21);
          res_18 = (x_19 + (x1_20 + (unity_SHC.xyz * ((normal_17.x * normal_17.x) - (normal_17.y * normal_17.y)))));
          float3 tmpvar_22;
          float _tmp_dvx_5 = max(((1.055 * pow(max(res_18, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
          tmpvar_22 = float3(_tmp_dvx_5, _tmp_dvx_5, _tmp_dvx_5);
          res_18 = tmpvar_22;
          tmpvar_5 = tmpvar_22;
          out_v.vertex = tmpvar_6;
          out_v.xlv_TEXCOORD0 = tmpvar_2;
          out_v.xlv_TEXCOORD1 = tmpvar_8;
          out_v.xlv_TEXCOORD2 = tmpvar_3;
          out_v.xlv_TEXCOORD3 = o_12;
          out_v.xlv_TEXCOORD4 = tmpvar_4;
          out_v.xlv_TEXCOORD5 = tmpvar_5;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 tmpvar_1;
          float4 c_2;
          float4 light_3;
          float3 tmpvar_4;
          float3 viewDir_5;
          float3 tmpvar_6;
          float3 tmpvar_7;
          tmpvar_7 = normalize(in_f.xlv_TEXCOORD2);
          viewDir_5 = tmpvar_7;
          tmpvar_6 = viewDir_5;
          tmpvar_4 = in_f.xlv_TEXCOORD0;
          float3 tmpvar_8;
          float3 tmpvar_9;
          tmpvar_8 = _InnerColor.xyz;
          float tmpvar_10;
          tmpvar_10 = clamp(dot(normalize(tmpvar_6), tmpvar_4), 0, 1);
          float tmpvar_11;
          tmpvar_11 = (1 - tmpvar_10);
          float tmpvar_12;
          tmpvar_12 = pow(tmpvar_11, _RimWidth);
          tmpvar_9 = ((_RimColor.xyz * _RimGlow) * tmpvar_12);
          float4 tmpvar_13;
          tmpvar_13 = tex2D(_LightBuffer, in_f.xlv_TEXCOORD3);
          light_3 = tmpvar_13;
          light_3 = (-log2(max(light_3, float4(0.001, 0.001, 0.001, 0.001))));
          light_3.xyz = (light_3.xyz + in_f.xlv_TEXCOORD5);
          float4 c_14;
          c_14.xyz = (tmpvar_8 * light_3.xyz);
          c_14.w = 0;
          c_2 = c_14;
          c_2.xyz = (c_2.xyz + tmpvar_9);
          c_2.w = 1;
          tmpvar_1 = c_2;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 5, name: DEFERRED
    {
      Name "DEFERRED"
      Tags
      { 
        "LIGHTMODE" = "DEFERRED"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      Blend One One
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float3 _WorldSpaceCameraPos;
      //uniform float4 unity_SHAr;
      //uniform float4 unity_SHAg;
      //uniform float4 unity_SHAb;
      //uniform float4 unity_SHBr;
      //uniform float4 unity_SHBg;
      //uniform float4 unity_SHBb;
      //uniform float4 unity_SHC;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _InnerColor;
      uniform float4 _RimColor;
      uniform float _RimWidth;
      uniform float _RimGlow;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float3 normal :NORMAL;
      };
      
      struct OUT_Data_Vert
      {
          float3 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float3 xlv_TEXCOORD2 :TEXCOORD2;
          float4 xlv_TEXCOORD3 :TEXCOORD3;
          float3 xlv_TEXCOORD4 :TEXCOORD4;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float3 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD2 :TEXCOORD2;
          float3 xlv_TEXCOORD4 :TEXCOORD4;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
          float4 color1 :SV_Target1;
          float4 color2 :SV_Target2;
          float4 color3 :SV_Target3;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float3 worldNormal_1;
          float3 tmpvar_2;
          float3 tmpvar_3;
          float4 tmpvar_4;
          float4 tmpvar_5;
          tmpvar_5.w = 1;
          tmpvar_5.xyz = in_v.vertex.xyz;
          float3 tmpvar_6;
          tmpvar_6 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          float3x3 tmpvar_7;
          tmpvar_7[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_7[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_7[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_8;
          tmpvar_8 = normalize(mul(in_v.normal, tmpvar_7));
          worldNormal_1 = tmpvar_8;
          tmpvar_2 = worldNormal_1;
          float3 tmpvar_9;
          tmpvar_9 = (_WorldSpaceCameraPos - tmpvar_6);
          tmpvar_3 = tmpvar_9;
          tmpvar_4.zw = float2(0, 0);
          tmpvar_4.xy = float2(0, 0);
          float3 normal_10;
          normal_10 = worldNormal_1;
          float4 tmpvar_11;
          tmpvar_11.w = 1;
          tmpvar_11.xyz = float3(normal_10);
          float3 res_12;
          float3 x_13;
          x_13.x = dot(unity_SHAr, tmpvar_11);
          x_13.y = dot(unity_SHAg, tmpvar_11);
          x_13.z = dot(unity_SHAb, tmpvar_11);
          float3 x1_14;
          float4 tmpvar_15;
          tmpvar_15 = (normal_10.xyzz * normal_10.yzzx);
          x1_14.x = dot(unity_SHBr, tmpvar_15);
          x1_14.y = dot(unity_SHBg, tmpvar_15);
          x1_14.z = dot(unity_SHBb, tmpvar_15);
          res_12 = (x_13 + (x1_14 + (unity_SHC.xyz * ((normal_10.x * normal_10.x) - (normal_10.y * normal_10.y)))));
          float3 tmpvar_16;
          float _tmp_dvx_6 = max(((1.055 * pow(max(res_12, float3(0, 0, 0)), float3(0.4166667, 0.4166667, 0.4166667))) - 0.055), float3(0, 0, 0));
          tmpvar_16 = float3(_tmp_dvx_6, _tmp_dvx_6, _tmp_dvx_6);
          res_12 = tmpvar_16;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_5));
          out_v.xlv_TEXCOORD0 = tmpvar_2;
          out_v.xlv_TEXCOORD1 = tmpvar_6;
          out_v.xlv_TEXCOORD2 = tmpvar_3;
          out_v.xlv_TEXCOORD3 = tmpvar_4;
          out_v.xlv_TEXCOORD4 = max(float3(0, 0, 0), tmpvar_16);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 outEmission_1;
          float3 tmpvar_2;
          float3 viewDir_3;
          float3 tmpvar_4;
          float3 tmpvar_5;
          tmpvar_5 = normalize(in_f.xlv_TEXCOORD2);
          viewDir_3 = tmpvar_5;
          tmpvar_4 = viewDir_3;
          tmpvar_2 = in_f.xlv_TEXCOORD0;
          float3 tmpvar_6;
          float3 tmpvar_7;
          tmpvar_6 = _InnerColor.xyz;
          float tmpvar_8;
          tmpvar_8 = clamp(dot(normalize(tmpvar_4), tmpvar_2), 0, 1);
          float tmpvar_9;
          tmpvar_9 = (1 - tmpvar_8);
          float tmpvar_10;
          tmpvar_10 = pow(tmpvar_9, _RimWidth);
          tmpvar_7 = ((_RimColor.xyz * _RimGlow) * tmpvar_10);
          float4 emission_11;
          float3 tmpvar_12;
          float3 tmpvar_13;
          tmpvar_12 = tmpvar_6;
          tmpvar_13 = tmpvar_2;
          float4 tmpvar_14;
          tmpvar_14.xyz = float3(tmpvar_12);
          tmpvar_14.w = 1;
          float4 tmpvar_15;
          tmpvar_15.xyz = float3(0, 0, 0);
          tmpvar_15.w = 0;
          float4 tmpvar_16;
          tmpvar_16.w = 1;
          tmpvar_16.xyz = float3(((tmpvar_13 * 0.5) + 0.5));
          float4 tmpvar_17;
          tmpvar_17.w = 1;
          tmpvar_17.xyz = float3(tmpvar_7);
          emission_11 = tmpvar_17;
          emission_11.xyz = (emission_11.xyz + (tmpvar_6 * in_f.xlv_TEXCOORD4));
          outEmission_1.w = emission_11.w;
          outEmission_1.xyz = exp2((-emission_11.xyz));
          out_f.color = tmpvar_14;
          out_f.color1 = tmpvar_15;
          out_f.color2 = tmpvar_16;
          out_f.color3 = outEmission_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 6, name: META
    {
      Name "META"
      Tags
      { 
        "LIGHTMODE" = "META"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      Cull Off
      Blend One One
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      // uniform float4 unity_LightmapST;
      // uniform float4 unity_DynamicLightmapST;
      uniform float4 unity_MetaVertexControl;
      //uniform float3 _WorldSpaceCameraPos;
      uniform float4 _InnerColor;
      uniform float4 _RimColor;
      uniform float _RimWidth;
      uniform float _RimGlow;
      uniform float4 unity_MetaFragmentControl;
      uniform float unity_OneOverOutputBoost;
      uniform float unity_MaxOutputValue;
      uniform float unity_UseLinearSpace;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float3 normal :NORMAL;
          float4 texcoord1 :TEXCOORD1;
          float4 texcoord2 :TEXCOORD2;
      };
      
      struct OUT_Data_Vert
      {
          float3 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float3 xlv_TEXCOORD0 :TEXCOORD0;
          float3 xlv_TEXCOORD1 :TEXCOORD1;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float3 worldNormal_1;
          float3 tmpvar_2;
          float4 vertex_3;
          vertex_3 = in_v.vertex;
          if(unity_MetaVertexControl.x)
          {
              vertex_3.xy = ((in_v.texcoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
              float tmpvar_4;
              if((in_v.vertex.z>0))
              {
                  tmpvar_4 = 0.0001;
              }
              else
              {
                  tmpvar_4 = 0;
              }
              vertex_3.z = tmpvar_4;
          }
          if(unity_MetaVertexControl.y)
          {
              vertex_3.xy = ((in_v.texcoord2.xy * unity_DynamicLightmapST.xy) + unity_DynamicLightmapST.zw);
              float tmpvar_5;
              if((vertex_3.z>0))
              {
                  tmpvar_5 = 0.0001;
              }
              else
              {
                  tmpvar_5 = 0;
              }
              vertex_3.z = tmpvar_5;
          }
          float4 tmpvar_6;
          tmpvar_6.w = 1;
          tmpvar_6.xyz = vertex_3.xyz;
          float3x3 tmpvar_7;
          tmpvar_7[0] = conv_mxt4x4_0(unity_WorldToObject).xyz;
          tmpvar_7[1] = conv_mxt4x4_1(unity_WorldToObject).xyz;
          tmpvar_7[2] = conv_mxt4x4_2(unity_WorldToObject).xyz;
          float3 tmpvar_8;
          tmpvar_8 = normalize(mul(in_v.normal, tmpvar_7));
          worldNormal_1 = tmpvar_8;
          tmpvar_2 = worldNormal_1;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_6));
          out_v.xlv_TEXCOORD0 = tmpvar_2;
          out_v.xlv_TEXCOORD1 = mul(unity_ObjectToWorld, in_v.vertex).xyz;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 tmpvar_1;
          float3 tmpvar_2;
          float3 tmpvar_3;
          float3 tmpvar_4;
          float3 worldViewDir_5;
          float3 tmpvar_6;
          float3 tmpvar_7;
          tmpvar_7 = normalize((_WorldSpaceCameraPos - in_f.xlv_TEXCOORD1));
          worldViewDir_5 = tmpvar_7;
          tmpvar_6 = worldViewDir_5;
          tmpvar_4 = in_f.xlv_TEXCOORD0;
          float3 tmpvar_8;
          float3 tmpvar_9;
          tmpvar_8 = _InnerColor.xyz;
          float tmpvar_10;
          tmpvar_10 = clamp(dot(normalize(tmpvar_6), tmpvar_4), 0, 1);
          float tmpvar_11;
          tmpvar_11 = (1 - tmpvar_10);
          float tmpvar_12;
          tmpvar_12 = pow(tmpvar_11, _RimWidth);
          tmpvar_9 = ((_RimColor.xyz * _RimGlow) * tmpvar_12);
          tmpvar_2 = tmpvar_8;
          tmpvar_3 = tmpvar_9;
          float4 res_13;
          res_13 = float4(0, 0, 0, 0);
          if(unity_MetaFragmentControl.x)
          {
              float4 tmpvar_14;
              tmpvar_14.w = 1;
              tmpvar_14.xyz = float3(tmpvar_2);
              res_13.w = tmpvar_14.w;
              float3 tmpvar_15;
              float _tmp_dvx_7 = clamp(unity_OneOverOutputBoost, 0, 1);
              tmpvar_15 = clamp(pow(tmpvar_2, float3(_tmp_dvx_7, _tmp_dvx_7, _tmp_dvx_7)), float3(0, 0, 0), float3(unity_MaxOutputValue, unity_MaxOutputValue, unity_MaxOutputValue));
              res_13.xyz = float3(tmpvar_15);
          }
          if(unity_MetaFragmentControl.y)
          {
              float3 emission_16;
              if(int(unity_UseLinearSpace))
              {
                  emission_16 = tmpvar_3;
              }
              else
              {
                  emission_16 = (tmpvar_3 * ((tmpvar_3 * ((tmpvar_3 * 0.305306) + 0.6821711)) + 0.01252288));
              }
              float4 tmpvar_17;
              tmpvar_17.w = 1;
              tmpvar_17.xyz = float3(emission_16);
              res_13 = tmpvar_17;
          }
          tmpvar_1 = res_13;
          out_f.color = tmpvar_1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack "Diffuse"
}
