import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';

export const adminGuard: CanActivateFn = () => {
  const token = localStorage.getItem('token');
  const userRole = localStorage.getItem('rolId');
  const router = inject(Router);

  console.log("Token:", token);
  console.log("RoleID:", userRole);

  if (token && userRole) {
    if (userRole === "2") {
      console.log("Admin girişi başarılı!");
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
