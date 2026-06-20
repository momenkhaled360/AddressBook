import { inject, Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Department } from '../models/department';

@Injectable({

  providedIn: 'root'

})

export class DepartmentService {

  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7092/api/department';

  getAll(): Observable<Department[]> {

    return this.http.get<Department[]>(this.apiUrl);

  }

  getById(id: number): Observable<Department> {

    return this.http.get<Department>(`${this.apiUrl}/${id}`);

  }

  create(department: Department): Observable<Department> {

    return this.http.post<Department>(this.apiUrl, department);

  }

  update(
    id: number,
    department: Department
  ): Observable<void> {

    return this.http.put<void>(
      `${this.apiUrl}/${id}`,
      department
    );

  }

  delete(id: number): Observable<void> {

    return this.http.delete<void>(
      `${this.apiUrl}/${id}`
    );

  }

}