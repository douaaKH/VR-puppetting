namespace Leap.Unity.Proxy {

	public class Arm : Proxy {

		protected Leap.Arm arm_;

		public Arm(Leap.Arm a) {
			arm_ = a;
		}

		protected override void RegisterGetters() {
			RegisterGetter(Component.WIDTH, () => arm_ != null ? (object) arm_.Width : null);
			RegisterGetter(Component.BASIS, () => arm_ != null ? (object) arm_.Basis : null);
			RegisterGetter(Component.ROTATION, () => arm_ != null ? (object) arm_.Rotation : null);
			RegisterGetter(Component.DIRECTION, () => arm_ != null ? (object) arm_.Direction : null);
			RegisterGetter(Component.WRIST_POSITION, () => arm_ != null ? (object) arm_.WristPosition : null);
			RegisterGetter(Component.ELBOW_POSITION, () => arm_ != null ? (object) arm_.ElbowPosition : null);
		}

		public float Width
		{
			get
			{
				return Get<float>(Component.WIDTH);
			}
		}

		public LeapTransform Basis {
			get {
				return Get<LeapTransform>(Component.BASIS);
			}
		}

		public LeapQuaternion Rotation {
			get {
				return Get<LeapQuaternion>(Component.ROTATION);
			}
		}

		public Vector Direction {
			get {
				return Get<Vector>(Component.DIRECTION) + noise;
			}
		}

		public Vector WristPosition {
			get {
				return Get<Vector>(Component.WRIST_POSITION) + noise;
			}
		}

		public Vector ElbowPosition {
			get {
				return Get<Vector>(Component.ELBOW_POSITION) + noise;
			}
		}
	}
}
