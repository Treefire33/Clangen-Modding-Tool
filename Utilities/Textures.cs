using System;
using System.Numerics;
using StbImageSharp;
using OpenTK.Graphics.OpenGL4;

namespace ClanGenModTool.Textures;

public class Texture
{
	public readonly int Handle;

	public static Texture LoadFromFile(string path)
	{
			int handle = GL.GenTexture();

			GL.ActiveTexture(TextureUnit.Texture0);
			GL.BindTexture(TextureTarget.Texture2D, handle);
			using(Stream stream = File.OpenRead(path))
			{
				ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
				GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
			}

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

			return new Texture(handle);
		}

	public Texture(int glHandle)
	{
			Handle = glHandle;
		}

	public void Use(TextureUnit unit)
	{
			GL.ActiveTexture(unit);
			GL.BindTexture(TextureTarget.Texture2D, Handle);
		}
}