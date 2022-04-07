using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleCollision.Models.DataAnalytics;

namespace VehicleCollision.Controllers
{
    public class DataAnalyticsController : Controller
    {
        private InferenceSession _session;

        public DataAnalyticsController(InferenceSession session)
        {
            _session = session;
        }
        public IActionResult Index()
        {
            return View("LandingPage");
        }
        
        [HttpGet]
        public IActionResult SeverityPredicter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SeverityPredicter(SeverityPrediction data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
            });

            Tensor<long> score = result.First().AsTensor<long>();
            var prediction = score.First();
            result.Dispose();
            ViewBag.Prediction = prediction;

            return View();
        }

        public IActionResult Analytics()
        {
            return View("Visualizations");
        }
    }

}
