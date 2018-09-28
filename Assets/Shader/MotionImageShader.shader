Shader "TestShader/MotionImageShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "black" {}
		_DistTex("DistTex", 2D) = "black" {}
		_DistMask("DistMask", 2D) = "black" {}
		_Offect("Offect ", float) = 1
	}
	SubShader
	{
		// No culling or depth
		Tags{
		"Queue" = "Transparent"
		}
		Cull Off ZWrite Off ZTest Always

		Pass
		{
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
			sampler2D _DistTex;
			float4 _DistTex_ST;
			sampler2D _DistMask;
			float _Offect;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed2 offset = fixed2(_Time.x, _Offect);
				fixed2 dist = tex2D(_DistTex, i.uv + offset);
				fixed4 distMask = tex2D(_DistMask, i.uv);
				fixed4 col = tex2D(_MainTex, i.uv);
				if (distMask.a - 0.1 > 0) {
					col = tex2D(_MainTex, i.uv + dist * distMask.rg);
				}
				return col;
			}
			ENDCG
		}
	}
}
