namespace Shadowrun.Model {
	public abstract class Gear {
		public string Name { get; set; }
		public string Description { get; set; }
		public int Rating { get; set; }
		public int Cost { get; set; }
	
		public int Availability { get { return AvailabilityRate + (Rating * AvailabilityMultiplier); } }
		public int AvailabilityRate { get; private set; }
		public int AvailabilityMultiplier { get; private set; }

		public Legality Legality { get; set; }

	}

	public enum Legality{Legal,Restricted,Forbidden}
}
