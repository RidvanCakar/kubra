import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { User } from '../Model';
import { RouterModule, Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css'
})
export class AuthComponent {
  isRegisterMode = false;
  errorMessage: string = " ";
  successMessage: string = " ";

  user: User = {
    id:0,
    username: '',
    email: '',
    password: ''
  };

  constructor(private userService: UserService, private router: Router) { }

  toggleMode() {
    this.isRegisterMode = !this.isRegisterMode;
  }

  addUser(username: string, email: string, password: string) {
    const p: User = {id:0,username, email, password };
    const roleID = 3;

    this.userService.saveUser(p, roleID).subscribe({
      next: (user) => {
        console.log("Kullanıcı Kaydedildi:", user);
      },
      error: (error) => {
        console.error("Kullanıcı kaydedilirken bir hata oluştu:", error);
        this.errorMessage = 'Kayıt işlemi başarısız oldu.';
      }
    });
  }

  loginUser(email: string, password: string) {
    this.errorMessage = '';
    this.successMessage = '';

    this.userService.loginUser(email, password).subscribe({
      next: (response) => {
        console.log('Giriş başarılı:', response);
        this.successMessage = "Giriş Başarılı.";

        const userId = response.id;
        const username = response.username;
        const token = response.token;

        localStorage.setItem('userId', userId.toString());
        localStorage.setItem('username', username);
        localStorage.setItem("token", token);

        console.log('User ID:', userId);
        console.log('Username:', username);

        this.userService.getUserRole(userId).subscribe({
          next: (roles: Role[]) => {
            console.log("Kullanıcı Rolleri:", roles);

            const roleID = roles.length > 0 ? roles[0].roleID : null;
            const rolName = roles.length > 0 ? roles[0].rolName : null;

            if (roleID !== null && roleID !== undefined) {
              localStorage.setItem("rolId", roleID.toString());
            }

            if (rolName !== null && rolName !== undefined) {
              localStorage.setItem("rolName", rolName);
            }

            console.log("roleID:", roleID);
            console.log("RolName:", rolName);

            if (roles.some(r => r.rolName === "Süper Admin")) {
              this.router.navigate([`süperAdmin/${userId}`]);
            } else if (roles.some(r => r.rolName === "Admin")) {
              this.router.navigate([`admin/${userId}`]);
            } else {
              this.router.navigate([`user/${userId}`]);
            }
          },
          error: (error) => {
            console.error("Rol bilgisi alınamadı:", error);
            this.errorMessage = "Rol bilgisi alınırken hata oluştu.";
          }
        });
      },
      error: (error) => {
        console.error('Giriş hatası:', error);
        this.errorMessage = 'Geçersiz e-posta veya şifre.';
      }
    });
  }
}


export interface Role {
  roleID: number;
  id: number;
  rolName: string;
}
