import {
  Component,
  inject,
  OnInit,
  output,
  signal
} from '@angular/core';

import {
  FormBuilder,
  ReactiveFormsModule
} from '@angular/forms';

import { JobService } from '../../../job/service/job-service';

import { Job } from '../../../job/models/job';

import { ContactSearch } from '../../models/contact-search';

@Component({

  selector: 'app-contact-filters',

  standalone: true,

  imports: [

    ReactiveFormsModule

  ],

  templateUrl: './contact-filters.html'

})

export class ContactFilters implements OnInit {

  private fb = inject(FormBuilder);

  private jobService = inject(JobService);

  filtersChanged = output<Partial<ContactSearch>>();

  jobs = signal<Job[]>([]);

  form = this.fb.group({

    keyword: [''],

    jobId: [''],

    dateOfBirthFrom: [''],

    dateOfBirthTo: ['']

  });

  ngOnInit(): void {

    this.loadJobs();

  }

  loadJobs(): void {

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

  apply(): void {

    const value = this.form.value;

    this.filtersChanged.emit({

      keyword: value.keyword || undefined,

      jobId: value.jobId ? Number(value.jobId) : null,

      dateOfBirthFrom: value.dateOfBirthFrom || undefined,

      dateOfBirthTo: value.dateOfBirthTo || undefined

    });

  }

  clear(): void {

    this.form.reset({

      keyword: '',

      jobId: '',

      dateOfBirthFrom: '',

      dateOfBirthTo: ''

    });

    this.apply();

  }

}