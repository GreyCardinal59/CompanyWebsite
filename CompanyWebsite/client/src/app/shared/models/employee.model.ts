export interface Employee {
  id: string;
  fullName: string;
  birthDate: string;
  hireDate: string;
  salary: number;
  departmentName: string;
}

export interface EmployeesResponse {
  employees: Employee[];
  totalCount: number;
}

export interface CreateEmployeeDto {
  fullName: string;
  birthDate: string;
  hireDate: string;
  salary: number;
  departmentId?: string | null;
  newDepartmentName?: string;
}

export interface UpdateEmployeeDto {
  fullName: string;
  birthDate: string;
  hireDate: string;
  salary: number;
  departmentId?: string | null;
  newDepartmentName?: string;
}

export interface GetEmployeesDto {
  page: number;
  pageSize: number;
  search?: string;
  departmentFilter?: string;
  fullNameFilter?: string;
  birthDateFilter?: string;
  hireDateFilter?: string;
  salaryFilter?: string;
  sortBy?: string;
  sortDirection?: 'asc' | 'desc';
}

export interface Department {
  id: string;
  name: string;
}

export interface AddDepartmentDto {
  name: string;
} 