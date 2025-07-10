import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgbActiveModal, NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { CreateEmployeeDto, Department, Employee, UpdateEmployeeDto } from '../../../shared/models/employee.model';

@Component({
  selector: 'app-employee-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NgbDatepickerModule],
  templateUrl: './employee-form.component.html',
  styleUrl: './employee-form.component.css'
})
export class EmployeeFormComponent implements OnInit {
  @Input() employee: Employee | null = null;
  @Input() departments: Department[] = [];
  @Output() save = new EventEmitter<CreateEmployeeDto | UpdateEmployeeDto>();
  
  employeeForm!: FormGroup;
  isNewDepartment: boolean = false;
  
  constructor(
    public activeModal: NgbActiveModal,
    private fb: FormBuilder
  ) {}
  
  ngOnInit(): void {
    this.initForm();
    
    if (this.employee) {
      // Заполняем форму данными сотрудника при редактировании
      const departmentId = this.departments.find(d => d.name === this.employee?.departmentName)?.id || null;
      this.employeeForm.patchValue({
        fullName: this.employee.fullName,
        birthDate: this.employee.birthDate,
        hireDate: this.employee.hireDate,
        salary: this.employee.salary,
        departmentId: departmentId
      });
    }
  }
  
  initForm(): void {
    this.employeeForm = this.fb.group({
      fullName: ['', [Validators.required, Validators.maxLength(100)]],
      birthDate: ['', Validators.required],
      hireDate: ['', Validators.required],
      salary: [0, [Validators.required, Validators.min(0)]],
      departmentId: [null],
      newDepartmentName: ['']
    });
  }
  
  toggleDepartmentType(): void {
    this.isNewDepartment = !this.isNewDepartment;
    
    if (this.isNewDepartment) {
      this.employeeForm.get('departmentId')?.setValue(null);
      this.employeeForm.get('departmentId')?.clearValidators();
      this.employeeForm.get('newDepartmentName')?.setValidators([Validators.required]);
    } else {
      this.employeeForm.get('newDepartmentName')?.setValue('');
      this.employeeForm.get('newDepartmentName')?.clearValidators();
      this.employeeForm.get('departmentId')?.setValidators([Validators.required]);
    }
    
    this.employeeForm.get('departmentId')?.updateValueAndValidity();
    this.employeeForm.get('newDepartmentName')?.updateValueAndValidity();
  }
  
  onSubmit(): void {
    if (this.employeeForm.invalid) {
      // Отмечаем все поля как затронутые, чтобы показать ошибки валидации
      Object.keys(this.employeeForm.controls).forEach(key => {
        const control = this.employeeForm.get(key);
        control?.markAsTouched();
      });
      return;
    }
    
    const formData = this.employeeForm.value;
    
    // Проверяем, есть ли у нас departmentId или newDepartmentName
    if ((!formData.departmentId || formData.departmentId === '') && !formData.newDepartmentName) {
      alert('Необходимо выбрать отдел или создать новый');
      return;
    }
    
    // Преобразуем пустую строку в null для departmentId
    if (formData.departmentId === '') {
      formData.departmentId = null;
    }
    
    this.activeModal.close(formData);
  }
} 