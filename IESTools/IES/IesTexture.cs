using System;

namespace IESTools
{
	public class IesTexture
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		double[,] intensities;

		public IesTexture (int width, int height)
		{
			this.Width = width;
			this.Height = height;
			intensities = new double[width, height];
		}

		public void WritePixelIntensity (int x, int y, double intensity)
		{
			intensities [x, y] = intensity;
		}

		public double ReadPixelIntensity (int x, int y)
		{
			return intensities [x, y];
		}
	}
}

