using System;

namespace IESTools
{
	public class IesRenderer
	{
		public IesRenderer ()
		{
		}

		public void Render (int resolution)
		{
			IESCubemap cubemap = new IESCubemap (resolution);
		}
	}
}

