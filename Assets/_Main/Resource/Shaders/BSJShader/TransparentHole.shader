Shader "UI/TransparentHole"
{
    Properties
    {
        _Color("Color", Color) = (0,0,0,0.7)
        _HoleCenter("Hole Center", Vector) = (0.5, 0.5, 0, 0)
        _HoleRadius("Hole Radius", Float) = 0.1
        _RectAspect("Rect Aspect", Vector) = (1,1,0,0)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;
            float2 _HoleCenter;
            float _HoleRadius;
            float2 _RectAspect;

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float dist = distance(i.uv * _RectAspect, _HoleCenter * _RectAspect);
                if (dist < _HoleRadius) discard;
                return _Color;
            }

            ENDCG
        }
    }
}
