import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Department } from '../models/employee.model';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  private departments: Department[] = [];
  private apiUrl = '/api/departments'; // Обновленный URL для отделов

  constructor(private http: HttpClient) {}

  // Получение всех отделов из API
  getDepartments(): Observable<Department[]> {
    // Если у нас уже есть кэшированные отделы, возвращаем их
    if (this.departments.length > 0) {
      return of(this.departments);
    }

    // Иначе делаем запрос к API
    return this.http.get<Department[]>(this.apiUrl).pipe(
      map(departments => {
        // Кэшируем отделы
        this.departments = departments;
        return departments;
      }),
      catchError(error => {
        console.error('Ошибка при загрузке отделов:', error);
        return of([]);
      })
    );
  }

  // Очистка кэша отделов (используется при добавлении нового отдела)
  clearCache(): void {
    this.departments = [];
  }
} 