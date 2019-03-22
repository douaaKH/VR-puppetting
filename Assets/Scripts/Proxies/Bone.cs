namespace Leap.Unity.Proxy {

	public class Bone : Proxy {

		protected Leap.Bone bone_;

		public Bone(Leap.Bone b) {
			bone_ = b;
		}

		protected override void RegisterGetters() {
			RegisterGetter(Component.LENGTH, () => bone_ != null ? (object) bone_.Length : null);
			RegisterGetter(Component.WIDTH, () => bone_ != null ? (object) bone_.Width : null);
            RegisterGetter(Component.BASIS, () => bone_ != null ? (object) bone_.Basis : null);
            RegisterGetter(Component.DIRECTION, () => bone_ != null ? (object) bone_.Direction : null);
            RegisterGetter(Component.ROTATION, () => bone_ != null ? (object) bone_.Rotation : null);
			RegisterGetter(Component.CENTER, () => bone_ != null ? (object) bone_.Center : null);
			RegisterGetter(Component.PREV_JOINT, () => bone_ != null ? (object) bone_.PrevJoint : null);
			RegisterGetter(Component.NEXT_JOINT, () => bone_ != null ? (object) bone_.NextJoint : null);
		}

		public float Length {
			get {
				return Get<float>(Component.LENGTH);
			}
		}

		public float Width {
			get {
				return Get<float>(Component.WIDTH);
			}
		}

        public LeapTransform Basis {
            get {
                return Get<LeapTransform>(Component.BASIS);
            }
        }

        public Vector Direction {
            get {
                return Get<Vector>(Component.DIRECTION) + noise;
            }
        }

        public LeapQuaternion Rotation {
			get {
				return Get<LeapQuaternion>(Component.ROTATION);
			}
		}

		public Vector Center {
			get {
				return Get<Vector>(Component.CENTER) + noise;
			}
		}

		public Vector PrevJoint {
			get {
				return Get<Vector>(Component.PREV_JOINT) + noise;
			}
		}

		public Vector NextJoint {
			get {
				return Get<Vector>(Component.NEXT_JOINT) + noise;
			}
		}
    }
}
