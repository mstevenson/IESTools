using System;

namespace IESTools
{
	public class IesTexture
	{
		IesPixel[,] pixels;

		public IesTexture (int resolution)
		{
			pixels = new IesPixel[resolution, resolution];
		}

		public void WritePixel (int horizontal, int vertical, IesPixel value)
		{
			pixels [horizontal, vertical] = value;
		}
	}
}

