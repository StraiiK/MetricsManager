using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MetricsManager
{
    [Route("api/WeatherMetrics")]
    [ApiController]
    public class WeatherMetricsController : ControllerBase
    {
        private TemperatureRepo _temperatureRepo;

        public WeatherMetricsController(TemperatureRepo temperatureRepo)
        {
            _temperatureRepo = temperatureRepo;
        }

        [HttpGet]
        public IActionResult GetTemperaturesAll()
        {
            var temperatures = _temperatureRepo.GetTemperaturesAll();
            if (temperatures.Count == 0)
            {
                return NotFound();
            }

            return Ok(temperatures);
        }

        [HttpGet]
        public IActionResult GetTemperatures(DateTime date, DateTime endTime)
        {
            var temperatures = _temperatureRepo.GetTemperatures(date, endTime);
            if (temperatures.Count == 0)
            {
                return NotFound();
            }

            return Ok(temperatures);
        }

        [HttpPost]
        public IActionResult SaveTemperature([FromQuery] DateTime timeStamp, [FromQuery] double temperature)
        {
            _temperatureRepo.SaveTemperature(timeStamp, temperature);
            return Ok();
        }

        [HttpPost]
        public IActionResult EditTemperature([FromQuery] DateTime timeStamp, [FromQuery] double newTemperature)
        {
            _temperatureRepo.EditTemperature(timeStamp, newTemperature);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(DateTime date, DateTime endTime)
        {
            _temperatureRepo.DeleteTemperatures(date, endTime);
            return Ok();
        }
    }
}
