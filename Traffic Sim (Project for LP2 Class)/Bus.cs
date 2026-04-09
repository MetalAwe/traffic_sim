using System;
using System.Collections.Generic;
using System.Text;
//the following two are for the vehicle to actually appear on the screen
using System.Windows.Shapes;
using System.Windows.Media;

namespace Traffic_Sim__Project_for_LP2_Class_
{
    internal class Bus : Vehicle
    {
        public Bus(string name, double speed, double x, int startLaneIndex, Brush color) : base(name, speed, x, startLaneIndex, color)
        {
            setSpeed(speed);
        }
        public override void Update(TrafficLight light, List<Vehicle> vehicles)
        {
            bool pathIsBlocked = false;
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle == this)
                {
                    continue; //skips itself
                }
                if(vehicle.getYPosition() != this.getYPosition())
                {
                    continue; //skips vehicles that are not in the same lane
                }
                
                double distance = vehicle.getXPosition() - this.getXPosition() - 10;

                if (distance > 0 && distance < 60)
                {
                    pathIsBlocked = true;
                    break; //We found someone, there is no need to keep looking
                }
            }
            bool atStopLine = (getXPosition() > 250 && getXPosition() < 300);
            if (light.CurrentColor == LightColor.red && atStopLine || pathIsBlocked)
            {

            }
            else if (light.CurrentColor == LightColor.yellow && atStopLine)
            {
                this.setXPosition(getXPosition() + (getSpeed() / 2));
            }
            else
            {
                double currentFrameSpeed = getSpeed() * MainWindow.TimeScale;
                setXPosition(getXPosition() + currentFrameSpeed);
            }
        }
        }
    }
