import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';

export class RestService<TModel> {

    baseUrl = '';
    entityUrl = '';

    constructor(protected httpClient: HttpClient, baseUrl: string, entityUrl: string) {
        this.baseUrl = baseUrl;
        this.entityUrl = baseUrl + entityUrl;
    }

    public fetchAll(): Observable<TModel> {
      const route = `${this.entityUrl}/get`;

      return this.httpClient.get<TModel>(route);
    }

    public fetchItem(id: number): Observable<TModel> {
        const route = `${this.entityUrl}/${id}`;

        return this.httpClient.get<TModel>(route);
    }

    public create(item: TModel): Observable<TModel> {
        return this.httpClient.post<TModel>(this.entityUrl, item);
    }

    public update(item: TModel): Observable<void> {
        const route = `${this.entityUrl}`;

        return this.httpClient.put<void>(route, item);
    }

    public delete(ids: number[]): Observable<TModel> {
        let params = new HttpParams();
        params = params.append('ids', ids.join(','));

        return this.httpClient.delete<TModel>(`${this.entityUrl}`, { params: params });
    }
}
