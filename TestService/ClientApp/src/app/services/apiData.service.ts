
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { RestService } from '../services/base/rest.service';

import { environment } from '../../environments/environment';
import {ApiData} from "../models/ApiData";

@Injectable({
  providedIn: 'root'
})
export class ApiClientService extends RestService<ApiData> {

  constructor(public httpClient: HttpClient) {
    super(httpClient, environment.apiUrl, 'test');
  }
}
