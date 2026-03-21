using System;
using System.Collections.Generic;
using System.Text;
//the following two are for the vehicle to actually appear on the screen
using System.Windows.Shapes;
using System.Windows.Media;

namespace Traffic_Sim__Project_for_LP2_Class_
{
    public enum LightColor { red, yellow, green }
    public class TrafficLight
    {
        public LightColor CurrentColor { get; private set; }
        public Rectangle VisualShape { get; set; }
        public TrafficLight()
        {
            CurrentColor = LightColor.green;
            VisualShape = new Rectangle
            {
                Width = 30,
                Height = 30,
                Fill = Brushes.Green
            };

        }
        public void ChangeColor()
        {
            if (CurrentColor == LightColor.red)
            {
                CurrentColor = LightColor.green;
                VisualShape.Fill = Brushes.Green;
            }
            else if (CurrentColor == LightColor.green)
            {
                CurrentColor = LightColor.yellow;
                VisualShape.Fill = Brushes.Yellow;
            }
            else
            {
                CurrentColor = LightColor.red;
                VisualShape.Fill = Brushes.Red;
            }
        }
    }
}
