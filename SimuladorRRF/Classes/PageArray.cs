using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimuladorRRF.Classes
{
    public class PageArray
    {
        private Page[] Value { get; }
        private readonly int _max = 64;

        private int?[] OlderProcessList;

        public PageArray()
        {
            OlderProcessList = new int?[_max];
            Value = new Page[_max];
        }

        public int Count
        {
            get
            {
                var i = 0;

                foreach (var page in Value)
                {
                    if (page != null)
                        i++;
                }

                return i;
            }
        }

        public int NumPages
        {
            get
            {
                return _max;
            }
        }

        public Page UseFramePage(int frame)
        {
            var page = Value[frame];
            var signal = false;
            int i;
            for (i = 0; i < _max; i++)
            {
                if (OlderProcessList[i] == null)
                    break;

                if (page.ProcessID == OlderProcessList[i])
                    signal = true;

                if (signal)
                    OlderProcessList[i] = OlderProcessList[i + 1];
            }

            OlderProcessList[i] = page.ProcessID;

            return page;
        }

        public int? GetOldestProcess(int processID)
        {
            foreach (var page in Value)
            {
                // ha espaco
                if (page == null)
                    return null;
            }

            foreach(var oldProcessID in OlderProcessList)
            {
                if (oldProcessID != processID)
                    return oldProcessID;
            }

            return null;
        }

        public int SwapIn(Page page)
        {
            int i;
            for (i = 0; i < NumPages; i++)
            {
                if (Value[i] == null)
                {
                    Value[i] = page;
                    break;
                }
            }

            // TODO: Alloca posicao em OlderProcessList
            UseFramePage(i);

            return i;
        }

        public int SwapInSameProcess(Page page, int oldestPageNum)
        {
            int i;
            for (i = 0; i < NumPages; i++)
            {
                if (Value[i].ProcessID == page.ProcessID && oldestPageNum == Value[i].PageNum)
                {
                    Value[i] = page;
                    break;
                }
            }

            // TODO: Renova posicao em OlderProcessList
            UseFramePage(i);

            return i;
        }

        public void SwapOut(int processID)
        {
            int i;

            // Tira paginas de process
            for (i = 0; i < NumPages; i++)
            {
                if (Value[i].ProcessID == processID)
                    Value[i] = null;
            }

            // Shift na OlderProcessList
            var signal = false;
            for (i = 0; i < NumPages; i++)
            {
                if (OlderProcessList[i] == processID)
                    signal = true;

                if (signal)
                    OlderProcessList[i] = OlderProcessList[i + 1];
            }
            OlderProcessList[i] = null;

        }

    }
}
