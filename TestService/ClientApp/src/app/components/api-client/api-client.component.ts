import { Component, OnInit } from '@angular/core';
import {ApiClientService} from "../../services/apiData.service";

@Component({
  selector: 'app-api-client',
  templateUrl: './api-client.component.html',
  styleUrls: ['./api-client.component.less']
})
export class ApiClientComponent implements OnInit {

  constructor(private apiClientService: ApiClientService) { }

  data = null;

  ngOnInit() {
    this.getData();
  }

  getData() {

    this.apiClientService.fetchAll()
      .subscribe(
        result => {
          this.data = result;
        },
        error => {
          // this.alertService.error('Помилка завантаження даних');
        });
  }

}
