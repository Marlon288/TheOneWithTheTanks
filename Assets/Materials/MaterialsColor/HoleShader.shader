Shader "Custom/HoleShader" {
    Properties {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _EdgeBrightness ("Edge Brightness", Range(0, 2)) = 1.0
    }

    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert vertex:vert

        sampler2D _MainTex;
        float4 _Color;
        float _EdgeBrightness;

        struct Input {
            float2 uv_MainTex;
            float3 worldPos;
            float gradient;
        };

        void vert (inout appdata_full v, out Input o) {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
            // Create a gradient based on the height (Y-axis) of the object
            o.gradient = 1 - saturate((o.worldPos.y - 0.01) / 0.02);
        }

        void surf (Input IN, inout SurfaceOutput o) {
            // Albedo comes from a texture tinted by color
            float4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Specular = 0.0;
            // Apply the gradient to the albedo, with lighter edges and a darker center
            float edgeColor = lerp(_EdgeBrightness, 1.0, IN.gradient);
            o.Albedo = lerp(o.Albedo * edgeColor, float3(0, 0, 0), IN.gradient);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
