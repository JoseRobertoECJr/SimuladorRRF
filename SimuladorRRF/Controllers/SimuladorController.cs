using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimuladorRRF.Classes;
using SimuladorRRF.Service;

namespace SimuladorRRF.Controllers
{
    [Route("api/[controller]")]
    public class SimuladorController : Controller
    {
        private readonly ISimuladorService _simuladorService;

        public SimuladorController(ISimuladorService simuladorService)
        {
            _simuladorService = simuladorService;
        }

        [HttpGet("[action]")]
        public List<Process> SimularProcessamento()
        {
            return _simuladorService.SimularProcessamento();
        }

        [HttpGet("[action]")]
        public List<Process> FixedData()
        {
            return _simuladorService.GetProcesses();
        }
    }
}
