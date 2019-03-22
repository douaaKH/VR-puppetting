using System.Collections.Generic;
using UnityEngine;

namespace Leap.Unity.Proxy {

	public class Handler : MonoBehaviour {

		[Range(0.0f, 2.0f)]
		public float latency = 0.0f;

		[Range(0.0f, 0.02f)]
		public float noise = 0.0f;

		protected HashSet<Proxy> proxies = new HashSet<Proxy>();

		protected void Update() {
			Vector current_noise = new Vector(
				Random.Range(-noise, noise),
				Random.Range(-noise, noise),
				Random.Range(-noise, noise)
			);

			foreach(Proxy proxy in proxies) {
				proxy.Update(latency, current_noise);
			}
		}

		public void Register(Proxy proxy) {
			proxies.Add(proxy);
		}

		public void Remove(Proxy proxy) {
			proxies.Remove(proxy);
		}
	}
}
