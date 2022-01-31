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

float fShakeAmount = 0.016;
float fPeriod = 0.016;
float fTime = 0;

bool Vertical = false;
bool Horizontal = true;
bool Sync = false;

Texture2D SpriteTexture;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
    AddressU = mirror;
    AddressV = mirror;
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
    
    if (Vertical)
    {
        if (Sync)
        {
            textureUVs.y += cos( ( fTime / fPeriod ) * 2 * 3.14159 ) * fShakeAmount;
        }
        else
        {
            textureUVs.y -= sin( ( fTime / fPeriod ) * 2 * 3.14159 ) * fShakeAmount;
        } 
    }
    if (Horizontal)
    {
        textureUVs.x += cos( ( fTime / fPeriod ) * 2 * 3.14159 ) * fShakeAmount;
    }
    
    float4 vSample = tex2D( SpriteTextureSampler, textureUVs );
	return vSample * input.Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};