using System.Collections.Generic;

namespace Leap.Unity.Proxy {
	
	public class Hand : Proxy {

		protected static Dictionary<Leap.Hand, Hand> hands = new Dictionary<Leap.Hand, Hand>();

		protected Leap.Hand hand_;
		protected Arm arm_;

		public List<Finger> Fingers;

		public Hand(Leap.Hand h) {
			hand_ = h;
			arm_ = new Arm(hand_.Arm);

			Fingers = new List<Finger>(HandModel.NUM_FINGERS);

			for(int i = 0; i < HandModel.NUM_FINGERS; ++i) {
				Fingers.Add(new Finger(hand_.Fingers[i]));
			}
		}

		public static implicit operator Hand(Leap.Hand h) {
			Hand hand;

			if(! hands.TryGetValue(h, out hand)) {
				hand = new Hand(h);

				hands[h] = hand;
			}

			return hand;
		}

		protected override void RegisterGetters() {
			RegisterGetter(Component.ID, () => hand_ != null ? (object) hand_.Id : null);
			RegisterGetter(Component.IS_LEFT, () => hand_ != null ? (object) hand_.IsLeft : null);
			RegisterGetter(Component.BASIS, () => hand_ != null ? (object) hand_.Basis : null);
			RegisterGetter(Component.DIRECTION, () => hand_ != null ? (object) hand_.Direction : null);
            RegisterGetter(Component.PALM_WIDTH, () => hand_ != null ? (object) hand_.PalmWidth : null);
            RegisterGetter(Component.PALM_POSITION, () => hand_ != null ? (object) hand_.PalmPosition : null);
			RegisterGetter(Component.PALM_NORMAL, () => hand_ != null ? (object) hand_.PalmNormal : null);
            RegisterGetter(Component.WRIST_POSITION, () => hand_ != null ? (object) hand_.WristPosition : null);
        }

        public Finger GetThumb() {
            return Fingers[(int) Leap.Finger.FingerType.TYPE_THUMB];
        }

        public Finger GetIndex() {
            return Fingers[(int) Leap.Finger.FingerType.TYPE_INDEX];
        }

        public Finger GetMiddle() {
            return Fingers[(int) Leap.Finger.FingerType.TYPE_MIDDLE];
        }

        public Finger GetRing() {
            return Fingers[(int) Leap.Finger.FingerType.TYPE_RING];
        }

        public Finger GetPinky() {
            return Fingers[(int) Leap.Finger.FingerType.TYPE_PINKY];
        }

        public Arm Arm {
			get {
				return arm_;
			}
		}

		public int Id {
			get {
				return Get<int>(Component.ID);
			}
		}

		public bool IsLeft {
			get {
				return Get<bool>(Component.IS_LEFT);
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

        public float PalmWidth {
            get {
                return Get<float>(Component.PALM_WIDTH);
            }
        }

        public Vector PalmPosition {
			get {
				return Get<Vector>(Component.PALM_POSITION) + noise;
			}
		}

		public Vector PalmNormal {
			get {
				return Get<Vector>(Component.PALM_NORMAL) + noise;
			}
		}

        public Vector WristPosition {
            get {
                return Get<Vector>(Component.WRIST_POSITION) + noise;
            }
        }
    }
}
