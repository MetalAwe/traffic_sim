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
        public Ambulance(string name, double speed, double x, int startLaneIndex, Brush color) : base(name, speed, x, startLaneIndex, color) {
            SirenActive = true;
            setSpeed(speed);
        }
        public override void Update(TrafficLight light, List<Vehicle> allVehicles)
        {
            HandleLaneMovement();
            double otherLaneY = (TargetY == 50) ? 100 : 200;

            bool pathIsBlocked = IsPathBlocked(allVehicles, getYPosition(), 60);
            if (pathIsBlocked && !this.isChangingLanes)
            {
                // try to go down (index + 1)
                if (CurrentLaneIndex < MainWindow.Lanes.Length - 1)
                {
                    int nextLane = CurrentLaneIndex + 1;
                    if (!IsPathBlocked(allVehicles, MainWindow.Lanes[nextLane], 100))
                    {
                        CurrentLaneIndex = nextLane;
                        TargetY = MainWindow.Lanes[CurrentLaneIndex];
                    }
                }
                // try to go up (Index - 1)
                else if (CurrentLaneIndex > 0)
                {
                    int prevLane = CurrentLaneIndex - 1;
                    if (!IsPathBlocked(allVehicles, MainWindow.Lanes[prevLane], 100))
                    {
                        CurrentLaneIndex = prevLane;
                        TargetY = MainWindow.Lanes[CurrentLaneIndex];
                    }
                }
            }
            if (!pathIsBlocked)
            {
                this.setXPosition(getXPosition() + getSpeed());
            } //ambulances do not care about lights when siren is active, they just go through, but they will try to change lanes to avoid traffic if possible
            if (SirenActive)
            {
                this.Shape.Effect = (DateTime.Now.Millisecond % 200 < 100) ? new System.Windows.Media.Effects.DropShadowEffect { Color = Colors.Red, BlurRadius = 15 } : new System.Windows.Media.Effects.DropShadowEffect { Color = Colors.Blue, BlurRadius = 15 }; ;
            }
        }
        private bool IsPathBlocked(List<Vehicle> allVehicles, double laneY, double distanceCheck)
        {
            foreach (Vehicle vehicle in allVehicles)
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
