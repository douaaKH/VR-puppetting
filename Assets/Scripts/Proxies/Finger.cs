using System.Collections.Generic;

namespace Leap.Unity.Proxy {

	public class Finger : Proxy {

		protected Leap.Finger finger_;
		protected List<Bone> bones;

		public Finger(Leap.Finger a) {
			finger_ = a;

			bones = new List<Bone>(FingerModel.NUM_BONES);

			for(int i = 0; i < FingerModel.NUM_BONES; ++i) {
				bones.Add(new Bone(finger_.Bone((Leap.Bone.BoneType) i)));
			}
		}

		protected override void RegisterGetters() {
			RegisterGetter(Component.TYPE, () => finger_ != null ? (object) finger_.Type : null);
            RegisterGetter(Component.IS_EXTENDED, () => finger_ != null ? (object) finger_.IsExtended : null);
            RegisterGetter(Component.LENGTH, () => finger_ != null ? (object) finger_.Length : null);
            RegisterGetter(Component.WIDTH, () => finger_ != null ? (object) finger_.Width : null);
            RegisterGetter(Component.TIP_POSITION, () => finger_ != null ? (object) finger_.TipPosition : null);
        }

        public Leap.Finger.FingerType Type {
			get {
				return Get<Leap.Finger.FingerType>(Component.TYPE);
			}
		}

        public bool IsExtended {
            get {
                return Get<bool>(Component.IS_EXTENDED);
            }
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

        public Vector TipPosition {
            get {
                return Get<Vector>(Component.TIP_POSITION) + noise;
            }
        }

        public Bone Bone(Leap.Bone.BoneType type) {
			return type != Leap.Bone.BoneType.TYPE_INVALID ? bones[(int) type] : null;
		}
    }
}
