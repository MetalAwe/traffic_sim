using System;
using System.Collections.Generic;
using System.Text;
//the following two are for the vehicle to actually appear on the screen
using System.Windows.Shapes;
using System.Windows.Media;
using System.Runtime.CompilerServices;
namespace Traffic_Sim__Project_for_LP2_Class_
{
    public abstract class Vehicle
    {
        private string licensePlate;
        private double speed;
        private double xPosition;
        private double yPosition;
        protected double TargetY {  get; set; }
        protected bool isChangingLanes { get; set; }
        private const double LaneChangeSpeed = 5.0;
        public Rectangle Shape { get; set; }
        protected int CurrentLaneIndex { get; set; }
        //getters and setters:
        public string getLicensePlate()
        {
            return licensePlate;
        }
        public double getSpeed()
        {
            return speed;
        }

        public double getXPosition()
        {
            return xPosition;
        }

        public double getYPosition()
        {
            return yPosition;
        }

        public void setLicensePlate(string licensePlate)
        {
            this.licensePlate = licensePlate;
        }

        public void setSpeed(double speed)
        {
            this.speed = speed;
        }

        public void setXPosition(double x)
        {
            this.xPosition = x;
        }

        public void setYPosition(double y)
        {
            this.yPosition = y;
        }

        //constructors:

        public Vehicle (string licensePlate, double speed, double xPosition, int startLaneIndex, Brush color)
        {
            this.isChangingLanes = false;
            this.licensePlate = licensePlate;
            this.speed = speed;

            this.CurrentLaneIndex = startLaneIndex;
            double startY = MainWindow.Lanes[startLaneIndex];

            this.xPosition = xPosition;
            this.yPosition = startY;
            this.TargetY = startY;

            Shape = new Rectangle
            {
                Width = 40,
                Height = 20,
                Fill = color
            };
            //so that the vehicles can tilt when changing lanes (not required but looks nice)
            RotateTransform rotateTransform = new RotateTransform(0);
            this.Shape.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            this.Shape.RenderTransform = rotateTransform;
        }

        //object methods:
        protected void HandleLaneMovement()
        {
            RotateTransform rotateTransform = this.Shape.RenderTransform as RotateTransform;

            if (Math.Abs(getYPosition() - TargetY) > 1)
            {
                isChangingLanes = true;
                if (getYPosition() < TargetY)
                {
                    setYPosition(getYPosition() + LaneChangeSpeed);
                    rotateTransform.Angle = 15; // Tilt right when moving down

                }
                else
                {
                    setYPosition(getYPosition() - LaneChangeSpeed);
                    rotateTransform.Angle = -15; // Tilt left when moving up
                }
            }
            else
            {
                setYPosition(TargetY);
                isChangingLanes=false;
                rotateTransform.Angle = 0; // Straighten out when lane change is complete
            }
        }
        public virtual void Move()
        {
            Console.WriteLine("The vehicle is moving down the road");
        }

        public abstract void Update(TrafficLight light, List<Vehicle> vehicles);
    }
}
