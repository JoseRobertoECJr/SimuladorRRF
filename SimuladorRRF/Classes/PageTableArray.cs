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
                var pageTable = Value[i];

                if (pageTable.ProcessID == processID)
                    break;
            }

            // Remove a referencia a memoria principal
            var processPageTable = Value[i];
            for (var j = 0; j < processPageTable.TableRowArray.Count(); j++)
            {
                processPageTable.TableRowArray[j].frame = null;
            }
        }

        public void AtualizaPageTable(Process process, int pageNum, int enderecoReal)
        {
            // Vasculha o PageTableArray procurando a PageTable do processo
            foreach (var pageTable in Value)
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
