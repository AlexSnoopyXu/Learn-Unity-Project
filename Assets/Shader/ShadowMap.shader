Shader "TestShader/ShadowMap"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags
		{
			"RenderType" = "Opaque"
		}
		LOD 100
		// No culling or depth
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _DepthTexture;
			float4x4 _lightTransMatris;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 wp : TANGENT;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 wp: TANGENT;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.wp = mul(UNITY_MATRIX_M, v.vertex);
				o.wp.xyz = o.wp.xyz;
				o.wp.w = 1;
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				float4 pos = mul(_lightTransMatris,i.wp);
				pos.xyz = pos.xyz / pos.w;
				float u = lerp(0, 1, (pos.x + 1) / 2);
				float v = lerp(0, 1, (pos.y + 1) / 2);
				float depth = DecodeFloatRGBA(tex2D(_DepthTexture, float2(u,v)));
				float4 col;
				if (pos.z + 0.005 < depth) {
					col = 0.5f;
				}
				else{
					col = fixed4(1, 1, 1, 1);
				}
				return col;
			}
			ENDCG
		}
	}
		Fallback "Diffuse"
}
