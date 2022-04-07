using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleCollision.Models.DataAnalytics
{
    public class SeverityPrediction
    {
        public float LatUtmY { get; set; }
        public float LongUtmX { get; set; }
        public float PedestrianInvolved { get; set; }
        public float BicyclistInvolved { get; set; }
        public float MotorcycleInvolved { get; set; }
        public float ImproperRestraint { get; set; }
        public float Unrestrained { get; set; }
        public float Dui { get; set; }
        public float IntersectionRelated { get; set; }
        public float WildAnimalRelated { get; set; }

        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
            LatUtmY, LongUtmX, PedestrianInvolved, BicyclistInvolved,
            MotorcycleInvolved, ImproperRestraint, Unrestrained, Dui,
            IntersectionRelated, WildAnimalRelated
            };
            int[] dimensions = new int[] { 1, 10 };
            return new DenseTensor<float>(data, dimensions);
        }
    }
}
