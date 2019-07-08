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

        //public PageArray(Page[] pages)
        //{
        //    Value = new Page[_max];
        //    for (var i = 0; i < pages.Length; i++)
        //    {
        //        if (pages != null)
        //            Value[i] = pages[i];
        //    }
        //}

        //public void Add(Page v)
        //{
        //    for (var i = 0; i < _max; i++)
        //    {
        //        if (Value[i] == null)
        //        {
        //            Value[i] = v;
        //            break;
        //        }

        //    }
        //}

        //public void Remove(Page v)
        //{
        //    for (var i = 0; i < _max; i++)
        //    {
        //        if (Value[i] != null && Value[i].Id == v.Id)
        //            Value[i] = null;
        //    }
        //}

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
            for (i = 0; i < _max || OlderProcessList[i] == null; i++)
            {
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

        private void InsertNewProcess()
        {
            // Pega o processo no OlderProcessList e renova para o final


            // Insert no processo na memoria
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

            return i;
        }

        public int SwapInSameProcess(Page page, int oldestPageNum)
        {
            for (var i = 0; i < NumPages; i++)
            {
                if (Value[i].ProcessID == page.ProcessID && oldestPageNum == Value[i].PageNum)
                {
                    Value[i] = page;
                    break;
                }
            }

            return page.PageNum;
        }

        public void SwapOut(int processID)
        {

        }

    }
}
