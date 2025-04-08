import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Product } from '../Model';
import { ProductService } from '../services/product.service';
import { RouterModule } from '@angular/router';
import { ShoppingCardService } from '../services/shoppingCard.service';
import { ShoppingCard } from '../Model';

@Component({
  selector: 'app-home',
  imports: [CommonModule, RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  products: Product[] = [];

  constructor(private productService: ProductService, private shoppingCardService: ShoppingCardService) { }

  ngOnInit(): void {
    this.productService.getProducts().subscribe(
      products => {
        this.products = products;
      });
  }
  addShoppingCard() {
    const price = parseInt(localStorage.getItem("price") || "0");
    const userId = parseInt(localStorage.getItem("userId") || "0");
    const productId = parseInt(localStorage.getItem("productId") || "0");
    const imageUrl=(localStorage.getItem("imageUrl") || "0");
    const quantity=1;

    if (!price || !userId || !productId) {
      console.error("Eksik veri var! Lütfen kontrol edin.");
      return;
    }

    const shoppingCard = {
      price,
      userId,
      productId,
      imageUrl,
      quantity
    };

    this.shoppingCardService.saveShoppingCard(shoppingCard).subscribe({
      next: (response) => {
        console.log("Seppette Kayıtlı Ürün Bilgileri", response);
      }
    })
  }

}

