Shader "GJ2019/ToonOutline"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}		
		_OutlineDist("Outline distance", Float) = 1.0
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent"}
		LOD 100

		Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
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
			float _OutlineDist;		
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
			
				return o;
			}

			


			float Outline(float2 uv, float size)
			{

				float2 Disc[16] = 
			{
				float2(0, 1),
				float2(0.3826835, 0.9238796),
				float2(0.7071069, 0.7071068),
				float2(0.9238769, 0.3826834),
				float2(1, 0),
				float2(0.9238795, -0.3826835),
				float2(0.7071068, -0.7071068),
				float2(0.3826833, -0.9238796),
				float2(0,-1),
				float2(-0.3826835, -0.9238796),
				float2(-0.7071069, -0.7071067),
				float2(-0.9238797, -0.3826832),
				float2(-1, 0),
				float2(-0.9238795, 0.3826835),
				float2(-0.7071066, 0.707107),
				float2(-0.3826834, 0.9238796)
			};

				float maxAlpha = 0;

				for(int d = 0; d < 16; d++)
				{
					float sampleAlpha = tex2D(_MainTex, uv + Disc[d] * _MainTex_ST * size).a;

					maxAlpha = max(sampleAlpha, maxAlpha);
				}

				return maxAlpha;
			}

			

			
			fixed4 frag (v2f i) : SV_Target
			{				
				fixed4 col = tex2D(_MainTex, i.uv);
				col.a = max(col.a, (Outline(i.uv, _OutlineDist)));			
				
				return col;
			}
			ENDCG
		}
	}
}
