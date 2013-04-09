using System;

namespace IESTools
{
	public class IesTexture
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		Pixel[,] pixels;

		public IesTexture (int width, int height)
		{
			this.Width = width;
			this.Height = height;
			pixels = new Pixel[width, height];
		}

		public void WritePixel (int x, int y, Pixel value)
		{
			pixels [x, y] = value;
		}

		public Pixel ReadPixel (int x, int y)
		{
			return pixels [x, y];
		}
	}
}

