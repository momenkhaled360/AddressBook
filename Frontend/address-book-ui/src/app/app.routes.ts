import { Routes } from '@angular/router';

import { ContactList } from './features/contact/pages/contact-list/contact-list';
import { Login } from '../app/features/Auth/pages/login/login';
import { Signup } from '../app/features/Auth/pages/signup/signup';

import { authGuard } from '../app/features/Auth/guards/auth-guard';
import { guestGuard } from '../app/features/Auth/guards/guest-guard';

export const routes: Routes = [

  {
    path: '',
    redirectTo: 'contacts',
    pathMatch: 'full'
  },

  {
    path: 'login',
    component: Login,
    canActivate: [guestGuard]
  },

  {
    path: 'signup',
    component: Signup,
    canActivate: [guestGuard]
  },

  {
    path: 'contacts',
    component: ContactList,
    canActivate: [authGuard]
  },

];