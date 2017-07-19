// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SelectionBox" {
	Properties {
		// Disabling this doesn't do anything apparently
		//_MainTex ("Albedo (RGB)", 2D) = "white" {}
		//_Transparency ("Transparency", Range(0, 1)) = 0.5
		//_BorderTransparency ("Border Transparency", Range(0, 1)) = 0.5
		//_Color ("Color", color) = (1, 1, 1, 1) 
	}
	SubShader {
		 Tags { "Queue"="Transparent" "RenderType"="Transparent" }

		 ZWrite Off
	     Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag


			float _Transparency;
			float4 _Color;

			struct VertexInput {
				float4 vertex : POSITION;
			};

			struct FragmentInput {
				float4 vertex : SV_POSITION;
			};

			FragmentInput vert(VertexInput input) {
				VertexInput o;
				o.vertex = UnityObjectToClipPos(input.vertex);
				return o;
			}

			float4 frag(FragmentInput input) : COLOR {
				return float4(_Color.rgb, _Transparency);
			}


			ENDCG
		}

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			float4 _Color;
			float _BorderTransparency;

			struct VertexInput {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct FragmentInput {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			FragmentInput vert(VertexInput input) {
				FragmentInput o;
				o.vertex = UnityObjectToClipPos(input.vertex);
				o.uv = input.uv;
				return o;
			}

			float4 frag(FragmentInput input) : COLOR {
				return tex2D(_MainTex, input.uv) * float4(_Color.rgb, _BorderTransparency);
			}


			ENDCG
		}
	}
}
