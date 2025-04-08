import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { User } from '../Model';
import { RouterModule } from '@angular/router';
import { UserService } from '../services/user.service';
import {Router} from "@angular/router"

@Component({
  selector: 'app-seller-auth',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './seller-auth.component.html',
  styleUrl: './seller-auth.component.css'
})
export class SellerAuthComponent {
  constructor(private router:Router, private userservice: UserService) { }

  addSeller(username: string, email: string, password: string) {
    const p: User = {
      id:0,
      username: username,
      email: email,
      password: password
    };

    const roleID = 2;

    this.userservice.saveUser(p, roleID).subscribe(
      (seller) => {
        console.log("Kullanıcı Kaydedildi:", seller);
        this.router.navigate(["/"]);
      },
      (error) => {
        console.error("Kullanıcı kaydedilirken bir hata oluştu:", error);
      }
    );
  }
}
