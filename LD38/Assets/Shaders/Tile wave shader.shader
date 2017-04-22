Shader "Tiles/Default"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
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
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;
			uniform float4 _SpriteRect;

			fixed4 frag(v2f IN) : SV_Target
			{
				float2 texcoord = IN.texcoord;
				fixed deltaY = sin((texcoord.y - _SpriteRect[1]) / _SpriteRect[3]);
				fixed deltaX = sin((texcoord.x - _SpriteRect[0]) / _SpriteRect[2]);
				texcoord += float2(sin(deltaX)/10, sin(deltaY)/10);
				texcoord.x = clamp(texcoord.x, _SpriteRect[0], _SpriteRect[0]+_SpriteRect[2]);
				texcoord.y = clamp(texcoord.y, _SpriteRect[1], _SpriteRect[1]+_SpriteRect[3]);
				fixed4 c = tex2D(_MainTex, texcoord);
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}
