using System;
using System.Collections.Generic;
//the following two are for the vehicle to actually appear on the screen
using System.Windows.Shapes;
using System.Windows.Media;
namespace Traffic_Sim__Project_for_LP2_Class_
{
    public class Car : Vehicle
    {
        public Car (string name, double speed, double x, double y, Brush color) : base (name, speed, x, y, color)
        {

        }
        public override void Update(TrafficLight light, List<Vehicle> AllVehicles)
        {
            bool pathIsBlocked = false;
            foreach(Vehicle vehicle in AllVehicles)
            {
                if (vehicle == this)
                {
                    continue; // Skip self
                }

                double distance = vehicle.getXPosition() - this.getXPosition();

                if(distance > 0 && distance < 60)
                {
                    pathIsBlocked = true;
                    break; //We found someone, there is no need to keep looking
                }
            }
            bool atStopLine = (getXPosition() > 250 && getXPosition() < 300);
            if (light.CurrentColor == LightColor.red && atStopLine || pathIsBlocked)
            {
                //setSpeed(0);
            }
            else if (light.CurrentColor == LightColor.yellow && atStopLine)
            {
                //setSpeed(getSpeed() / 2);
                this.setXPosition(getXPosition() + (getSpeed() / 2));
            }
            else
            {
                //setSpeed(10);
                this.setXPosition(getXPosition() + getSpeed());
            }
        }
    }
}
