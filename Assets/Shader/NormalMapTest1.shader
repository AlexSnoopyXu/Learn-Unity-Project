// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "TestShader/NormalMapTest1"
{
		//属性
		Properties{
			_Color("Color", Color) = (1,1,1,1)
			_MainTex("Texture", 2D) = "white"{}
			_BumpMap("Bump Map", 2D) = "bump"{}
		}
		
		//子着色器	
		SubShader
		{
			
			Pass
			{
				//定义Tags
				Tags{ "RenderType" = "Opaque" }

				CGPROGRAM
				//引入头文件
				#include "Lighting.cginc"
				//定义Properties中的变量
				fixed4 _Color;
				sampler2D _MainTex;
				//使用了TRANSFROM_TEX宏就需要定义XXX_ST
				float4 _MainTex_ST;
				sampler2D _BumpMap;
				float4 _BumpMap_ST;
				float4 _LightPos;

				//定义结构体：vertex shader阶段输出的内容
				struct appdata 
				{ 
					float4 vertex : POSITION; 
					float2 uv : TEXCOORD0; 
					float4 tangent : TANGENT; 
					float3 normal:NORMAL; 
					float3 lightDir : TEXCOORD1;
				};
				
				struct v2f 
				{ 
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION; 
					float3 lightDir : TEXCOORD1;
				};


				//定义顶点shader
				v2f vert(appdata v)
				{
					v2f o; 
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _BumpMap); 
					float3 binormal = cross(v.normal, v.tangent); 
					//用顶点的Tangent,Binormal,Normal组合成选择矩阵
					float3x3 rotation = float3x3(v.tangent.xyz, binormal, v.normal);
					o.lightDir = mul(rotation, ObjSpaceLightDir(v.vertex));
					return o;
				}

				//定义片元shader
				fixed4 frag(v2f i) : SV_Target
				{
					float3 normalTex = UnpackNormal(tex2D(_BumpMap, i.uv)).xyz; 
					float3 normal = normalize(normalTex); 
					i.lightDir = normalize(i.lightDir); 
					fixed3 lambert = 0.5 * dot(normalTex, i.lightDir) + 0.5;
					fixed3 diffuse = lambert * _LightColor0.rgb;
					fixed4 col = tex2D(_MainTex, i.uv);
					return fixed4(diffuse * col.rgb, 1.0);
				}

				//使用vert函数和frag函数
				#pragma vertex vert
				#pragma fragment frag

				ENDCG
			}

		}
		//前面的Shader失效的话，使用默认的Diffuse
		FallBack "Diffuse"
}