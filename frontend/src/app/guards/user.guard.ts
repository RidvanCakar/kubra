import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';

export const userGuard: CanActivateFn = () => {
  const token = localStorage.getItem('token');
  const userRole = localStorage.getItem('rolId');
  const router = inject(Router);

  if (token && userRole) {
    if (userRole === '3') {
      return true;
    }
    console.log('Bu sayfaya erişim yetkiniz yok.');
    router.navigate(['/']);
    return false;
  }

  console.log('Oturum açmanız gerekiyor.');
  router.navigate(['/auth']);
  return false;
};
