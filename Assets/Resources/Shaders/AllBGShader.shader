// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "BG/movingBG" {
	Properties{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_F("����", range(1, 10)) = 1
		_Speed("�ٶ�", range(1,10)) = 2
	}
		SubShader{
			pass {
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				float _F;
				float _Speed;

				struct v2f {
						float4 pos:POSITION;
						float2 uv:TEXCOORD0;
				};

				v2f vert(appdata_full v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = v.texcoord.xy;
					return o;
				}

				fixed4 frag(v2f IN) : COLOR
				{
					float2 uv = IN.uv;

					float offset_uv = 0.05 * sin(IN.uv * _F + _Time.x * _Speed);
					uv += offset_uv;

					fixed4 color_1 = tex2D(_MainTex, uv);

					uv = IN.uv;
					uv -= offset_uv * 2;
					fixed4 color_2 = tex2D(_MainTex, uv);

					fixed4 color = (color_1 + color_2) / 2;
					return color;
				}
				ENDCG
			}
		}
}