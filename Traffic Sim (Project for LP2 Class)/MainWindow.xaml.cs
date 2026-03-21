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
        List<Vehicle> allVehicles = new List<Vehicle>();
        TrafficLight mainLight = new TrafficLight();
        DispatcherTimer timer = new DispatcherTimer();
        Random rng = new Random();
        Brush[] availableColors =
        {
            Brushes.Black,
            Brushes.Red,
            Brushes.Green,
            Brushes.Blue,
            Brushes.White
        };
        public MainWindow()
        {
            InitializeComponent();

            //We create our objects
            Car car = new Car("B250POD", 10, 0, 100, Brushes.Blue);
            allVehicles.Add(car);
            TrafficCanvas.Children.Add(car.Shape);

            Car car2 = new Car("C250POD", 10, -100, 100, Brushes.Green);
            allVehicles.Add(car2);
            TrafficCanvas.Children.Add(car2.Shape);

            Ambulance ambulance = new Ambulance("A23145", 10, 0, 200, Brushes.White);
            allVehicles.Add(ambulance);
            TrafficCanvas.Children.Add(ambulance.Shape);

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
            double randomY = rng.Next(0, 2) == 0 ? 50 : 100;
            double randomSpeed = rng.Next(5, 15);
            int colorIndex = rng.Next(availableColors.Length);
            Brush randomColor = availableColors[colorIndex];
            Car newCar = new Car("Car" + allVehicles.Count, randomSpeed, -100, randomY, randomColor);
            allVehicles.Add(newCar);
            TrafficCanvas.Children.Add(newCar.Shape);
        }

        private void AddAmbulance(object sender, RoutedEventArgs e)
        {
            Ambulance newAmbulance = new Ambulance("B23145", 10, -100, 200, Brushes.White);
            allVehicles.Add(newAmbulance);
            TrafficCanvas.Children.Add(newAmbulance.Shape);
        }
    }
}