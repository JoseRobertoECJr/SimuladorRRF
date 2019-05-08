import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {

  public processos: any[];
  public cycleLength: number

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.processos = [];
    http.get<any[]>(baseUrl + 'api/Simulador/FixedData').subscribe(result => {
      let processos = result;
      console.log(result)

      for (let processo of processos) {
        if (processo != null)
          this.processos.push(processo)
      }
      console.log(this.processos)

    }, error => console.error(error));

    this.getCycleLength();
  }

  getCycleLength() {
    this.http.get<number>(this.baseUrl + 'api/Simulador/CycleLength').subscribe(result => {
      this.cycleLength = result;
      console.log(result)
    }, error => console.error(error));
  }

  changeCycleLength() {
    this.http.get(this.baseUrl + 'api/Simulador/ChangeCycleLength?cycleLength=' + this.cycleLength).subscribe(() => {
      
    }, error => console.error(error));
  }

  changeProcessList() {
    let processList = this.processos;
    console.log(processList)
    this.http.post(this.baseUrl + 'api/Simulador/ChangeProcessListData', processList).subscribe(() => {

    }, error => console.error(error));
  }

  addProcess() {
    this.processos.push({ blocks: { count: 0, tempoExecutado: 0, turnAround: 0, value: Array(1000) }, id: "Z", chegada: 0, tempoCPU: 1, tempoExecutado: 0, turnAround: 0, tempoTotal: 0 });
  }

  deleteProcess(index: number) {
    this.processos.splice(index, 1);
  }

}
