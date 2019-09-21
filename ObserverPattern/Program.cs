using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            WeatherData wd = new WeatherData();
            CurrentConditionsDisplay cd = new CurrentConditionsDisplay(wd);

            wd.SetMeasurements(80, 65, 30.4f);
            wd.SetMeasurements(82, 70, 29.2f);
            wd.SetMeasurements(78, 90, 29.2f);

            while (true)
            {

            }
        }
    }

    interface Subject
    {
        void RegisterObserver(Observer o);
        void RemoveObserver(Observer o);
        void NotifyObserver();
    }

    interface Observer
    {
        void Update(float _temperature, float _humidity, float _pressure);
    }

    interface DisplayElement
    {
        void Display();
    }

    class WeatherData : Subject
    {
        private List<Observer> listObserver;
        private float temperature;
        private float humidity;
        private float pressure;

        public WeatherData()
        {
            listObserver = new List<Observer>();
        }

        public void RegisterObserver(Observer o)
        {
            listObserver.Add(o);
        }

        public void RemoveObserver(Observer o)
        {
            listObserver.Remove(o);
        }

        public void NotifyObserver()
        {
            foreach (Observer o in listObserver)
            {
                o.Update(temperature, humidity, pressure);
            }
        }

        public void MeasurementsChanged()
        {
            NotifyObserver();
        }

        public void SetMeasurements(float _temperature, float _humidity, float _pressure)
        {
            this.temperature = _temperature;
            this.humidity = _humidity;
            this.pressure = _pressure;
            MeasurementsChanged();
        }
    }

    class CurrentConditionsDisplay : Observer, DisplayElement
    {
        private float temperature;
        private float humidity;
        private Subject weatherData;

        public CurrentConditionsDisplay(Subject _weatherData)
        {
            this.weatherData = _weatherData;
            weatherData.RegisterObserver(this);
        }

        public void Update(float _temperature, float _humidity, float _pressure)
        {
            this.temperature = _temperature;
            this.humidity = _humidity;
            Display();
        }

        public void Display()
        {
            Console.WriteLine("Current Condition: " + temperature.ToString() + "F degrees and " + humidity + "% humidity");
        }        
    }
}
