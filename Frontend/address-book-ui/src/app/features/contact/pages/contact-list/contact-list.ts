import {
  Component,
  inject,
  OnInit,
  signal
} from '@angular/core';

import { ContactService } from '../../services/contact-service';

import { Contact } from '../../models/contact';

import { ContactSearch } from '../../models/contact-search';

import { ContactForm } from '../contact-form/contact-form';

import { ContactFilters } from '../contact-filters/contact-filters';

import { Pagination } from
'../../../../shared/compoents/pagination/pagination';

import { JobManagement } from
'../../../job/components/job-management/job-management';

import { DepartmentManagement } from
'../../../department/components/department-management/department-management';

import { AuthService } from
'../../../Auth/services/auth-service';

import { NotificationService } from
'../../../../shared/services/notification-service';

import { Router } from '@angular/router';

import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-contact-list',

  standalone: true,

  imports: [

    ContactForm,
    ContactFilters,
    Pagination,
    JobManagement,
    DepartmentManagement,
    DatePipe

  ],

  templateUrl: './contact-list.html',

  styleUrl: './contact-list.css'

})

export class ContactList implements OnInit {

  private contactService = inject(ContactService);

  private authService = inject(AuthService);

  private notify = inject(NotificationService);

  private router = inject(Router);

  contacts = signal<Contact[]>([]);

  totalCount = signal<number>(0);

  exporting = signal<boolean>(false);

  showCreatePopup = false;

  showEditPopup = false;

  showDeletePopup = false;

  showJobPopup = false;

  showDepartmentPopup = false;

  showMobileActions = false;

  selectedContact: Contact | null = null;

  searchParams: ContactSearch = {

    pageNumber: 1,

    pageSize: 10

  };

  ngOnInit(): void {

    this.getContacts();

  }

  getContacts(): void {

    this.contactService
      .search(this.searchParams)
      .subscribe({

        next: (result) => {
        console.log('result', result);

          this.contacts.set(result.items);

          this.totalCount.set(result.totalCount);
          // console.log(this.contacts);

        },

        error: (err) => {

          console.log(err);

        }

      });

  }

  onFiltersChanged(filters: Partial<ContactSearch>): void {

    console.log(filters);

    this.searchParams = {

      ...this.searchParams,

      ...filters,

      pageNumber: 1

    };

    this.getContacts();

  }

  onPageChanged(page: number): void {

    this.searchParams = {

      ...this.searchParams,

      pageNumber: page

    };

    this.getContacts();

  }


  openCreatePopup(): void {

    this.selectedContact = null;

    this.showCreatePopup = true;

  }

  closeCreatePopup(): void {

    this.showCreatePopup = false;

  }

  onSaved(): void {

    this.closeCreatePopup();

    this.getContacts();

  }


  openEditPopup(contact: Contact): void {

    this.selectedContact = contact;

    this.showEditPopup = true;

  }

  closeEditPopup(): void {

    this.showEditPopup = false;

    this.selectedContact = null;

  }

  onUpdated(): void {

    this.closeEditPopup();

    this.getContacts();

  }


  openDeletePopup(contact: Contact): void {

    this.selectedContact = contact;

    this.showDeletePopup = true;

  }

  closeDeletePopup(): void {

    this.showDeletePopup = false;

    this.selectedContact = null;

  }

  deleteContact(): void {

    if (!this.selectedContact) {

      return;

    }

    this.contactService
      .delete(this.selectedContact.id)
      .subscribe({

        next: () => {

          this.closeDeletePopup();

          this.getContacts();

        },

        error: (err) => {

          console.log(err);

        }

      });

  }


  openJobPopup(): void {

    this.showJobPopup = true;

  }

  closeJobPopup(): void {

    this.showJobPopup = false;

  }


  openDepartmentPopup(): void {

    this.showDepartmentPopup = true;

  }

  closeDepartmentPopup(): void {

    this.showDepartmentPopup = false;

  }


  exportToExcel(): void {

    this.exporting.set(true);

    this.contactService
      .exportToExcel()
      .subscribe({

        next: (blob) => {

          this.exporting.set(false);

          const url = window.URL.createObjectURL(blob);

          const link = document.createElement('a');

          link.href = url;

          link.download = 'AddressBook.xlsx';

          link.click();

          window.URL.revokeObjectURL(url);

        },

        error: (err) => {

          this.exporting.set(false);

          console.log(err);

          this.notify.error('Failed to export contacts');

        }

      });

  }


  logout(): void {

    this.authService.logout();

    this.router.navigate(['/login']);

  }

}