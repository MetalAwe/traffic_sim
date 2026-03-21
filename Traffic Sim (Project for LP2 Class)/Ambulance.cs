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
        public override void Update(TrafficLight light)
        {
            this.setXPosition(getXPosition() + getSpeed()); //ambulances do not care about lights
        }
    }
}
