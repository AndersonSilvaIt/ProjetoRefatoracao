using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Entities {
	public class Entity {

		public Entity() {

			Id = Guid.NewGuid();
		}

		public Guid Id { get; private set; }

	}
}
