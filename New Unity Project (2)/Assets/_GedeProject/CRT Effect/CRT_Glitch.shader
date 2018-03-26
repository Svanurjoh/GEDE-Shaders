Shader "GEDE/CRT_Glitch"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ValueX("Horizontal Shift - Full version only", Range(-1,1)) = 0.1
		_ValueY("Vertical Shift", Range(-0,1)) = 0

	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			half _ValueY;

			struct v2f {
			   float4 pos : POSITION;
			   half2 uv : TEXCOORD0;
			};
			   
			//Our Vertex Shader 
			v2f vert (appdata_img v){
			   v2f o;
			   o.pos = UnityObjectToClipPos (v.vertex);
			   o.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
			   return o; 
			}
			
			uniform sampler2D _MainTex;  

			fixed4 frag(v2f i) : COLOR {
				i.uv.y += _ValueY;
				float4 c = tex2D(_MainTex, i.uv);

				return c;
         	}
			ENDCG
		}
	}
}
