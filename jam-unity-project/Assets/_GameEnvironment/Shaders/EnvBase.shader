// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "EnvBase"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_CloudsPower("CloudsPower", Float) = 0
		_Tiling("Tiling", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Lambert keepalpha addshadow fullforwardshadows 
		struct Input
		{
			half2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform sampler2D _TextureSample1;
		uniform half _Tiling;
		uniform half _CloudsPower;

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			half4 tex2DNode1 = tex2D( _TextureSample0, uv_TextureSample0 );
			float3 ase_worldPos = i.worldPos;
			float2 appendResult4 = (half2(( _Time.y + ase_worldPos.x ) , ase_worldPos.z));
			o.Albedo = saturate( ( tex2DNode1 + saturate( ( tex2D( _TextureSample1, ( appendResult4 * _Tiling ) ).r * _CloudsPower * tex2DNode1 ) ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16200
227;28;1304;983;1241.955;285.4941;1;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;12;-1572.728,39.36125;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleTimeNode;14;-1485.199,-106.5067;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;-1243.199,-4.90665;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;4;-965.3293,72.76467;Float;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-857.9658,343.2438;Float;False;Property;_Tiling;Tiling;3;0;Create;True;0;0;False;0;0;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-700.9658,204.2438;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-545.1755,163.5434;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;False;0;e24b2c680edaa90458d31f11544d79ca;0f6eb034366acdc4d852ce5cd7aae0a2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-388.3391,373.3065;Float;False;Property;_CloudsPower;CloudsPower;2;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-484.1755,-144.4566;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;None;c8cd52c1b65283448bd2ceb4ff3303d1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-169.3391,174.3065;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;8;9.287977,100.2129;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;5;72.86089,-107.6935;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;9;232.888,-144.187;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;389.0999,-100.2;Half;False;True;2;Half;ASEMaterialInspector;0;0;Lambert;EnvBase;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;13;0;14;0
WireConnection;13;1;12;1
WireConnection;4;0;13;0
WireConnection;4;1;12;3
WireConnection;18;0;4;0
WireConnection;18;1;19;0
WireConnection;2;1;18;0
WireConnection;6;0;2;1
WireConnection;6;1;7;0
WireConnection;6;2;1;0
WireConnection;8;0;6;0
WireConnection;5;0;1;0
WireConnection;5;1;8;0
WireConnection;9;0;5;0
WireConnection;0;0;9;0
ASEEND*/
//CHKSM=324A62806253CE68863B21A040EC49FEA16AEF78