using System;
using System.Collections.Generic;
using System.Text;
//the following two are for the vehicle to actually appear on the screen
using System.Windows.Shapes;
using System.Windows.Media;
namespace Traffic_Sim__Project_for_LP2_Class_
{
    public class Ambulance : Vehicle
    {
        private bool SirenActive {  get; set; }
        public Ambulance(string name, double speed, double x, double y, Brush color) : base(name, speed, x, y, color) {
            SirenActive = true;
            setSpeed(speed);
        }
        public override void Update(TrafficLight light, List<Vehicle> allVehicles)
        {
            bool pathIsBlocked = false;
            foreach (Vehicle vehicle in allVehicles)
            {
                if (vehicle == this)
                {
                    continue; // Skip self
                }

                if(vehicle.getYPosition() !=  this.getYPosition() )
                {
                    continue; // Skip vehicles in different lanes
                }
                
                double distance = vehicle.getXPosition() - this.getXPosition();

                if (distance > 0 && distance < 60)
                {
                    pathIsBlocked = true;
                    break; //We found someone, there is no need to keep looking
                }
            }
            if (!pathIsBlocked)
            {
                this.setXPosition(getXPosition() + getSpeed());
            } //ambulances do not care about lights
        }
    }
}
