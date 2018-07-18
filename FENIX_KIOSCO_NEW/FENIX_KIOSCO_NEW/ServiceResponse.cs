using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
namespace FENIX_KIOSCO
{
    public class ServiceResponse
    {
        public string estado { get; set; }
        public datos datos { get; set; }
        public string mensaje { get; set; }
        public ServiceResponse()
        {
        }
    }

    public class datos
    {
        public string secuencia { get; set; }
        public int fechaGeneracion { get; set; }
    }

    
}
