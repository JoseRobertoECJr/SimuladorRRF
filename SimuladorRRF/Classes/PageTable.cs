﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Classes
{
    public class PageTable
    {
        public int ProcessID { get; set; }
        public int?[] FrameList { get; set; }
        public readonly int WSL = 4;
        public int?[] WorkingSet { get; set; }

        public PageTable(Process process)
        {
            ProcessID = process.Id;
            WorkingSet = new int?[WSL];
            FrameList = new int?[process.NumPags];
        }

        public int QntInMem
        {
            get
            {
                var i = 0;
                foreach (var pageNum in WorkingSet)
                {
                    if (pageNum != null)
                        i++;
                }
                return i;
            }
        }

        public void Atualiza(int pageNum, int enderecoReal)
        {
            // Atualiza o frame da página com o endereco real
            FrameList[pageNum] = enderecoReal;

            if (QntInMem == WSL)
            {
                var olderPage = Shift();
                FrameList[olderPage] = null;
            }

            for (var i = 0; i < WSL; i++)
            {
                if (WorkingSet[i] == null)
                {
                    WorkingSet[i] = pageNum;
                    break;
                }
            }
        }

        private int Shift()
        {
            var olderPage = WorkingSet[0];

            for (var i = 0; i < WSL; i++)
            {
                WorkingSet[i] = WorkingSet[i + 1];
            }

            return (int)olderPage;
        }

    }
}
