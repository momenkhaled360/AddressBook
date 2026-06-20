import {
  Component,
  EventEmitter,
  inject,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
  signal
} from '@angular/core';

import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { ContactService } from '../../services/contact-service';

import { NotificationService } from '../../../../shared/services/notification-service';

import { ContactRequest } from '../../models/contact-request';

import { Contact } from '../../models/contact';

import { JobService } from '../../../job/service/job-service';

import { DepartmentService } from '../../../department/services/department-service';

import { Job } from '../../../job/models/job';

import { Department } from '../../../department/models/department';

@Component({
  selector: 'app-contact-form',

  standalone: true,

  imports: [

    ReactiveFormsModule

  ],

  templateUrl: './contact-form.html',

  styleUrl: './contact-form.css'

})

export class ContactForm
implements OnInit, OnChanges {

  private fb = inject(FormBuilder);

  private contactService = inject(ContactService);

  private notificationService = inject(NotificationService);

  private jobService = inject(JobService);

  private departmentService = inject(DepartmentService);

  @Input()

  contact: Contact | null = null;

  @Output()

  saved = new EventEmitter<void>();

  @Output()

  updated = new EventEmitter<void>();

  @Output()

  closed = new EventEmitter<void>();

  isEditMode = false;

  loading = false;

  serverError = '';

  jobs = signal<Job[]>([]);

  departments = signal<Department[]>([]);

  selectedFile: File | null = null;

  contactForm = this.fb.group({

    fullName: ['', Validators.required],

    jobId: ['', Validators.required],

    departmentId: ['', Validators.required],

    mobileNumber: ['', Validators.required],

    dateOfBirth: ['', Validators.required],

    address: ['', Validators.required],

    email: [

      '',

      [

        Validators.required,

        Validators.email

      ]

    ],

    password: ['', Validators.required]

  });

  ngOnInit(): void {

    this.loadJobs();

    this.loadDepartments();

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

  loadDepartments(): void {

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

  ngOnChanges(changes: SimpleChanges): void {

    if (!changes['contact'] || !this.contact) {

      return;

    }

    this.isEditMode = true;

    this.contactForm.patchValue({

      fullName: this.contact.fullName,

      jobId: this.contact.jobId.toString(),

      departmentId: this.contact.departmentId.toString(),

      mobileNumber: this.contact.mobileNumber,

      dateOfBirth: this.contact.dateOfBirth

        ? this.contact.dateOfBirth.split('T')[0]

        : '',

      address: this.contact.address,

      email: this.contact.email,

      password: this.contact.password

    });

  }

  get f() {

    return this.contactForm.controls;

  }

  onFileSelected(event: Event): void {

    const input = event.target as HTMLInputElement;

    if (input.files?.length) {

      this.selectedFile = input.files[0];

    }

  }

  saveContact(): void {

    this.serverError = '';

    if (this.contactForm.invalid) {

      this.contactForm.markAllAsTouched();

      return;

    }

    const formData = new FormData();

    formData.append(
      'fullName',
      this.contactForm.value.fullName ?? ''
    );

    formData.append(
      'jobId',
      String(this.contactForm.value.jobId)
    );

    formData.append(
      'departmentId',
      String(this.contactForm.value.departmentId)
    );

    formData.append(
      'mobileNumber',
      this.contactForm.value.mobileNumber ?? ''
    );

    formData.append(
      'dateOfBirth',
      this.contactForm.value.dateOfBirth ?? ''
    );

    formData.append(
      'address',
      this.contactForm.value.address ?? ''
    );

    formData.append(
      'email',
      this.contactForm.value.email ?? ''
    );

    formData.append(
      'password',
      this.contactForm.value.password ?? ''
    );

    if (this.selectedFile) {

      formData.append(
        'photo',
        this.selectedFile
      );

    }

    this.loading = true;

    if (this.isEditMode) {

      this.contactService

        .update(
          this.contact!.id,
          formData
        )

        .subscribe({

          next: async () => {

            this.loading = false;

            await this.notificationService.success(

              'Contact updated successfully'

            );

            this.updated.emit();

          },

          error: (err) => {

            this.loading = false;

            this.handleServerError(err);

          }

        });

    }

    else {

      this.contactService

        .create(
          formData
        )

        .subscribe({

          next: async () => {

            this.loading = false;

            this.contactForm.reset();

            this.selectedFile = null;

            await this.notificationService.success(

              'Contact created successfully'

            );

            this.saved.emit();

          },

          error: (err) => {

            this.loading = false;

            this.handleServerError(err);

          }

        });

    }

  }

  private handleServerError(err: any): void {

    console.log(err);

    const message = err?.error?.message ?? 'Something went wrong. Please try again.';

    const lowerMessage = message.toLowerCase();

    if (lowerMessage.includes('email')) {

      this.f.email.setErrors({ server: message });

      this.f.email.markAsTouched();

    }

    else if (lowerMessage.includes('mobile') || lowerMessage.includes('phone')) {

      this.f.mobileNumber.setErrors({ server: message });

      this.f.mobileNumber.markAsTouched();

    }

    else {

      this.serverError = message;

    }

  }

}