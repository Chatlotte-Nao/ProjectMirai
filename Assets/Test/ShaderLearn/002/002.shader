Shader "Unlit/002"
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

            float4 vert(float4 v:POSITION):SV_POSITION
			{
				return UnityObjectToClipPos(v);
			}
            //SV_Target 表示当前像素着色器的输出目标
			fixed4 frag():SV_Target
			{
				return fixed4(1,1,1,1);
			}
            ENDCG
        }
    }
}
