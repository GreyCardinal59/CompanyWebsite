import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Employee } from '../../../shared/models/employee.model';

@Component({
  selector: 'app-employee-delete-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './employee-delete-modal.component.html',
  styleUrl: './employee-delete-modal.component.css'
})
export class EmployeeDeleteModalComponent {
  @Input() employee!: Employee;
  
  constructor(public activeModal: NgbActiveModal) {}
  
  confirm(): void {
    this.activeModal.close(true);
  }
  
  cancel(): void {
    this.activeModal.dismiss();
  }
} 