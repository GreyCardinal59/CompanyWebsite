import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbDatepickerModule, NgbModal, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { EmployeeService } from '../../shared/services/employee.service';
import { DepartmentService } from '../../shared/services/department.service';
import { 
  Employee, 
  EmployeesResponse, 
  GetEmployeesDto, 
  CreateEmployeeDto, 
  UpdateEmployeeDto,
  Department
} from '../../shared/models/employee.model';
import { EmployeeFormComponent } from './employee-form/employee-form.component';
import { EmployeeDeleteModalComponent } from './employee-delete-modal/employee-delete-modal.component';
import { Subject } from 'rxjs';
import { debounceTime, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NgbDatepickerModule,
    NgbModalModule
  ],
  templateUrl: './employees.component.html',
  styleUrl: './employees.component.css'
})
export class EmployeesComponent implements OnInit, OnDestroy {
  employees: Employee[] = [];
  departments: Department[] = [];
  totalCount: number = 0;
  loading: boolean = false;
  Math = Math; // Для использования в шаблоне
  
  // Для дебаунса фильтрации
  private filterSubject = new Subject<void>();
  private destroy$ = new Subject<void>();
  
  // Фильтры
  filters: GetEmployeesDto = {
    page: 1,
    pageSize: 10,
    departmentFilter: '',
    fullNameFilter: '',
    birthDateFilter: '',
    hireDateFilter: '',
    salaryFilter: ''
  };
  
  // Сортировка
  sortBy: string | null = null;
  sortDirection: 'asc' | 'desc' = 'asc';

  constructor(
    private employeeService: EmployeeService,
    private departmentService: DepartmentService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.loadEmployees();
    this.loadDepartments();
    
    // Настройка дебаунса для фильтрации
    this.filterSubject.pipe(
      debounceTime(300), // Ждем 300мс после последнего ввода
      takeUntil(this.destroy$)
    ).subscribe(() => {
      this.filters.page = 1; // Сбрасываем на первую страницу при применении фильтров
      this.loadEmployees();
    });
  }
  
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadEmployees(): void {
    this.loading = true;
    
    // Добавляем сортировку к фильтрам
    if (this.sortBy) {
      this.filters.sortBy = this.sortBy;
      this.filters.sortDirection = this.sortDirection;
    }
    
    this.employeeService.getEmployees(this.filters).subscribe({
      next: (response: EmployeesResponse) => {
        this.employees = response.employees;
        this.totalCount = response.totalCount;
        this.loading = false;
      },
      error: (error) => {
        console.error('Ошибка при загрузке сотрудников:', error);
        this.loading = false;
      }
    });
  }

  loadDepartments(): void {
    this.departmentService.getDepartments().subscribe({
      next: (departments: Department[]) => {
        this.departments = departments;
      },
      error: (error) => {
        console.error('Ошибка при загрузке отделов:', error);
      }
    });
  }

  // Обработка сортировки
  sort(column: string): void {
    if (this.sortBy === column) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortBy = column;
      this.sortDirection = 'asc';
    }
    this.loadEmployees();
  }

  // Обработка изменения страницы
  onPageChange(page: number): void {
    this.filters.page = page;
    this.loadEmployees();
  }

  // Обработка фильтрации с дебаунсом
  applyFilters(): void {
    this.filterSubject.next();
  }

  // Сброс фильтров
  resetFilters(): void {
    this.filters = {
      page: 1,
      pageSize: 10,
      departmentFilter: '',
      fullNameFilter: '',
      birthDateFilter: '',
      hireDateFilter: '',
      salaryFilter: ''
    };
    this.sortBy = null;
    this.sortDirection = 'asc';
    this.loadEmployees();
  }

  // Открытие модального окна для создания сотрудника
  openCreateModal(): void {
    const modalRef = this.modalService.open(EmployeeFormComponent, { centered: true });
    modalRef.componentInstance.departments = this.departments;
    
    modalRef.result.then(
      (formData: CreateEmployeeDto) => {
        console.log('Отправляемые данные:', JSON.stringify(formData));
        
        // Проверка и обработка departmentId
        if (formData.departmentId === '') {
          formData.departmentId = null;
        }
        
        // Проверяем, что departmentId имеет правильный формат Guid или null
        if (formData.departmentId !== null && !this.isValidGuid(formData.departmentId)) {
          console.error('Неверный формат departmentId:', formData.departmentId);
          alert('Ошибка: Неверный формат идентификатора отдела. Пожалуйста, выберите отдел из списка или создайте новый.');
          return;
        }
        
        this.employeeService.createEmployee(formData).subscribe({
          next: (id: string) => {
            console.log('Сотрудник создан с ID:', id);
            
            // Если был создан новый отдел, очищаем кэш отделов
            if (formData.newDepartmentName) {
              this.departmentService.clearCache();
              this.loadDepartments(); // Перезагружаем список отделов
            }
            
            this.loadEmployees();
          },
          error: (error) => {
            console.error('Ошибка при создании сотрудника:', error);
            
            // Дополнительная информация об ошибке
            if (error.error) {
              console.error('Детали ошибки:', error.error);
            }
          }
        });
      },
      () => {}
    );
  }

  // Открытие модального окна для редактирования сотрудника
  openEditModal(employee: Employee): void {
    const modalRef = this.modalService.open(EmployeeFormComponent, { centered: true });
    modalRef.componentInstance.employee = employee;
    modalRef.componentInstance.departments = this.departments;
    
    modalRef.result.then(
      (formData: UpdateEmployeeDto) => {
        // Проверка и обработка departmentId
        if (formData.departmentId === '') {
          formData.departmentId = null;
        }
        
        // Проверяем, что departmentId имеет правильный формат Guid или null
        if (formData.departmentId !== null && !this.isValidGuid(formData.departmentId)) {
          console.error('Неверный формат departmentId:', formData.departmentId);
          alert('Ошибка: Неверный формат идентификатора отдела. Пожалуйста, выберите отдел из списка или создайте новый.');
          return;
        }
        
        this.employeeService.updateEmployee(employee.id, formData).subscribe({
          next: () => {
            console.log('Сотрудник обновлен');
            
            // Если был создан новый отдел, очищаем кэш отделов
            if (formData.newDepartmentName) {
              this.departmentService.clearCache();
              this.loadDepartments(); // Перезагружаем список отделов
            }
            
            this.loadEmployees();
          },
          error: (error) => {
            console.error('Ошибка при обновлении сотрудника:', error);
          }
        });
      },
      () => {}
    );
  }

  // Открытие модального окна для удаления сотрудника
  openDeleteModal(employee: Employee): void {
    const modalRef = this.modalService.open(EmployeeDeleteModalComponent, { centered: true });
    modalRef.componentInstance.employee = employee;
    
    modalRef.result.then(
      (result: boolean) => {
        if (result) {
          this.employeeService.deleteEmployee(employee.id).subscribe({
            next: () => {
              console.log('Сотрудник удален');
              this.loadEmployees();
            },
            error: (error) => {
              console.error('Ошибка при удалении сотрудника:', error);
            }
          });
        }
      },
      () => {}
    );
  }
  
  // Проверка, является ли строка валидным Guid
  private isValidGuid(guid: string | null | undefined): boolean {
    if (!guid) return false;
    const guidRegex = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;
    return guidRegex.test(guid);
  }
} 