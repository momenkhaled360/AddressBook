import {
  Component,
  inject,
  OnInit,
  signal
} from '@angular/core';

import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { DepartmentService } from '../../services/department-service';

import { Department } from '../../models/department';

import { NotificationService } from '../../../../shared/services/notification-service';

@Component({

  selector: 'app-department-management',

  standalone: true,

  imports: [

    ReactiveFormsModule

  ],

  templateUrl: './department-management.html'

})

export class DepartmentManagement implements OnInit {

  private departmentService = inject(DepartmentService);

  private fb = inject(FormBuilder);

  private notify = inject(NotificationService);

  departments = signal<Department[]>([]);

  editingId: number | null = null;

  form = this.fb.group({

    name: [

      '',

      Validators.required

    ]

  });

  ngOnInit(): void {

    this.getDepartments();

  }

  getDepartments(): void {

    this.departmentService
      .getAll()
      .subscribe({

        next: (data) => {

          this.departments.set(data);

        },

        error: (err) => {

          console.log(err);

        }

      });

  }

  save(): void {

    if (this.form.invalid) {

      return;

    }

    const dto: Department = {

      id: 0,

      name: this.form.value.name ?? ''

    };

    if (this.editingId) {

      this.departmentService
        .update(
          this.editingId,
          dto
        )
        .subscribe({

          next: () => {

            this.notify.success('Department updated successfully');

            this.cancelEdit();

            this.getDepartments();

          },

          error: (err) => {

            console.log(err);

            this.notify.error('Failed to update department');

          }

        });

      return;

    }

    this.departmentService
      .create(dto)
      .subscribe({

        next: () => {

          this.notify.success('Department added successfully');

          this.form.reset();

          this.getDepartments();

        },

        error: (err) => {

          console.log(err);

          this.notify.error('Failed to add department');

        }

      });

  }

  edit(department: Department): void {

    this.editingId = department.id;

    this.form.patchValue({

      name: department.name

    });

  }

  cancelEdit(): void {

    this.editingId = null;

    this.form.reset();

  }

  delete(id: number): void {

    this.departmentService
      .delete(id)
      .subscribe({

        next: () => {

          this.notify.success('Department deleted successfully');

          this.getDepartments();

        },

        error: (err) => {

          console.log(err);

          this.notify.error('Failed to delete department');

        }

      });

  }

}