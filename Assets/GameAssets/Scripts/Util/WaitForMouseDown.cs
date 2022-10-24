using UnityEngine;

namespace Match3D
{
	public class WaitForMouseDown : CustomYieldInstruction
	{
		public override bool keepWaiting 
		{
			get 
			{
				return !Input.GetMouseButtonDown(0);
			} 
		}
	}
}
