using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Gpio;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MorseCode
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private const int LED_PIN = 5;
        private const string LINE = "-";
        private const string DOT = ".";
        private GpioPin pin;
        private GpioPinValue pinValue;
        private readonly int intervalBetweenLetters = 800;
        private readonly int intervalBetweenSymbols = 250;
        private readonly int intervalBetweenWords = 1000;

        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO();
        }

        private void sendText_Click(object sender, RoutedEventArgs e)
        {
            sendText.IsEnabled = false;
            var stringToConvert = textToConvert.Text.ToUpper().Trim();
            var convertedString = convertTextToMorse(stringToConvert);
            BlinkTextOnLeds(convertedString);
            textToConvert.Text = "";
            sendText.IsEnabled = true;
        }

        private void BlinkTextOnLeds(List<string> convertedString)
        {
            foreach (string letter in convertedString)
            {
                blinkLetter(letter);
                if (isInvalidLetter(letter))
                {
                    sleep(intervalBetweenWords);
                }
                else
                {
                    sleep(intervalBetweenLetters);
                }
            }
        }

        private bool isInvalidLetter(string letter)
        {
            return (letter != LINE && letter != DOT);
        }

        private void blinkLetter(string letter)
        {
            for (int i = 0; i < letter.Length; i++)
            {
                turnLedOn(timeToBlink(letter.Substring(i, 1)));
                sleep(intervalBetweenSymbols);
            }
        }

        private int timeToBlink(string character)
        {
            return character == LINE? 300:100;
        }

        private static List<string> convertTextToMorse(string stringToConvert)
        {
            var convertedString = new List<string>();
            for (int i = 0; i < stringToConvert.Length; i++)
            {
                string c = stringToConvert.Substring(i, 1);
                convertedString.Add(MorseCodeDictionary.ConvertToMorse(c));
            }
            return convertedString;
        }

        private void turnLedOn(int time)
        {
            pinValue = GpioPinValue.Low;
            pin.Write(pinValue);

            sleep(time);
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(time);
            timer.Start();
            timer.Stop();
            pinValue = GpioPinValue.High;
            pin.Write(pinValue);

        }

        private void sleep(int time)
        {
            new System.Threading.ManualResetEvent(false).WaitOne(time);
        }

        private void Timer_Tick(object sender, object e)
        {
            if (pinValue == GpioPinValue.High)
            {
                pinValue = GpioPinValue.Low;
                pin.Write(pinValue);
                //LED.Fill = redBrush;
            }
            else
            {
                pinValue = GpioPinValue.High;
                pin.Write(pinValue);
                //LED.Fill = grayBrush;
            }
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                pin = null;
                //GpioStatus.Text = "There is no GPIO controller on this device.";
                return;
            }

            pin = gpio.OpenPin(LED_PIN);
            pinValue = GpioPinValue.High;
            pin.Write(pinValue);
            pin.SetDriveMode(GpioPinDriveMode.Output);

            //GpioStatus.Text = "GPIO pin initialized correctly.";

        }
    }
}
