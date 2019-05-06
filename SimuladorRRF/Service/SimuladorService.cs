using SimuladorRRF.Classes;
using SimuladorRRF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SimuladorRRF.Utils;

namespace SimuladorRRF.Service
{
    public class SimuladorService : ISimuladorService
    {
        private readonly IProcessData _processData;
        private List<Process> _processList;
        private Fila<Process> _processFila;
        private int _instante;
        private int _cycleLength;

        public SimuladorService(IProcessData processData)
        {
            _processData = processData;

            // Pegando Dados
            _processList = new List<Process>(_processData.GetProcesses());
            _cycleLength = _processData.GetCycleLength();
            _processFila = new Fila<Process>();
        }

        #region Get and Set Data

        public List<Process> GetProcesses()
        {
            return _processData.GetProcesses();
        }
        public void SetProcesses(List<Process> newProcesses)
        {
            _processData.SetProcesses(newProcesses);
        }

        public int GetCycleLength()
        {
            return _processData.GetCycleLength();
        }

        public void ChangeProcessListData(List<Process> processList)
        {
            _processData.SetProcesses(processList);
        }

        public void ChangeCycleLength(int cycleLength)
        {
            _processData.SetCycleLength(cycleLength);
        }


        #endregion

        #region Main

        public List<Process> SimularProcessamento()
        {
            var process = new Process();
            var finishedProcessList = new List<Process>();

            while (TemProcesso())
            {
                // Roda o processo (nao executa no primeiro loop)
                if (_instante != 0)
                    process = Run();

                // Verifica se chegaram novos processos e coloca na fila
                NextProcesses();

                // Decide se processo vai para fila, se termina, ou se retornara depois por conta de algum I/O (nao executa no primeiro loop)
                if (process.Id != null)
                    finishedProcessList = FinishOrQueueProcess(process, finishedProcessList);
            }

            return finishedProcessList;

        }

        #endregion

        #region Metodos

        private Process Run()
        {
            var process = _processFila.Remove();

            // Calcula tempo de execucao
            var processTempo = _processData.GetCycleLength(); ;
            var tempoRestante = process.TempoCPU - process.TempoExecutado;
            if (processTempo > tempoRestante)
            {
                processTempo = tempoRestante;
            }

            // Cria bloco anterior ao do processamento
            process.Blocks.Add(new Block()
            {
                Tipo = BlockTipoEnum.NonExec,
                Tempo = _instante - process.TurnAround
            });

            // Cria Bloco do processo e altera a ProxChegada
            process.Blocks.Add(new Block {
                Tipo =  BlockTipoEnum.Processo,
                Tempo = processTempo
            });

            // Define prox instante
            _instante = process.TurnAround;

            return process;
        }

        private void NextProcesses()
        {
            var newProcessList = new List<Process>();
            
            // Verifica se chegou algum processo
            foreach (var process in _processList)
            {
                if (process.ProxChegada <= _instante)
                {
                    newProcessList.Add(process);
                }
            }

            // Ordena os processos na Fila
            while(newProcessList.Count > 0)
            {
                var nextInFila = new Process();
                foreach(var nP in newProcessList)
                {
                    if (nP == newProcessList[0])
                        nextInFila = nP;

                    if (nextInFila.ProxChegada > nP.ProxChegada)
                        nextInFila = nP;
                }
                newProcessList.Remove(nextInFila);
                _processList.Remove(nextInFila);
                _processFila.Add(new Process(nextInFila));
            }


            // Vai para o proximo instante de tempo se nao ha processo na fila, coloca processos na Fila
            if (_processFila.Count == 0 && _processList.Count != 0)
            {
                // Define qual sera o instante inicial
                foreach (var processo in _processList)
                {
                    if (processo == _processList[0])
                        _instante = processo.ProxChegada;

                    if (_instante > processo.ProxChegada)
                        _instante = processo.ProxChegada;
                }
                NextProcesses();
            }

        }

        private List<Process> FinishOrQueueProcess(Process process, List<Process> finishedProcessList)
        {
            // Se processo terminou, coloca na finishedProcessList
            if (process.TempoCPU == process.TempoExecutado)
            {
                finishedProcessList.Add(process);
                return finishedProcessList;
            }

            /*
             *
             *
             *  TODO: NAO TEMOS OUTROS TIPOS DE BLOCOS NA EXECUCAO AINDA
             *
             * 
             */
            if (process.Blocks.Last().Tipo != BlockTipoEnum.Processo)
            {
                _processList.Add(process);

                return finishedProcessList;
            }

            // Adiciona processo executado na Fila
            _processFila.Add(process);


            return finishedProcessList;
        }

        private bool TemProcesso()
        {
            if (_processFila.Count == 0 && _processList.Count == 0)
                return false;

            return true;
        }

        #endregion
    }
}
