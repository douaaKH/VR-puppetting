using System;
using System.Collections.Generic;
using UnityEngine;

namespace Leap.Unity.Proxy {

	public enum Component {
		ID,
		TYPE,
		IS_LEFT,
        IS_EXTENDED,
		WIDTH,
		LENGTH,
		BASIS,
		ROTATION,
		DIRECTION,
		CENTER,
        PALM_WIDTH,
		PALM_POSITION,
		PALM_NORMAL,
		WRIST_POSITION,
		ELBOW_POSITION,
		PREV_JOINT,
		NEXT_JOINT,
        TIP_POSITION
	}

	public struct Record {
		public float time;
		public object value;

		public Record(float t, object v) {
			time = t;
			value = v;
		}
	}

	public abstract class Proxy {

		private Dictionary<Component, Func<object>> getters;
		private Dictionary<Component, Queue<Record>> records;
		private Handler handler;

		protected Vector noise;

		protected abstract void RegisterGetters();

		public Proxy() {
			getters = new Dictionary<Component, Func<object>>();
			records = new Dictionary<Component, Queue<Record>>();
			handler = GameObject.FindObjectOfType<Handler>();

			RegisterGetters();

			if(handler != null) {
				handler.Register(this);
			}
		}

		~Proxy() {
			if(handler != null) {
				handler.Remove(this);
			}
		}

		public void Update(float latency, Vector current_noise) {
			noise = current_noise;

			foreach(var pair in getters) {
				Add(pair.Key, pair.Value());
			}

			foreach(var pair in records) {
				while(pair.Value.Count > 1 && (Time.time - pair.Value.Peek().time) > latency) {
					pair.Value.Dequeue();
				}
			}
		}

		protected void RegisterGetter(Component type, Func<object> getter) {
			getters[type] = getter;
		}

		private void Add(Component type, object value) {
			if(value != null) {
				Queue<Record> queue;

				if(!records.TryGetValue(type, out queue)) {
					queue = new Queue<Record>();

					records[type] = queue;
				}

				queue.Enqueue(new Record(Time.time, value));
			}
		}

		protected T Get<T>(Component type) {
			Queue<Record> queue;

			if(records.TryGetValue(type, out queue)) {
				if(queue.Count > 0) {
					return (T) queue.Peek().value;
				}
			}

			return (T) getters[type]();
		}
	}
}
