import {

  Injectable,

  signal

} from '@angular/core';

@Injectable({

  providedIn: 'root'

})

export class LoadingService {

  isLoading = signal(false);

  private activeRequests = 0;

  private timer: any;

  show(): void {

    this.activeRequests++;

    if (this.activeRequests === 1) {

      this.timer = setTimeout(() => {

        this.isLoading.set(true);

      }, 300);

    }

  }

  hide(): void {

    this.activeRequests--;

    if (this.activeRequests <= 0) {

      this.activeRequests = 0;

      clearTimeout(this.timer);

      this.isLoading.set(false);

    }

  }

}