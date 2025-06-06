Shader "UI/BlurWithAlpha"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Float) = 1
        _Color ("Color Tint", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _BlurSize;
            fixed4 _Color;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float blurOffset = _BlurSize / 100.0;

                fixed4 col = tex2D(_MainTex, uv) * 0.2;
                col += tex2D(_MainTex, uv + float2(blurOffset, 0)) * 0.2;
                col += tex2D(_MainTex, uv - float2(blurOffset, 0)) * 0.2;
                col += tex2D(_MainTex, uv + float2(0, blurOffset)) * 0.2;
                col += tex2D(_MainTex, uv - float2(0, blurOffset)) * 0.2;

                col *= _Color;

                // 알파 값을 곱해서 투명도 적용
                col.a *= _Color.a;

                return col;
            }
            ENDCG
        }
    }
}
