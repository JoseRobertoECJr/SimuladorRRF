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

            MemPrincipal = new PageArray();

            GenerateProcessList();

        }

        private void GenerateProcessList()
        {
            for (var i = 0; i < 100; i++)
            {
                Processes.Add(new Process
                {
                    Id = i + 1,
                    Chegada = (new Random()).Next(0, 100),
                    TempoCPU = (new Random()).Next(4, 15),
                    Blocks = new BlockArray(),
                    NumPags = (new Random()).Next(4, 9)
                });
            }
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

        public PageTable GetPageTable(Process process)
        {
            // Procura page table
            PageTable processPageTable = null;
            foreach(var pageTable in PageTableArray.Value)
            {
                if (pageTable != null && pageTable.ProcessID == process.Id)
                {
                    processPageTable = pageTable;
                    break;
                }
            }

            if(processPageTable == null)
            {
                for (var i = 0; i < PageTableArray.Value.Count(); i++)
                {
                    if (PageTableArray.Value[i] == null)
                    {
                        PageTableArray.Value[i] = new PageTable(process);
                        return PageTableArray.Value[i];
                    }
                }
            }

            return processPageTable;
        }

        public void LimpaPageTable(int processID)
        {
            PageTableArray.LimpaPageTable(processID);
        }

        public Page GetMemPrincipalFrame(int frame)
        {
            return MemPrincipal.UseFramePage(frame);
        }

        public int? GetOldestProcess(int processID)
        {
            return MemPrincipal.GetOldestProcess(processID);
        }

        public int SwapInSameProcess(Page page, int oldestFrame, int oldestPageNum)
        {
            return MemPrincipal.SwapInSameProcess(page, oldestFrame);
        }

        public int SwapIn(Page page)
        {
            return MemPrincipal.SwapIn(page);
        }

        public void SwapOut(int oldProcessID)
        {
            MemPrincipal.SwapOut(oldProcessID);
        }

        public void AtualizaPageTable(Process process, int pageNum, int enderecoReal)
        {
            PageTableArray.AtualizaPageTable(process, pageNum, enderecoReal);
        }

    }
}
