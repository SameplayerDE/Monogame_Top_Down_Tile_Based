#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float DeltaTime;
float TotalTime;

float Curves = 3;
float Wraps = 0;
float Speed = 10;

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
    AddressU = mirror;
    AddressV = border;
    MinFilter = point;
    MagFilter = point;	
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 textureUVs = input.TextureCoordinates;
    textureUVs.x += sin(textureUVs.y * Curves + TotalTime * Speed) * Wraps;
	return tex2D(SpriteTextureSampler, textureUVs) * input.Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};