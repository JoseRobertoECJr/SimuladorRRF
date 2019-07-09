using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Classes
{
    public class PageTableArray
    {
        public PageTable[] Value { get; set; }

        public PageTableArray(int maxProcesses)
        {
            Value = new PageTable[maxProcesses];
        }

        public void LimpaPageTable(int processID)
        {
            // Acha a pagina do process
            int i;
            for (i = 0; i < Value.Count(); i++)
            {
                if (Value[i].ProcessID == processID)
                    break;
            }

            // Remove a referencia a memoria principal
            for (var j = 0; j < Value[i].FrameList.Count(); j++)
            {
                Value[i].FrameList[j] = null;
            }

            for(var j = 0; j < Value[i].WSL; j++)
            {
                Value[i].WorkingSet[j] = null;
            }

        }

        public void AtualizaPageTable(Process process, int pageNum, int enderecoReal)
        {
            // Vasculha o PageTableArray procurando a PageTable do processo
            foreach (var pageTable in Value)
            {
                if (pageTable.ProcessID == process.Id)
                {
                    pageTable.Atualiza(pageNum, enderecoReal);
                    break;
                }
            }
        }
    }
}
