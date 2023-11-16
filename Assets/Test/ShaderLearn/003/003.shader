Shader "Unlit/003"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

            struct a2f
            {
                float4 vertext:POSITION;
                float3 normal:NORMAL;
                float4 textcoord:TEXCOORD0;
            };

            struct v2f
            {
                //告诉unity pos存储裁剪空间的顶点信息
                float4 pos:SV_POSITION;
                //告诉unity color存储颜色信息
                fixed3 color:COLOR0;
            };

            v2f vert(a2f v)
			{
			    v2f o;
			    o.pos=UnityObjectToClipPos(v.vertext);
			    o.color=v.normal*0.5+fixed3(0.5,0.5,0.5);
				return o;
			}

			fixed4 frag(v2f i):SV_Target
			{
				return fixed4(i.color,1);
			}
            ENDCG
        }
    }
}
