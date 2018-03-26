Shader "GEDE/CRT_Effect"
{
	Properties {
		_MainTex 	("Base (RGB)", 2D) = "white" {}

    	_LinePos	("LinePos", Range(0, 1)) = 0
    	_LineWidth	("LineWidth", Range(0, 1)) = 0
		_NoiseTex	("Noise", 2D) = "white" {}
		_Rnd		("Random", Vector) = (0.3, 0.7, 0, 0)

		_Radian		("Radian", float) = 0
		_FlyTex		("FlyTex", 2D) = "Blue" {}
		_FlyPos		("FlyPos", Vector) = (0.5, 0.5, 0, 0)
		_FlySize	("FlySize", float) = 0.2

	}
	SubShader {
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex, _NoiseTex, _FlyTex;
			uniform float _Static, _LineWidth, _LinePos, _Radian;
			uniform float2 _Rnd, _FlyPos;

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

			fixed4 frag(v2f i) : SV_Target {

				// Get Current Color in current Pixel
				float4 c = tex2D(_MainTex, i.uv);

				// FLy Effect START
//				float Sin = sin(_Radian);
//				float Cos = cos(_Radian);
//				float3x3 rotationMatrix = float3x3(
//					Cos, -Sin, 0,
//					Sin, Cos, 0, 
//					0, 0, 1);
//
//				if (i.uv.x >= _FlyPos.x - 0.1 && i.uv.x <= _FlyPos.x + 0.1) {
//					if (i.uv.y >= _FlyPos.y - 0.1 && i.uv.y <= _FlyPos.y + 0.1) {
//						fixed4 fly = tex2D(_FlyTex, mul(rotationMatrix, i.uv));
//						c *= fly;
//					}
//				}
				//Fly Effect END

				// Static effect START
				float interference = -0.6 + tex2D(_NoiseTex, i.uv + float2(_Rnd.x, _Rnd.y));
				interference *= _Static;
				c += interference;
				// Static Effect END

				// Line Effect START
				fixed p = i.uv.y;
				if((int)(p < _LinePos || p > _LinePos + (_LineWidth * 0.01))) 
		        	return c;
		        else {
			        fixed4 result = c;
					result.rgb = lerp(c.rgb, 1, 0.1 * _Static);
					return result; 
				}
				// Line Effect END

				return c; // DONE
         	}

			ENDCG
		}
	}
}
