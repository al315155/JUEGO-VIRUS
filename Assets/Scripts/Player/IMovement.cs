using System;

namespace AssemblyCSharp
{
	public interface IMovement
	{
		void Move(float acceleration);
		void Run(bool buffed, out bool running, out float acceleration);
	}
}

