using System;
using System.Collections.Generic;
//the following two are for the vehicle to actually appear on the screen
using System.Windows.Shapes;
using System.Windows.Media;
namespace Traffic_Sim__Project_for_LP2_Class_
{
    public class Car : Vehicle
    {
        public Car(string name, double speed, double x, int startLaneIndex, Brush color) : base(name, speed, x, startLaneIndex, color)
        {

        }
        public override void Update(TrafficLight light, List<Vehicle> AllVehicles)
        {
            HandleLaneMovement();
            bool pathIsBlocked = IsPathBlocked(AllVehicles, getYPosition(), 60);
            if (pathIsBlocked && !this.isChangingLanes)
            {
                int[] possibleLanes = { CurrentLaneIndex + 1, CurrentLaneIndex - 1 };
                foreach (int nextIndex in possibleLanes)
                {
                    if (nextIndex < 0 || nextIndex >= MainWindow.Lanes.Length) continue; // skip out of bounds
                    if (nextIndex == MainWindow.BusLaneIndex) continue;
                    if ((CurrentLaneIndex == MainWindow.MedianIndex && nextIndex == MainWindow.MedianIndex + 1) ||
                (CurrentLaneIndex == MainWindow.MedianIndex + 1 && nextIndex == MainWindow.MedianIndex))
                    {
                        continue; // illegal move
                    }
                    // is the other lane clear
                    if (pathIsBlocked)
                    {
                        CurrentLaneIndex = nextIndex;
                        TargetY = MainWindow.Lanes[CurrentLaneIndex];
                        break;
                    }
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
        //Helper method to keep code clean (Abstraction)
        private bool IsPathBlocked(List<Vehicle> allVehicles, double laneY, double distanceCheck)
        {
            foreach(Vehicle vehicle in allVehicles)
            {
                if (vehicle == this) continue; // Skip self
                if (Math.Abs(vehicle.getYPosition() - laneY) > 10) continue;
                double dist = vehicle.getXPosition() - this.getXPosition();
                if (dist > -20 && dist < distanceCheck)
                {
                    return true; // Path is blocked
                }
            }
            return false; // Path is clear
        }
    }
}