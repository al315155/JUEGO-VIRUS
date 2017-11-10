using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	public interface ICameraMovement
	{
		void Move(float speed, Transform camera, Transform player);
	}
}

