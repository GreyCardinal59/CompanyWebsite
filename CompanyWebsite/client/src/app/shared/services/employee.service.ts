import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { 
  Employee, 
  EmployeesResponse, 
  CreateEmployeeDto, 
  UpdateEmployeeDto, 
  GetEmployeesDto, 
  AddDepartmentDto 
} from '../models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private apiUrl = '/api/employees'; // Обновленный URL с префиксом api

  constructor(private http: HttpClient) { }

  getEmployees(filters: GetEmployeesDto): Observable<EmployeesResponse> {
    let params = new HttpParams()
      .set('page', filters.page.toString())
      .set('pageSize', filters.pageSize.toString());

    if (filters.search) {
      params = params.set('search', filters.search);
    }

    // Добавляем дополнительные фильтры, которые мы добавили в модель
    if (filters.departmentFilter) {
      params = params.set('departmentFilter', filters.departmentFilter);
    }
    
    if (filters.fullNameFilter) {
      params = params.set('fullNameFilter', filters.fullNameFilter);
    }
    
    if (filters.birthDateFilter) {
      params = params.set('birthDateFilter', filters.birthDateFilter);
    }
    
    if (filters.hireDateFilter) {
      params = params.set('hireDateFilter', filters.hireDateFilter);
    }
    
    if (filters.salaryFilter) {
      params = params.set('salaryFilter', filters.salaryFilter);
    }
    
    if (filters.sortBy) {
      params = params.set('sortBy', filters.sortBy);
      if (filters.sortDirection) {
        params = params.set('sortDirection', filters.sortDirection);
      }
    }

    return this.http.get<EmployeesResponse>(this.apiUrl, { params });
  }

  getEmployeeById(id: string): Observable<Employee> {
    return this.http.get<Employee>(`${this.apiUrl}/${id}`);
  }

  createEmployee(employee: CreateEmployeeDto): Observable<string> {
    return this.http.post<string>(this.apiUrl, employee);
  }

  updateEmployee(id: string, employee: UpdateEmployeeDto): Observable<string> {
    return this.http.put<string>(`${this.apiUrl}/${id}`, employee);
  }

  deleteEmployee(id: string): Observable<string> {
    return this.http.delete<string>(`${this.apiUrl}/${id}`);
  }

  addDepartment(employeeId: string, department: AddDepartmentDto): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/${employeeId}/departments`, department);
  }
} 