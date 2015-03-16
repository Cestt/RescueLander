// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:0,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:2,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:6788,x:32939,y:32777,varname:node_6788,prsc:2|emission-5680-OUT,clip-1397-A;n:type:ShaderForge.SFN_Tex2d,id:5909,x:31788,y:33048,varname:node_5909,prsc:2,tex:1a4d4c2f18e9e3f4699902b51517cae9,ntxv:0,isnm:False|TEX-2034-TEX;n:type:ShaderForge.SFN_Color,id:5084,x:31623,y:32867,ptovrint:False,ptlb:Color_A,ptin:_Color_A,varname:node_5084,prsc:2,glob:False,c1:1,c2:0.6827586,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:7256,x:31623,y:33242,ptovrint:False,ptlb:Color_B,ptin:_Color_B,varname:node_7256,prsc:2,glob:False,c1:0.8676471,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Blend,id:2239,x:32379,y:32965,varname:node_2239,prsc:2,blmd:10,clmp:True|SRC-1397-RGB,DST-6876-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:2034,x:31623,y:33048,ptovrint:False,ptlb:ColorMap,ptin:_ColorMap,varname:node_2034,tex:1a4d4c2f18e9e3f4699902b51517cae9,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:3026,x:32012,y:32992,varname:node_3026,prsc:2|A-5084-RGB,B-5909-G;n:type:ShaderForge.SFN_Multiply,id:9824,x:32012,y:33119,varname:node_9824,prsc:2|A-7256-RGB,B-5909-B;n:type:ShaderForge.SFN_Add,id:6876,x:32191,y:33050,varname:node_6876,prsc:2|A-3026-OUT,B-9824-OUT;n:type:ShaderForge.SFN_Tex2d,id:1397,x:32191,y:32878,ptovrint:False,ptlb:Ship_01,ptin:_Ship_01,varname:node_1397,prsc:2,tex:743e0b5b1e9985f46aeaea96250e3162,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:8699,x:32191,y:32717,varname:node_8699,prsc:2|A-1397-RGB,B-5909-R;n:type:ShaderForge.SFN_Add,id:5680,x:32552,y:32912,varname:node_5680,prsc:2|A-8699-OUT,B-2239-OUT;n:type:ShaderForge.SFN_Vector1,id:9321,x:32669,y:32706,varname:node_9321,prsc:2,v1:255;proporder:5084-7256-2034-1397;pass:END;sub:END;*/

Shader "Shader Forge/ShipColor" {
    Properties {
        _Color_A ("Color_A", Color) = (1,0.6827586,0,1)
        _Color_B ("Color_B", Color) = (0.8676471,0,0,1)
        _ColorMap ("ColorMap", 2D) = "white" {}
        _Ship_01 ("Ship_01", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            // Dithering function, to use with scene UVs (screen pixel coords)
            // 3x3 Bayer matrix, based on https://en.wikipedia.org/wiki/Ordered_dithering
            float BinaryDither3x3( float value, float2 sceneUVs ) {
                float3x3 mtx = float3x3(
                    float3( 3,  7,  4 )/10.0,
                    float3( 6,  1,  9 )/10.0,
                    float3( 2,  8,  5 )/10.0
                );
                float2 px = floor(_ScreenParams.xy * sceneUVs);
                int xSmp = fmod(px.x,3);
                int ySmp = fmod(px.y,3);
                float3 xVec = 1-saturate(abs(float3(0,1,2) - xSmp));
                float3 yVec = 1-saturate(abs(float3(0,1,2) - ySmp));
                float3 pxMult = float3( dot(mtx[0],yVec), dot(mtx[1],yVec), dot(mtx[2],yVec) );
                return round(value + dot(pxMult, xVec));
            }
            uniform float4 _Color_A;
            uniform float4 _Color_B;
            uniform sampler2D _ColorMap; uniform float4 _ColorMap_ST;
            uniform sampler2D _Ship_01; uniform float4 _Ship_01_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
                float4 _Ship_01_var = tex2D(_Ship_01,TRANSFORM_TEX(i.uv0, _Ship_01));
                clip( BinaryDither3x3(_Ship_01_var.a - 1.5, sceneUVs) );
////// Lighting:
////// Emissive:
                float4 node_5909 = tex2D(_ColorMap,TRANSFORM_TEX(i.uv0, _ColorMap));
                float3 emissive = ((_Ship_01_var.rgb*node_5909.r)+saturate(( ((_Color_A.rgb*node_5909.g)+(_Color_B.rgb*node_5909.b)) > 0.5 ? (1.0-(1.0-2.0*(((_Color_A.rgb*node_5909.g)+(_Color_B.rgb*node_5909.b))-0.5))*(1.0-_Ship_01_var.rgb)) : (2.0*((_Color_A.rgb*node_5909.g)+(_Color_B.rgb*node_5909.b))*_Ship_01_var.rgb) )));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            // Dithering function, to use with scene UVs (screen pixel coords)
            // 3x3 Bayer matrix, based on https://en.wikipedia.org/wiki/Ordered_dithering
            float BinaryDither3x3( float value, float2 sceneUVs ) {
                float3x3 mtx = float3x3(
                    float3( 3,  7,  4 )/10.0,
                    float3( 6,  1,  9 )/10.0,
                    float3( 2,  8,  5 )/10.0
                );
                float2 px = floor(_ScreenParams.xy * sceneUVs);
                int xSmp = fmod(px.x,3);
                int ySmp = fmod(px.y,3);
                float3 xVec = 1-saturate(abs(float3(0,1,2) - xSmp));
                float3 yVec = 1-saturate(abs(float3(0,1,2) - ySmp));
                float3 pxMult = float3( dot(mtx[0],yVec), dot(mtx[1],yVec), dot(mtx[2],yVec) );
                return round(value + dot(pxMult, xVec));
            }
            uniform sampler2D _Ship_01; uniform float4 _Ship_01_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float2 uv0 : TEXCOORD5;
                float4 screenPos : TEXCOORD6;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
                float4 _Ship_01_var = tex2D(_Ship_01,TRANSFORM_TEX(i.uv0, _Ship_01));
                clip( BinaryDither3x3(_Ship_01_var.a - 1.5, sceneUVs) );
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Cull Off
            Offset 1, 1
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            // Dithering function, to use with scene UVs (screen pixel coords)
            // 3x3 Bayer matrix, based on https://en.wikipedia.org/wiki/Ordered_dithering
            float BinaryDither3x3( float value, float2 sceneUVs ) {
                float3x3 mtx = float3x3(
                    float3( 3,  7,  4 )/10.0,
                    float3( 6,  1,  9 )/10.0,
                    float3( 2,  8,  5 )/10.0
                );
                float2 px = floor(_ScreenParams.xy * sceneUVs);
                int xSmp = fmod(px.x,3);
                int ySmp = fmod(px.y,3);
                float3 xVec = 1-saturate(abs(float3(0,1,2) - xSmp));
                float3 yVec = 1-saturate(abs(float3(0,1,2) - ySmp));
                float3 pxMult = float3( dot(mtx[0],yVec), dot(mtx[1],yVec), dot(mtx[2],yVec) );
                return round(value + dot(pxMult, xVec));
            }
            uniform sampler2D _Ship_01; uniform float4 _Ship_01_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
                float4 _Ship_01_var = tex2D(_Ship_01,TRANSFORM_TEX(i.uv0, _Ship_01));
                clip( BinaryDither3x3(_Ship_01_var.a - 1.5, sceneUVs) );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
