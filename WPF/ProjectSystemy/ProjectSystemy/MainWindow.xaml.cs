using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media;

namespace ProjectSystemy
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer _trainTimer;
        private DispatcherTimer _carTimer;
        private readonly Random _random = new();
        private bool _isTrainMoving;
        private readonly double _carSpeed = 50.0; // Speed of the car
        private readonly double _trainSpeed = 200.0; // Speed of the train

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimers();
        }

        private void InitializeTimers()
        {
            // Timer for the train
            _trainTimer = new DispatcherTimer();
            _trainTimer.Interval = TimeSpan.FromSeconds(10);
            _trainTimer.Tick += TrainTimer_Tick;
            _trainTimer.Start();

            // Timer for the car
            _carTimer = new DispatcherTimer();
            _carTimer.Interval = TimeSpan.FromMilliseconds(100);
            _carTimer.Tick += CarTimer_Tick;
            _carTimer.Start();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Move car to the middle of horizontal axis
            Canvas.SetLeft(carRectangle, (MainCanvas.ActualWidth - carRectangle.ActualWidth) / 2);
            
            // Move car to the top of the screen
            Canvas.SetTop(carRectangle, -carRectangle.Height);
        }

        private void TrainTimer_Tick(object sender, EventArgs e)
        {
            // Randomly decide whether to start the train
            if (_random.NextDouble() < 0.5) // 50% chance
            {
                StartTrain();
            }
        }

        private void StartTrain()
        {
            _isTrainMoving = true;
            StopCars();
            trainRectangle.Visibility = Visibility.Visible; // Make sure the train is visible when it starts

            // Correct the way we get the 'from' and 'to' values for the animation
            double trainStart = -trainRectangle.Width; // Use Width instead of ActualWidth
            double trainEnd = MainCanvas.ActualWidth; // This should now resolve correctly

            // Pass the Canvas as a parameter if needed
            AnimateRectangle(trainRectangle, trainStart, trainEnd, _trainSpeed, MainCanvas, () =>
            {
                _isTrainMoving = false;
                StartCars();
                trainRectangle.Visibility = Visibility.Hidden; // Hide the train after animation
            });
        }

        private void CarTimer_Tick(object sender, EventArgs e)
        {
            if (!_isTrainMoving)
            {
                // // Move the car vertically
                // double newY = Canvas.GetTop(carRectangle) + carSpeed * carTimer.Interval.TotalSeconds;
                // if (newY > MainCanvas.ActualHeight - carRectangle.ActualHeight)
                // {
                //     newY = 0; // Reset car position if it goes off screen
                // }
                // Canvas.SetTop(carRectangle, newY);
                
                // Assuming the car moves vertically down along the y-axis
                // Adjust the position of the car by changing the Y property of TranslateTransform
                TranslateTransform transform = (TranslateTransform)carRectangle.RenderTransform;
                transform.Y += _carSpeed * _carTimer.Interval.TotalSeconds;

                // If the car goes off the screen, reset its position to the top
                if (transform.Y >= MainCanvas.ActualHeight)
                {
                    transform.Y = -carRectangle.Height;
                }
            }
        }

        private void StopCars()
        {
            _carTimer.Stop();
        }

        private void StartCars()
        {
            _carTimer.Start();
        }

        private void AnimateRectangle(Rectangle rect, double from, double to, double speed, Canvas canvas, Action onComplete)
        {
            double duration = Math.Abs(to - from) / speed;

            var animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(duration),
                FillBehavior = FillBehavior.Stop // This will prevent the rectangle from snapping back to the original position after the animation completes
            };

            animation.Completed += (s, e) =>
            {
                // Reset the transform after animation to prevent accumulation of transform values
                rect.RenderTransform = new TranslateTransform();
                onComplete?.Invoke();
            };

            // Make sure the transform is applied to the correct property and the object is set up properly
            TranslateTransform transform = rect.RenderTransform as TranslateTransform ?? new TranslateTransform();
            rect.RenderTransform = transform;
            transform.BeginAnimation(TranslateTransform.XProperty, animation);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Code to start the simulation
            StartCars();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            // Code to stop the simulation
            StopCars();
            _trainTimer.Stop();
        }

        private void RunTrainButton_Click(object sender, RoutedEventArgs e)
        {
            // Code to trigger the train immediately
            if (!_isTrainMoving)
            {
                StartTrain();
            }
        }
    }
}
