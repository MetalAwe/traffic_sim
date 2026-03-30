using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Traffic_Sim__Project_for_LP2_Class_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static double[] Lanes = { 60, 110, 180, 230 };
        //Lane rules:
        public static int BusLaneIndex = 0; //Buses must be in the top lane
        public static int MedianIndex = 1; //the double yellow line is between lanes 1 & 2
        List<Vehicle> allVehicles = new List<Vehicle>();
        TrafficLight mainLight = new TrafficLight();
        DispatcherTimer timer = new DispatcherTimer();
        Random rng = new Random();
        Brush[] availableColors =
        {
            Brushes.Black,
            Brushes.Red,
            Brushes.Blue,
            Brushes.White
        };
        public MainWindow()
        {
            InitializeComponent();

            //We create our objects
            Car car = new Car("B250POD", 10, 0, 1, Brushes.Blue);
            allVehicles.Add(car);
            TrafficCanvas.Children.Add(car.Shape);

            Car car2 = new Car("C250POD", 10, -100, 1, Brushes.Yellow);
            allVehicles.Add(car2);
            TrafficCanvas.Children.Add(car2.Shape);

            Ambulance ambulance = new Ambulance("A23145", 10, 0, 2, Brushes.White);
            allVehicles.Add(ambulance);
            TrafficCanvas.Children.Add(ambulance.Shape);

            Bus bus = new Bus("D250POD", 5, -200, 0, Brushes.Green);
            allVehicles.Add(bus);
            TrafficCanvas.Children.Add(bus.Shape);

            //We set up the timer to tick every 20 milliseconds
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += GameLoop; // This calls the GameLoop method every tick
            timer.Start();

            DrawRoad();
            
            //Position the light visually on the canvas:
            Canvas.SetLeft(mainLight.VisualShape, 300);
            Canvas.SetTop(mainLight.VisualShape, 142.5);
            TrafficCanvas.Children.Add(mainLight.VisualShape);
        }

        private void GameLoop(object sender, EventArgs e)
        {
            List<Vehicle> vehiclesToDelete = new List<Vehicle>();

            foreach(Vehicle vehicle in allVehicles)
            {
                //we run the logic
                vehicle.Update(mainLight, allVehicles);
                
                //Update the visual position on the canvas:
                Canvas.SetLeft(vehicle.Shape, vehicle.getXPosition());
                Canvas.SetTop(vehicle.Shape, vehicle.getYPosition());

                //So they don't drive off forever and ever:
                if (vehicle.getXPosition() > TrafficCanvas.ActualWidth)
                {
                    vehicle.setXPosition(-40);
                }
            }

            TxtVehicleCount.Text = allVehicles.Count.ToString();
            if(mainLight.CurrentColor == LightColor.green)
            {
                LightIndicator.Fill = Brushes.Green;
            }
            else if(mainLight.CurrentColor == LightColor.yellow)
            {
                LightIndicator.Fill = Brushes.Yellow;
            }
            else
            {
                LightIndicator.Fill = Brushes.Red;
            }

        }

        private void ChangeLight(object sender, RoutedEventArgs e)
        {
            mainLight.ChangeColor();
        }

        private void AddCar(object sender, RoutedEventArgs e)
        {
            int startingLane = rng.Next(0, MainWindow.Lanes.Length);
            double randomSpeed = rng.Next(5, 15);
            int colorIndex = rng.Next(availableColors.Length);
            Brush randomColor = availableColors[colorIndex];
            Car newCar = new Car("Car" + allVehicles.Count, randomSpeed, -100, startingLane, randomColor);
            allVehicles.Add(newCar);
            TrafficCanvas.Children.Add(newCar.Shape);
        }

        private void AddAmbulance(object sender, RoutedEventArgs e)
        {
            int startingLane = rng.Next(0, MainWindow.Lanes.Length);
            Ambulance newAmbulance = new Ambulance("Ambulance" + allVehicles.Count, 10, -100, startingLane, Brushes.White);
            allVehicles.Add(newAmbulance);
            TrafficCanvas.Children.Add(newAmbulance.Shape);
        }

        private void AddBus(object sender, RoutedEventArgs e)
        {
            Bus newBus = new Bus("D250POD", 5, -100, 0, Brushes.Green);
            allVehicles.Add(newBus);
            TrafficCanvas.Children.Add(newBus.Shape);
        }

        private void DrawRoad()
        {
            Rectangle topShoulder = new Rectangle { Width = 2000, Height = 10, Fill = Brushes.DarkGray };
            Canvas.SetTop(topShoulder, 40);
            TrafficCanvas.Children.Add(topShoulder);

            Rectangle bottomShoulder = new Rectangle { Width = 2000, Height = 10, Fill = Brushes.DarkGray };
            Canvas.SetTop(bottomShoulder, 260);
            TrafficCanvas.Children.Add(bottomShoulder);

            Rectangle doubleLine1 = new Rectangle { Width = 2000, Height = 5, Fill = Brushes.Yellow };
            Canvas.SetTop(doubleLine1, 145);
            Rectangle doubleLine2 = new Rectangle { Width = 2000, Height = 5, Fill = Brushes.Yellow };
            Canvas.SetTop(doubleLine2, 165);
            TrafficCanvas.Children.Add(doubleLine1);
            TrafficCanvas.Children.Add(doubleLine2);

            for (int i = 0; i < 2000; i++)
            {
                Line dashedLane1 = new Line
                {
                    X1 = i * 40,
                    Y1 = 95,
                    X2 = i * 40 + 20,
                    Y2 = 95,
                    Stroke = Brushes.White,
                    StrokeThickness = 2
                };
                Line dashedLane2 = new Line
                {
                    X1 = i * 40,
                    Y1 = 215,
                    X2 = i * 40 + 20,
                    Y2 = 215,
                    Stroke = Brushes.White,
                    StrokeThickness = 2
                };
                TrafficCanvas.Children.Add(dashedLane1);
                TrafficCanvas.Children.Add(dashedLane2);
            }
        }
    }
}