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

        [HttpGet("[action]")]
        public int CycleLength()
        {
            return _simuladorService.GetCycleLength();
        }

        [HttpPost("[action]")]
        public void ChangeProcessListData([FromBody] List<Process> processList)
        {
            _simuladorService.ChangeProcessListData(processList);
        }

        [HttpGet("[action]")]
        public void ChangeCycleLength(int cycleLength)
        {
            _simuladorService.ChangeCycleLength(cycleLength);
        }

    }
}
