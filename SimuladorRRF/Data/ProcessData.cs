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

        public PageTable GetPageTable(Process process)
        {
            PageTable processPageTable = null;
            foreach(var pageTable in PageTableArray.Value)
            {
                if (pageTable.ProcessID == process.Id)
                {
                    processPageTable = pageTable;
                    break;
                }
            }

            return processPageTable;
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
                processPageTable.TableRowArray[j].frame = null;
            }
        }

        public Page GetMemPrincipalFrame(int frame)
        {
            return MemPrincipal.UseFramePage(frame);
        }

        public int? GetOldestProcess()
        {
            return MemPrincipal.GetOldestProcess();
        }

        public int SwapInSameProcess(Page page, int oldestPageNum)
        {
            return MemPrincipal.SwapInSameProcess(page, oldestPageNum);
        }

        public int SwapIn(Page page)
        {
            return MemPrincipal.SwapIn(page);
        }

        public void SwapOut(Process oldProcess)
        {
            // Vasculha o PageTableArray procurando a PageTable do processo
            foreach(var pageTable in PageTableArray.Value)
            {
                if (pageTable.ProcessID == oldProcess.Id)
                {
                    // Atualiza o frame da página com o endereco nulo
                    for (var i = 0; i < pageTable.TableRowArray.Count(); i++)
                    {
                        pageTable.TableRowArray[i].frame = null;
                    }
                    break;
                }
            }
        }

        public void AtualizaPageTable(Process process, int pageNum, int enderecoReal)
        {
            // Vasculha o PageTableArray procurando a PageTable do processo
            foreach(var pageTable in PageTableArray.Value)
            {
                if (pageTable.ProcessID == process.Id)
                {
                    // Atualiza o frame da página com o endereco real
                    pageTable.TableRowArray[pageNum].frame = enderecoReal;
                    break;
                }
            }
        }

    }
}
