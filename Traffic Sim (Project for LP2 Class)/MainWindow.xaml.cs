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
        public static double[] Lanes = { 50, 100, 200 };
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

            //Position the light visually on the canvas:
            Canvas.SetLeft(mainLight.VisualShape, 300);
            Canvas.SetTop(mainLight.VisualShape, 150);
            TrafficCanvas.Children.Add(mainLight.VisualShape);

            //We set up the timer to tick every 20 milliseconds
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += GameLoop; // This calls the GameLoop method every tick
            timer.Start();
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
    }
}