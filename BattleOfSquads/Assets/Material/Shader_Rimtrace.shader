// Использует модель освещения Lambert
// Шейдер с эффектом обрамления с настройкой дополнительного цвета

Shader "Custom/Shader_Rimtrace"
{
    Properties
    {
        _MainTex(" Diffuse (RGB)", 2D) = "white" {}
        _NormalMap("Normal Map", 2D) = "bump" {}
        _RimColor("Rim Color", Color) = (1.0, 1.0, 1.0, 0.0)
        _RimConcentration("Rim Concentration", Range(0.5, 5.0)) = 0.9
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            CGPROGRAM
            #pragma surface surf Lambert
            sampler2D _MainTex;
            sampler2D _NormalMap;
            float4 _RimColor;
            float _RimConcentration;
            struct Input
            {
                float2 uv_MainTex;
                float2 uv_NormalMap;
                float3 viewDir;
            };
            void surf(Input In, inout SurfaceOutput o)
            {
                fixed4 c = tex2D(_MainTex, In.uv_MainTex);
                float Lc = saturate(dot(normalize(In.viewDir), o.Normal));
                half rim = 1.0 - Lc;
                o.Normal = UnpackNormal(tex2D(_NormalMap, In.uv_NormalMap));
                o.Alpha = c.a;
                o.Albedo = c.rgb;
                o.Emission = _RimColor.rgb * pow(rim, _RimConcentration);
            }
            ENDCG
        }        
}
