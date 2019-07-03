using SimuladorRRF.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Data
{
    public class ProcessData : IProcessData
    {
        private int CycleLength = 3;
        public readonly int _maxProcesses = 1000;
        public int qntProcesses = 0;

        private ProcessArray Processes;

        public PageTableArray PageTableArray { get; set; }

        public PageArray MemPrincipal { get; set; }

        public ProcessData()
        {
            PageTableArray = new PageTableArray(_maxProcesses);
            Processes = new ProcessArray();

            Processes.Add(new Process
            {
                Id = 1,
                Chegada = 2,
                TempoCPU = 15,
                Blocks = new BlockArray()
            });
        }

        public ProcessArray GetProcesses()
        {
            return Processes;
        }

        public void SetProcesses(ProcessArray newProcesses)
        {
            Processes = newProcesses;
        }

        public int GetCycleLength()
        {
            return CycleLength;
        }

        public void SetCycleLength(int newLength)
        {
            CycleLength = newLength;
        }

        public int? GetPageTableFrameAddress(Process process, int pageNum)
        {
            PageTable processPage = null;
            foreach(var page in PageTableArray.Value)
            {
                if (page.ProcessID == process.Id)
                {
                    processPage = page;
                    break;
                }
            }

            return processPage.TableRowArray[pageNum].frame;
        }

        public void LimpaPageTable(Process process)
        {
            // Acha a pagina do process
            int i;
            for (i = 0; i < PageTableArray.Value.Count(); i++)
            {
                var pageTable = PageTableArray.Value[i];

                if (pageTable.ProcessID == process.Id)
                    break;
            }

            // Remove a referencia a memoria principal
            var processPageTable = PageTableArray.Value[i];
            for (var j = 0; j < processPageTable.TableRowArray.Count(); j++)
            {
                PageTableArray.Value[i].TableRowArray[j].frame = null;
            }
        }

        public Page GetMemPrincipalFrame(int frame)
        {
            return MemPrincipal.Value[frame];
        }

    }
}
