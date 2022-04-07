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
        public float OverturnRollover { get; set; }
        public float CommercialMotorVehInvolved { get; set; }
        public float TeenageDriverInvolved { get; set; }
        public float OlderDriverInvolved { get; set; }
        public float NightDarkCondition { get; set; }
        public float DistractedDriving { get; set; }
        public float DrowsyDriving { get; set; }
        public float RoadwayDeparture { get; set; }
        public float CityOther { get; set; }
        public float CitySaltLakeCity { get; set; }
        public float CityWestValleyCity { get; set; }
        public float CountyNameOther { get; set; }
        public float CountyNameUtah { get; set; }
        public float CountyNameWeber { get; set; }

        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
            LatUtmY, LongUtmX, PedestrianInvolved, BicyclistInvolved,
            MotorcycleInvolved, ImproperRestraint, Unrestrained, Dui,
            IntersectionRelated, WildAnimalRelated, OverturnRollover,
            CommercialMotorVehInvolved, TeenageDriverInvolved,
            OlderDriverInvolved, NightDarkCondition, DistractedDriving,
            DrowsyDriving, RoadwayDeparture, CityOther, CitySaltLakeCity,
            CityWestValleyCity, CountyNameOther, CountyNameUtah,
            CountyNameWeber
            };
            int[] dimensions = new int[] { 1, 24 };
            return new DenseTensor<float>(data, dimensions);
        }
    }
}
