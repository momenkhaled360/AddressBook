import {

  Component,

  inject

} from '@angular/core';

import {

  LoadingService

} from '../../services/loading';

@Component({

  selector: 'app-loading',

  standalone: true,

  templateUrl: './loading.html'

})

export class Loading {

  loadingService =

    inject(LoadingService);

}