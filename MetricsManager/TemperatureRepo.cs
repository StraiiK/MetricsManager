using System;
using System.Collections.Generic;
using System.Linq;

namespace MetricsManager
{
    public class TemperatureRepo
    {
        private List<WeatherMetrics> _weatherMetricsList;

        public TemperatureRepo()
        {
            _weatherMetricsList = new List<WeatherMetrics>();
        }

        public void SaveTemperature(DateTime date, double temperature)
        {
            _weatherMetricsList.Add(new WeatherMetrics { Date = date, Temperature = temperature });
        }

        public void EditTemperature(DateTime date, double newTemperature)
        {
            var existingData = _weatherMetricsList.FirstOrDefault(data => data.Date == date);
            if (existingData != null)
            {
                existingData.Temperature = newTemperature;
            }
        }

        public void DeleteTemperatures(DateTime date, DateTime endTime)
        {
            _weatherMetricsList.RemoveAll(data => data.Date >= date && data.Date <= endTime);
        }

        public List<WeatherMetrics> GetTemperatures(DateTime date, DateTime endTime)
        {
            return _weatherMetricsList.Where(data => data.Date >= date && data.Date <= endTime).ToList();
        }

        public List<WeatherMetrics> GetTemperaturesAll()
        {
            return _weatherMetricsList;
        }
    }
}