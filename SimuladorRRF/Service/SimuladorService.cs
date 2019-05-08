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
        private ProcessArray _processList;
        private Fila<Process> _baixaPrFila;
        private Fila<Process> _altaPrFila;
        private int _instante;
        private int _ioInstante;
        private int _cycleLength;

        public SimuladorService(IProcessData processData)
        {
            _processData = processData;

            _processList = new ProcessArray(_processData.GetProcesses().Value);

            _baixaPrFila = new Fila<Process>();
            _altaPrFila = new Fila<Process>();

            _instante = 0;
            _ioInstante = 0;

            _cycleLength = _processData.GetCycleLength();
        }

        #region Get and Set Data

        public ProcessArray GetProcesses()
        {
            return _processData.GetProcesses();
        }
        public void SetProcesses(ProcessArray newProcesses)
        {
            _processData.SetProcesses(newProcesses);
        }

        public int GetCycleLength()
        {
            return _processData.GetCycleLength();
        }

        public void ChangeProcessListData(ProcessArray processList)
        {
            _processData.SetProcesses(processList);
        }

        public void ChangeCycleLength(int cycleLength)
        {
            _processData.SetCycleLength(cycleLength);
        }


        #endregion

        public ProcessArray SimularProcessamento()
        {
            var process = new Process();
            var finishedProcessList = new ProcessArray();

            var pulaInstante = false;

            while (TemProcesso())
            {
                // Roda um processo se ha processos na fila
                if (_altaPrFila.Count > 0 || _baixaPrFila.Count > 0)
                    process = Run();
                
                // Verifica se chegaram novos processos e coloca na fila
                pulaInstante = NextProcesses();

                // Decide se processo vai para fila, se termina, ou se retornara depois por conta de algum I/O (nao executa no primeiro loop)
                if (process.Id != null)
                {
                    finishedProcessList = FinishOrQueueProcess(process, finishedProcessList);
                    process.Id = null;
                }

                // Pula para o proximo instante de tempo se ainda nao chegaram processos
                if (pulaInstante)
                {
                    JumpTime();
                    pulaInstante = false;
                }
            }

            return finishedProcessList;

        }

        #region Metodos

        private Process Run()
        {
            var process = new Process();

            // Decide qual das filas de prioridade vai servir
            if (_altaPrFila.Count == 0)
                process = _baixaPrFila.Remove();
            else
                process = _altaPrFila.Remove();

            // Calcula tempo de execucao
            var processTempo = _cycleLength;
            var tempoRestante = process.TempoCPU - process.TempoExecutado;
            if (processTempo > tempoRestante)
            {
                processTempo = tempoRestante;
            }

            // Cria bloco anterior ao do processamento
            if(process.Blocks.Count == 0)
                process.Blocks.Add(new Block()
                {
                    Tipo = BlockTipoEnum.NonExec,
                    Tempo = process.Chegada
                });

            if (process.Blocks.Last().Tipo == BlockTipoEnum.NonExec
                || process.Blocks.Last().Tipo == BlockTipoEnum.FitaMagnetica
                || process.Blocks.Last().Tipo == BlockTipoEnum.Impressora)
                process.Blocks.Add(new Block()
                {
                    Tipo = BlockTipoEnum.AltaPrQueue,
                    Tempo = _instante - process.TempoTotal
                });
            else if (process.Blocks.Last().Tipo == BlockTipoEnum.Processo
                || process.Blocks.Last().Tipo == BlockTipoEnum.Disco)
                process.Blocks.Add(new Block()
                {
                    Tipo = BlockTipoEnum.BaixaPrQueue,
                    Tempo = _instante - process.TempoTotal
                });


            // Verifica se ocorrera IO e qual tipo e coloca os blocos de: Processo, espera em IOQueue e IO
            var blockIO = CalculateIO(processTempo);
            if (blockIO.Tempo != 0)
            {
                // Calcula quando o IO ocorrera
                var ioTime = (new Random()).Next(1, processTempo);

                process.Blocks.Add(new Block
                {
                    Tipo = BlockTipoEnum.Processo,
                    Tempo = ioTime
                });

                _instante = process.TempoTotal;

                if (_ioInstante > _instante)
                {
                    process.Blocks.Add(new Block
                    {
                        Tipo = BlockTipoEnum.IOQueue,
                        Tempo = _ioInstante - _instante
                    });
                }

                process.Blocks.Add(new Block(blockIO));

                _ioInstante = process.TempoTotal;
            }
            else
            {
                // Cria Bloco do processo
                process.Blocks.Add(new Block
                {
                    Tipo = BlockTipoEnum.Processo,
                    Tempo = processTempo
                });

                // Define prox instante
                _instante = process.TempoTotal;
            }
            

            return process;
        }

        private bool NextProcesses()
        {
            var newProcessList = new ProcessArray();
            
            // Verifica se chegou algum processo
            foreach (var process in _processList.Value)
            {
                if (process != null && process.TempoTotal <= _instante)
                {
                    newProcessList.Add(process);
                }
            }

            // Ordena os processos nas Filas
            while(newProcessList.Count > 0)
            {
                var nextInFila = new Process();
                foreach(var nP in newProcessList.Value)
                {
                    if (nP != null && nP == newProcessList.First())
                        nextInFila = nP;

                    if (nP != null &&  nextInFila.TempoTotal > nP.TempoTotal)
                        nextInFila = nP;
                }
                newProcessList.Remove(nextInFila);
                _processList.Remove(nextInFila);

                if (nextInFila.Blocks.Count == 0
                || nextInFila.Blocks.Last().Tipo == BlockTipoEnum.FitaMagnetica
                || nextInFila.Blocks.Last().Tipo == BlockTipoEnum.Impressora)
                    _altaPrFila.Add(new Process(nextInFila));
                else {
                    _baixaPrFila.Add(new Process(nextInFila));
                }

            }

            // Vai para o proximo instante de tempo se nao ha processo na fila, coloca processos na Fila
            if (_baixaPrFila.Count == 0 && _altaPrFila.Count == 0 && _processList.Count != 0)
            {
                return true;
            }

            return false;

        }

        private ProcessArray FinishOrQueueProcess(Process process, ProcessArray finishedProcessList)
        {
            // Se processo terminou, coloca na finishedProcessList
            if (process.TempoCPU == process.TempoExecutado)
            {
                finishedProcessList.Add(new Process(process));
                return finishedProcessList;
            }

            if (process.Blocks.Last().Tipo != BlockTipoEnum.Processo)
            {
                _processList.Add(new Process(process));

                return finishedProcessList;
            }

            // Adiciona processo executado numa das Filas
            if (process.Blocks.Count == 0
                || process.Blocks.Last().Tipo == BlockTipoEnum.FitaMagnetica
                || process.Blocks.Last().Tipo == BlockTipoEnum.Impressora)
                _altaPrFila.Add(new Process(process));
            else if (process.Blocks.Last().Tipo == BlockTipoEnum.Processo
                || process.Blocks.Last().Tipo == BlockTipoEnum.Disco)
                _baixaPrFila.Add(new Process(process));


            return finishedProcessList;
        }

        private bool TemProcesso()
        {
            return (_baixaPrFila.Count > 0 || _altaPrFila.Count > 0 || _processList.Count > 0);
        }

        #endregion

        #region Metodos Auxiliares

        private void JumpTime()
        {
            // Define qual sera o instante inicial
            foreach (var processo in _processList.Value)
            {
                if (processo != null && processo == _processList.First())
                    _instante = processo.TempoTotal;

                if (processo != null &&  _instante > processo.TempoTotal)
                    _instante = processo.TempoTotal;
            }
        }

        private Block CalculateIO(int processTempo)
        {
            var block = new Block();

            if (processTempo == 1)
                return block;

            // Gera numeros de 1 a 5
            var rand = (new Random()).Next(1,6);

            // Se rand for 1, tem IO (20% de chances de IO)
            if(rand == 1)
            {
                // Gera numeros entre 3 e 5. Numeros correspondentes ao Tipo de IO no enumerado
                rand = (new Random()).Next(3,6);

                block = new Block((BlockTipoEnum)rand, rand);
            }

            return block;
        }

        #endregion

    }
}
