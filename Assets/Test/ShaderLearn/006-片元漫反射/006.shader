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
                float4 vertex:SV_POSITION;
                fixed3 vertexNormal:NORMAL;
            };
            
            v2f vert (appdata_base v)
            {
                v2f o;
                o.vertex=UnityObjectToClipPos(v.vertex);
                fixed3 vertexNormal=UnityObjectToWorldNormal(v.normal);
                o.vertexNormal=vertexNormal;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed3 ambient=UNITY_LIGHTMODEL_AMBIENT;

                fixed3 worldLightDir=normalize(_WorldSpaceLightPos0.xyz);
                fixed3 diffuse=_LightColor0.rgb*_Diffuse.rgb*max(0,dot(worldLightDir,normalize(i.vertexNormal)));
                fixed3 color=ambient+diffuse;
                return fixed4(color,1);
            }
            ENDCG
        }
    }
}
