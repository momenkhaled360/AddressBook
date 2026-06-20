import { inject, Injectable } from '@angular/core';

import {
  HttpClient,
  HttpParams
} from '@angular/common/http';

import { Observable } from 'rxjs';

import { Contact } from '../models/contact';

import { ContactRequest } from '../models/contact-request';

import { ContactSearch } from '../models/contact-search';

export interface PaginatedResult<T> {

  items: T[];

  totalCount: number;

}

@Injectable({
  providedIn: 'root'
})

export class ContactService {

  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7092/api/contact';

  getAll(): Observable<Contact[]> {

    return this.http.get<Contact[]>(
      this.apiUrl
    );

  }

  getById(id: number): Observable<Contact> {

    return this.http.get<Contact>(
      `${this.apiUrl}/${id}`
    );

  }

  search(
    search: ContactSearch
  ): Observable<PaginatedResult<Contact>> {

    let params = new HttpParams();

    Object.entries(search)
      .forEach(([key, value]) => {

        if (
          value !== null &&
          value !== undefined &&
          value !== ''
        ) {

          params = params.set(
            key,
            value.toString()
          );

        }

      });

  return this.http.get<PaginatedResult<Contact>>(

    `${this.apiUrl}/search`,

    { params }

  );

}

create(formData: FormData) {

  return this.http.post<Contact>(
    this.apiUrl,
    formData
  );

}

update(
  id: number,
  formData: FormData
) {

  return this.http.put(

    `${this.apiUrl}/${id}`,

    formData

  );

}

  delete(
    id: number
  ) {

    return this.http.delete(

      `${this.apiUrl}/${id}`

    );

  }

  exportToExcel(): Observable<Blob> {

    return this.http.get(

      `${this.apiUrl}/export`,

      {

        responseType: 'blob'

      }

    );

  }

}