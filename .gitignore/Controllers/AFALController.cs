using AFAL_WS.Models;
using FuzzyLogic_FIS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AFAL_WS.Controllers
{
    public class AFALController : ApiController
    {
        public IHttpActionResult Post(ElectricalConsumption EC)
        {
            double in1, in2, in3;
            Execute ex; //FuzzyLogic Library
            double[] outputs;

            outputs = new double[3];

            in1 = EC.L1 - EC.L2;
            in2 = EC.L2 - EC.L3;
            in3 = EC.L3 - EC.L1;

            ex = new Execute(in1, in2, in3);

            ex.KnowledgeBaseFuzzification(in1, in2, in3);
            ex.Inference();
            outputs = ex.Deffuzzification();

            Classifier c = new Classifier
            {
                o1 = outputs[0],
                o2 = outputs[1],
                o3 = outputs[2],
            };

            return Ok(c);
        }



    }
}
