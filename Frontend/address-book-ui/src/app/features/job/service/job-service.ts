import { inject, Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Job } from '../models/job';

@Injectable({

  providedIn: 'root'

})

export class JobService {

  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7092/api/job';

  getAll(): Observable<Job[]> {

    return this.http.get<Job[]>(this.apiUrl);

  }

  getById(id: number): Observable<Job> {

    return this.http.get<Job>(`${this.apiUrl}/${id}`);

  }

  create(job: Job): Observable<Job> {

    return this.http.post<Job>(this.apiUrl, job);

  }

  update(id: number, job: Job): Observable<void> {

    return this.http.put<void>(`${this.apiUrl}/${id}`, job);

  }

  delete(id: number): Observable<void> {

    return this.http.delete<void>(`${this.apiUrl}/${id}`);

  }

}