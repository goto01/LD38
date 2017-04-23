Shader "Tiles/Glow shader"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_GlowTex ("Glow texture", 2D) = "white" {}
		_BloodTex ("Blood texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_GlowPower ("Glow", range(0, 3)) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _GlowTex;
			sampler2D _BloodTex;
			float _GlowPower;
			float _AlphaSplitEnabled;

			fixed4 frag(v2f IN) : SV_Target
			{
				float2 texcoord = IN.texcoord;
				texcoord.x += sin(texcoord.y*50 + _Time.x * 70)*.01;
				fixed4 c = tex2D(_MainTex, texcoord);
				float alfa = tex2D(_GlowTex, texcoord).a;
				c.rgb *= 1 + alfa * ((sin(_Time.x*50) + 1)/2+1 );
				c.rgb *= IN.color;
				c.rgb += tex2D(_BloodTex, (texcoord + _Time.x)%1).rgb/50 * alfa;
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}
