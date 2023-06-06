Shader "NewImageEffectShader" {
    Properties {
        _MainTex ("Sprite Texture", 2D) = "white" {}
    }

    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Lấy màu của pixel tại vị trí (1 - i.uv.x, i.uv.y) (phản chiếu theo trục x)
                fixed4 col = tex2D(_MainTex, float2(1 - i.uv.x, i.uv.y));
                return col;
            }
            ENDCG
        }
    }
}
