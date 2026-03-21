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
        public override void Update(TrafficLight light)
        {
            bool atStopLine = (getXPosition() > 250 && getXPosition() < 300);
            if (light.CurrentColor == LightColor.red && atStopLine)
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
