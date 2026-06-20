import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';
@Injectable({
  providedIn: 'root',
})
export class NotificationService {

  success(message: string) {

    return Swal.fire({

      icon: 'success',

      title: 'Success',

      text: message,

      confirmButtonColor: '#2563eb'

    });

  }

  error(message: string) {

    return Swal.fire({

      icon: 'error',

      title: 'Error',

      text: message

    });

  }

}
