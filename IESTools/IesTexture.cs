using System;

namespace IESTools
{
	public class IesTexture
	{
		float[,] pixels;

		public IesTexture (int resolution)
		{
			pixels = new float[resolution, resolution];
		}

		public void WritePixel (int horizontal, int vertical, float value)
		{
			pixels [horizontal, vertical] = value;
		}
	}
}

