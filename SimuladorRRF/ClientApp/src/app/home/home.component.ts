import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as CanvasJS from '../../assets/canvasjs.min';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public processos: any[];
  public graphBlocks: any[];
  public data: any[];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.graphBlocks = [];
    this.data = [];
    
  }



  ngOnInit() {

    this.http.get<any[]>(this.baseUrl + 'api/Simulador/SimularProcessamento').subscribe(result => {
      this.processos = result;
      console.log(result)

      // Limpando Arrays
      let clearProcessos = [];
      for (let process of this.processos) {
        if (process != null)
          clearProcessos.push(process);
      }
      this.processos = clearProcessos;
      console.log(clearProcessos)

      // Setando blocos
      for (let process of this.processos) {
        let blocos = [];
        for (let bloco of process.blocks.value) {
          if (bloco != null)
            blocos.push(bloco);
        }
        process.blocks = blocos;
      }

      let tamBlocos = 0;
      // Verifica qual o tamanho maior de blocos
      for (let process of this.processos) {
        if (process.blocks.length > tamBlocos)
          tamBlocos = process.blocks.length;
      }

      // Cria todos os Arrays sem posicao
      for (let process of this.processos) {
        for (let i = 0; i < tamBlocos; i++) {

          // Alloca mais espaco no array
          if (this.graphBlocks.length == i)
            this.graphBlocks.push([]);

          this.graphBlocks[i].push([{ y: 0, label: process.id, color: "white" }]);

        }
      }

      // Traduz para os blocos
      for (let pos = 0; pos < this.processos.length; pos++) {
        for (let i = 0; i < this.processos[pos].blocks.length; i++) {
          // Insere Bloco
          this.graphBlocks[i][pos] = { y: this.processos[pos].blocks[i].tempo, label: this.processos[pos].id, color: this.processos[pos].blocks[i].color };
        }
      }

      for (let block of this.graphBlocks) {
        this.data.push(
          {
            type: "stackedBar",
            fillOpacity: .6,
            dataPoints: block
          }
        );
      }

      this.loadChart()

    }, error => console.error(error));
    
  }

  loadChart() {
    while (this.graphBlocks == []);

    let chart = new CanvasJS.Chart("chartContainer", {
      animationEnabled: false,
      exportEnabled: true,
      title: {
        text: "Resultado da simulação",
        fontFamily: "arial"
      },
      data: this.data
    });

    chart.render();
  }

}
