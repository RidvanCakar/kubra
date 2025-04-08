import { CanActivateFn, Router } from "@angular/router";
import { inject } from "@angular/core";

export const süperAdminGuard: CanActivateFn = () => {
  const token = localStorage.getItem("token");
  const userRole = localStorage.getItem("rolId");
  const router = inject(Router);

  if (token && userRole) {
    if (userRole === "1") {
      return true;
    }
    console.log("Bu sayfaya erişim yetkiniz yok.");
    router.navigate(["/"]);
    return false;
  }
  console.log("Oturum Açmanız Gerekli");
  router.navigate(["/auth"]);
  return false;
}
