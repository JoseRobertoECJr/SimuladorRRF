import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {

  public processos: any[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<any[]>(baseUrl + 'api/Simulador/FixedData').subscribe(result => {
      this.processos = result;
      console.log(result)
    }, error => console.error(error));
  }
}
