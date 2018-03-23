Shader "GEDE/CRT_Effect"
{
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}

		_Intensity ("Black & White blend", Range (0, 1)) = 0
		_Color("Color - Full version only", Color) = (0,0,0,1) 
    	_ValueX("LinesSize", Range(1,10)) = 1

    	_LinePos	   ("LinePos", Range(0, 1)) = 0
    	_LineWidth	   ("LineWidth", Range(0, 1)) = 0
		_NoiseTex      ("Noise", 2D) = "white" {}
		_Rnd           ("Random", Vector) = (0.3, 0.7, 0, 0)
	}
	SubShader {
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			half _ValueX; half _Intensity; half _Value;

			uniform sampler2D _MainTex, _NoiseTex;
			uniform float _Static, _LineWidth, _LinePos;
			uniform float2 _Rnd;

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
				
				float4 c = tex2D(_MainTex, i.uv);
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



				const float2 offsets[4] = 
				{
					-0.3,  0.4,
					-0.3, -0.4,
					0.3, -0.4,
					0.3,  0.4
				};
         	}

			ENDCG
		}
	}
}
