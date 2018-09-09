// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "TestShader/PlanarShadow"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Plane("Plane", vector) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		
		Pass
		{
			ZWrite on
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
						
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
		
		Pass
		{
			ZWrite Off

			CGPROGRAM

			
#pragma vertex vert
#pragma fragment frag
			
				

#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			half4 _Plane;

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert(appdata v)
			{
				v2f o;

				float3 lightDir = WorldSpaceLightDir(v.vertex);

				// 模型空间到世界空间
				float4 worldPosW = mul(unity_ObjectToWorld, v.vertex);
				worldPosW.xyz = worldPosW.xyz - dot(worldPosW.xyz, _Plane.xyz) / dot(lightDir, _Plane.xyz) * lightDir;
				worldPosW.y = 0;
				o.vertex = mul(UNITY_MATRIX_VP,worldPosW);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = fixed4(0.5, 0.5, 0.5, 0.5);
				return col;
			}
			ENDCG
		}
	}
}
