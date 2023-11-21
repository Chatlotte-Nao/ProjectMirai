Shader "Unlit/005"
{
    Properties
    {
        _Diffuse("_Diffuse", Color)=(1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            fixed4 _Diffuse;
            struct v2f
            {
                float4 vertext:SV_POSITION;
                fixed3 color: Color;
            };
            
            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertext=UnityObjectToClipPos(v.vertex);
                fixed3 vertextNormal=UnityObjectToWorldNormal(v.normal);
                fixed3 LightDic=normalize(_WorldSpaceLightPos0.xyz);
                fixed3 diffuse=_LightColor0.rgb*_Diffuse.rgb*saturate(dot(vertextNormal,LightDic));
                fixed3 ambient=UNITY_LIGHTMODEL_AMBIENT.rgb;
                o.color=diffuse+ambient;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(i.color,1);
            }
            ENDCG
        }
    }
}
