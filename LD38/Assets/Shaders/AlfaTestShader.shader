Shader "UI/Alfa test shader"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_AlfaTestTexture ("Alfa test texture", 2D) = "white" {}
		_AlfaTestValue ("Alfa test", Range(0, 1)) = 1
		_FullColor ("Full color", color) = (1,1,1,1)
		_EmptyColor ("Full color", color) = (1,1,1,1)
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
			sampler2D _AlfaTestTexture;
			fixed _AlfaTestValue;
			fixed4 _FullColor;
			fixed4 _EmptyColor;

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
				fixed2 alfaTestCoords = fixed2(0, IN.texcoord.y);
				fixed alfa = tex2D(_AlfaTestTexture, alfaTestCoords).r;
				c.a = ceil(max((_AlfaTestValue - alfa), 0));
				c.rgb = lerp(_EmptyColor, _FullColor, _AlfaTestValue);
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}
