import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user-detail',
  imports: [CommonModule],
  templateUrl: './user-detail.component.html',
  styleUrl: './user-detail.component.css'
})
export class UserDetailComponent {

  user: any;

  get username(): string {
    return localStorage.getItem("username") || "";
  }

  constructor(private router: Router, private userservice: UserService) { }

  ngOnInit(): void {
    this.loadUser();
  }

  loadUser(): void {
    const id = localStorage.getItem("userId");

    if (!id) {
      console.error("User ID bulunamadı!");
      return;
    }

    this.userservice.getUser(parseInt(id)).subscribe({
      next: (users) => {
        this.user = [users];
        console.log("User", users);
      },
      error: (error) => {
        console.error('User yüklenirken hata:', error);
      }
    });

  }

  logout() {
    localStorage.removeItem('token');

    this.router.navigate(['/login']);
  }

}
