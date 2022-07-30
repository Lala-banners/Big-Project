using Big_Project.Scripts.Dumplings.MovementAndCamera.Cameras;
using Big_Project.Scripts.Dumplings.MovementAndCamera.Core;
namespace Big_Project.Scripts.Dumplings.MovementAndCamera.Motors
{
	public class FpsMotor : BaseMotorModularBehaviour
	{
		private new FpsCamera camera;

		public override void Init(IMCCPlayer _playerInterface)
		{
			base.Init(_playerInterface);
			if (_playerInterface.TryGetBehaviour(out FpsCamera cam))
				camera = cam;
			if (_playerInterface.TryGetBehaviour(out MultiCamera multiCamera))
			{
				if (multiCamera.TryGetBehaviour(out cam))
					camera = cam;
			}
		}
	}
}