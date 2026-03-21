using System;
using System.Collections.Generic;
using System.Text;
//the following two are for the vehicle to actually appear on the screen
using System.Windows.Shapes;
using System.Windows.Media; 
namespace Traffic_Sim__Project_for_LP2_Class_
{
    public abstract class Vehicle
    {
        private string licensePlate;
        private double speed;
        private double xPosition;
        private double yPosition;
        public Rectangle Shape { get; set; }

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

        public Vehicle (string licensePlate, double speed, double xPosition, double yPosition, Brush color)
        {
            this.licensePlate = licensePlate;
            this.speed = speed;
            this.xPosition = xPosition;
            this.yPosition = yPosition;

            Shape = new Rectangle
            {
                Width = 40,
                Height = 20,
                Fill = color
            };
        }

        //object methods:

        
        public virtual void Move()
        {
            Console.WriteLine("The vehicle is moving down the road");
        }

        public abstract void Update(TrafficLight lightS);
    }
}
