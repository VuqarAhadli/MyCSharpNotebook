using System;
class ShabbatTask {

	public abstract class SmartDevice {
		public int DeviceID;
		public string DeviceName{get; set;}
		private bool _DeviceStatus;
		public string DeviceStatus {
			get { return _DeviceStatus ? "On" : "Off"; }
		}
		public static int DeviceCount ;

		public SmartDevice ( int d,string d1, bool d3) {
			this.DeviceID = d;
			this.DeviceName = d1;
			this._DeviceStatus = d3;
			SmartDevice.DeviceCount += 1;
		}



		public virtual void ToggleStatus() {
			this._DeviceStatus = !_DeviceStatus;
		}

		public abstract  void ShowInfo();

		public void ChangeName(string NewName) {
			this.DeviceName = NewName;
		}

	}

	public class SmartLight : SmartDevice {
		private int _brightness;

		public int Brightness {
			get => _brightness;
			set
			{
				if (value < 0 || value > 100)
					throw new ArgumentOutOfRangeException("Brightness must be between 0 and 100.");

				_brightness = value;
			}
		}

		public SmartLight(int d,string d1, bool d3, int b) : base(d,d1,d3) {
			this.Brightness = b;

		}

		public void AdjustBrightness(int level) {
			this.Brightness = level;
		}

		public override  void ShowInfo() {
			Console.WriteLine($"SmartLight Info:\nID: {this.DeviceID}\nName: {this.DeviceName}\nStatus: {this.DeviceStatus}\nBrightness: {this.Brightness}");

		}

		public override void ToggleStatus() {
			base.ToggleStatus();
			Console.WriteLine($"SmartLight toggled {this.DeviceStatus}");
		}



	}

	public class SmartThermostat : SmartDevice {

		public double Temperature {get; set;}


		public SmartThermostat(int d,string d1, bool d3, int t) : base(d,d1,d3) {
			this.Temperature = t;
		}

		public void AdjustTemperature(double temp) {
			this.Temperature = temp;
		}

		public override void ShowInfo() {
			Console.WriteLine($"SmartThermostat Info:\nID: {this.DeviceID}\nName: {this.DeviceName}\nStatus: {this.DeviceStatus}\nTemperature: {this.Temperature}");

		}

		public override void ToggleStatus() {
			base.ToggleStatus();
			Console.WriteLine($"SmartThermostat toggled {this.DeviceStatus}");
		}



	}



	static void Main() {
		SmartLight s1 = new SmartLight(1, "Sadiq", false, 50);
		SmartThermostat s2 = new SmartThermostat(2, "Ekrem", true, 24);

		s1.ShowInfo();
		s2.ShowInfo();


		s1.ToggleStatus();
		s2.AdjustTemperature(22.5);


		Console.WriteLine($"DeviceCount: {SmartDevice.DeviceCount}");
	}

}
