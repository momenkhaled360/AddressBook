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

import { JobService } from '../../service/job-service';

import { Job } from '../../models/job';

import { NotificationService } from '../../../../shared/services/notification-service';

@Component({

  selector: 'app-job-management',

  standalone: true,

  imports: [

    ReactiveFormsModule

  ],

  templateUrl: './job-management.html'

})

export class JobManagement implements OnInit {

  private jobService = inject(JobService);

  private fb = inject(FormBuilder);

  private notify = inject(NotificationService);

  jobs = signal<Job[]>([]);

  editingId: number | null = null;

  form = this.fb.group({

    name: [

      '',

      Validators.required

    ]

  });

  ngOnInit(): void {

    this.getJobs();

  }

  getJobs(): void {

    this.jobService
      .getAll()
      .subscribe({

        next: (data) => {

          this.jobs.set(data);

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

    const dto: Job = {

      id: 0,

      name: this.form.value.name ?? ''

    };

    if (this.editingId) {

      this.jobService
        .update(
          this.editingId,
          dto
        )
        .subscribe({

          next: () => {

            this.notify.success('Job updated successfully');

            this.cancelEdit();

            this.getJobs();

          },

          error: (err) => {

            console.log(err);

            this.notify.error('Failed to update job');

          }

        });

      return;

    }

    this.jobService
      .create(dto)
      .subscribe({

        next: () => {

          this.notify.success('Job added successfully');

          this.form.reset();

          this.getJobs();

        },

        error: (err) => {

          console.log(err);

          this.notify.error('Failed to add job');

        }

      });

  }

  edit(job: Job): void {

    this.editingId = job.id;

    this.form.patchValue({

      name: job.name

    });

  }

  cancelEdit(): void {

    this.editingId = null;

    this.form.reset();

  }

  delete(id: number): void {

    this.jobService
      .delete(id)
      .subscribe({

        next: () => {

          this.notify.success('Job deleted successfully');

          this.getJobs();

        },

        error: (err) => {

          console.log(err);

          this.notify.error('Failed to delete job');

        }

      });

  }

}