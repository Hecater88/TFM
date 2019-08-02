


Shader "Custom/OutlineShader" {

	Properties
	{
		_Color("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex("Texture", 2D) = "white" {}

		_FirstOutlineColor("Outline color", Color) = (0,0,0,1)
		_FirstOutlineWidth("Outlines width", Range(0.0, 2.0)) = 1.1

		_SecondOutlineColor("Outline color", Color) = (0,0,0,1)
		_SecondOutlineWidth("Outlines width", Range(0.0, 2.0)) = 1.1

		_Angle("Switch shader on angle", Range(0.0, 180.0)) = 85
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	struct appdata{
		float4 vertex : POSITION;
		float4 normal : NORMAL;
	};

	struct v2f{
		float4 pos : POSITION;
	};

	uniform float4 _FirstOutlineColor;
	uniform float _FirstOutlineWidth;

	uniform float4 _SecondOutlineColor;
	uniform float _SecondOutlineWidth;

	uniform sampler2D _MainTex;
	uniform float4 _Color;
	uniform float _Angle;

	ENDCG

	SubShader{
		Tags{ "Queue" = "Transparent" "Queue" = "Transparent" "IgnoreProjector" = "True" }

		// Este será el primer outline
		Pass{
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			Cull Back
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			v2f vert(appdata v){
				appdata original = v;

				float3 scaleDir = normalize(v.vertex.xyz - float4(0,0,0,1));
				//Este shader consiste en dos formas de generar el outline que se cambian automaticamente basado en el ángulo que se limita
				// Si el vértice normal apunta fuera del origen del objeto, entonces se crea el outline (que se basa en escalar a lo largo del vector origen-vértice)
				// De lo contrario se usa la escala normal de vectores para el outline
				// De esta manera se evita la creación de ángulos y líneas extrañas al usar cualquiera de los métodos
				if ( degrees(acos(dot(scaleDir.xyz, v.normal.xyz))) > _Angle) {
					v.vertex.xyz += normalize(v.normal.xyz) * _FirstOutlineWidth;
				}else {
					v.vertex.xyz += scaleDir *_FirstOutlineWidth;
				}

				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				return o;
			}

			half4 frag(v2f i) : COLOR{
				return _FirstOutlineColor;
			}

			ENDCG
		}

		// Este será el segundo outline
		Pass{
			Blend SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			Cull Back
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

				v2f vert(appdata v) {
					appdata original = v;

					float3 scaleDir = normalize(v.vertex.xyz - float4(0,0,0,1));
				//Este shader consiste en dos formas de generar el outline que se cambian automaticamente basado en el ángulo que se limita
				// Si el vértice normal apunta fuera del origen del objeto, entonces se crea el outline (que se basa en escalar a lo largo del vector origen-vértice)
				// De lo contrario se usa la escala normal de vectores para el outline
				// De esta manera se evita la creación de ángulos y líneas extrañas al usar cualquiera de los métodos
					if (degrees(acos(dot(scaleDir.xyz, v.normal.xyz))) > _Angle) {
						v.vertex.xyz += normalize(v.normal.xyz) * _SecondOutlineWidth;
					}
					else {
						v.vertex.xyz += scaleDir *_SecondOutlineWidth;
					}

					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					return o;
				}

			half4 frag(v2f i) : COLOR{
				return _SecondOutlineColor;
			}

			ENDCG
		}


		//Surface shader
		Tags{ "Queue" = "Geometry" }
		CGPROGRAM
		#pragma surface surf Lambert

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
	ENDCG
	}
		
	Fallback "Diffuse"
}